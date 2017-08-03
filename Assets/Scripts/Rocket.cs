using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Rocket : MonoBehaviour
{


	GM gm;
	InputListener inputListener;
	public float initialEngineForce = 20f;
	public float engineForce = 5f;
	//public bool gravityInactiveWhileEngineOn = true;
	public float engineGravityModifier = 0.2f;
	public float energyUsePerFrame = 0.005f;
	public float maxVelocityMagnitude = 2f;
	public float minVelocityMagnitude = 0.01f;
	public float gravityModifier = 2f;
	public GameObject afterBurner;
	public bool drawGravitationalPull = false;
	public Transform trailStart;
	List<Planet> pullingPlanets = new List<Planet> ();
//	List<Planet> allPlanets = new List<Planet> ();
	float currentEnergy = 1;
	float maxEnergy = 1;
	float startRotation;
	Vector3 startPosition;
	[HideInInspector]
	public bool engineOn = false;
	bool docked = false;
	Rigidbody2D rb;
	LineRenderer lr;

	void Awake ()
	{
		rb = GetComponent <Rigidbody2D> ();
		lr = GetComponent <LineRenderer> ();
		gm = FindObjectOfType <GM> ();
		gm.RestartLevelEvent += Gm_RestartLevel;

		inputListener = gm.gameObject.GetComponent <InputListener> ();
		inputListener.TouchScreen += InputListener_TouchScreen;
		inputListener.ReleaseScreen += InputListener_ReleaseScreen;
		inputListener.HoldScreen += InputListener_HoldScreen;
	}

	void InputListener_HoldScreen (Vector3 obj)
	{
		
	}

	void InputListener_ReleaseScreen (Vector3 obj)
	{
		DeactivateEngine ();
	}

	void InputListener_TouchScreen (Vector3 obj)
	{
		TryToActivateEngine ();
	}
		
	void Gm_RestartLevel ()
	{
		Reset ();
	}
		
	public void Reset ()
	{
		maxEnergy = gm.currentLevel.levelFuelTime;
		currentEnergy = maxEnergy;
		pullingPlanets.Clear ();
		afterBurner.SetActive (false);
		rb.velocity = Vector2.zero;
		transform.position = gm.currentLevel.launchPad.transform.position;
		transform.rotation = gm.currentLevel.launchPad.transform.rotation;
		engineOn = false;
		docked = true;
	}

	void TryToActivateEngine ()
	{

		if (currentEnergy > 0) {
			engineOn = true;
			afterBurner.SetActive (true);
			if (docked)
				LaunchRocket ();
		}

	}

	void DeactivateEngine ()
	{
		engineOn = false;
		afterBurner.SetActive (false);
	}

	public void LaunchRocket ()
	{
		docked = false;
		rb.AddRelativeForce (new Vector2 (0, initialEngineForce));
	}

	void FixedUpdate ()
	{
		if (gm.gmState != GM.GMState.Gameplay)
			return;
		
		rb.AddForce (ApplyPlanetGravitationalForces ());

		if (engineOn) 
		{
			rb.AddRelativeForce (new Vector2 (0, engineForce));
			currentEnergy -= Time.deltaTime;
			if (currentEnergy < 0)
			{
				currentEnergy = 0;
				DeactivateEngine ();
			}
		}
			
		var angle = Mathf.Atan2 (rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
		rb.rotation = -angle;

		if (rb.velocity.magnitude > maxVelocityMagnitude) {
			rb.velocity = rb.velocity.normalized * maxVelocityMagnitude;
		}
	}

	public float GetEnergyRatio()
	{
		return currentEnergy / maxEnergy;
	}

	Vector2 ApplyPlanetGravitationalForces ()
	{
		



		Vector2 forceToApply = Vector2.zero;

		foreach (Planet planet in pullingPlanets) {
			Vector3 planetPullVector = planet.GetPullVector (transform.position);
			forceToApply += new Vector2 (planetPullVector.x, planetPullVector.y) * gravityModifier;

		}
		if (pullingPlanets.Count == 0) {
			var d = 10000000f;
			Planet closestPlanet = gm.currentLevel.planetsInLevel [0];
			foreach (Planet planet in gm.currentLevel.planetsInLevel) {
				var planet_d = (transform.position - planet.transform.position).magnitude;
				if (planet_d < d) {
					d = planet_d;
					closestPlanet = planet;
				}
			}
			Vector3 planetPullVector = closestPlanet.GetPullVector (transform.position);
			forceToApply += new Vector2 (planetPullVector.x, planetPullVector.y) * gravityModifier;
		}

		if (drawGravitationalPull)
		{
			Vector3[] positionArray = new Vector3[] { transform.position, transform.position + new Vector3 (forceToApply.x, forceToApply.y)};
			lr.SetPositions (positionArray);
		}

		if (docked)
			return Vector2.zero;


		if (engineOn)
			forceToApply *= engineGravityModifier;




		return forceToApply;
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "PlanetGravity") 
		{
			var pullingPlanet = col.gameObject.GetComponentInParent<Planet> ();
			var pullingPlanetIndex = pullingPlanets.FindIndex (x => x = pullingPlanet);
			//Debug.Log (pullingPlanetIndex);
			if (pullingPlanetIndex == -1)
				pullingPlanets.Add (pullingPlanet); 

		}
	}


	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "PlanetGravity") {
			pullingPlanets.Remove (col.gameObject.GetComponentInParent<Planet> ()); 
		}
	}
}
