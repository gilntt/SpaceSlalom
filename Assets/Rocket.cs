using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Rocket : MonoBehaviour {

	public float initialEngineForce = 20f;
	public float engineForce = 5f;
	//public float maxPullMagnitude = 3f;
	public float currentEnergy = 1;
	public float energyUsePerFrame = 0.005f;
	public float maxVelocityMagnitude = 2f;
	public float minVelocityMagnitude = 0.01f;
	public float gravityModifier = 2f;
	public GameObject afterBurner;
	List<Planet> pullingPlanets = new List<Planet> ();
	List<Planet> allPlanets = new List<Planet> ();
	float startRotation;
	Vector3 startPosition;

	Rigidbody2D rb;
	LineRenderer lr;

	void Awake()
	{
		rb = GetComponent <Rigidbody2D> ();
		lr = GetComponent <LineRenderer> ();

		startRotation = rb.rotation;
		startPosition = rb.position;
	}

	void Start()
	{
		Reset ();

	}
		
	public void Reset()
	{
		currentEnergy = 1;
		afterBurner.SetActive (false);
		rb.velocity = Vector2.zero;
		rb.position = startPosition;
		rb.rotation = startRotation;
		rb.AddRelativeForce (new Vector2(0,initialEngineForce));
		allPlanets.Clear ();
		allPlanets = FindObjectsOfType <Planet> ().ToList ();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{

		if (rb.velocity.magnitude > maxVelocityMagnitude)
		{
			rb.velocity = rb.velocity.normalized * maxVelocityMagnitude;
		}
		else if (rb.velocity.magnitude < minVelocityMagnitude)
		{
			//rb.velocity = rb.velocity.normalized * minVelocityMagnitude;
		} 


		Vector2 forceToApply = Vector2.zero;
		foreach (Planet planet in pullingPlanets)
		{
			Vector3 planetPullVector = planet.GetPullVector (transform.position);
			forceToApply += new Vector2 (planetPullVector.x, planetPullVector.y) * gravityModifier;

		}
		if (pullingPlanets.Count == 0)
		{
			var d = 10000000f;
			Planet closestPlanet = allPlanets [0];
			foreach (Planet planet in allPlanets)
			{
				var planet_d = (transform.position - planet.transform.position).magnitude;
				if (planet_d < d)
				{
					d = planet_d;
					closestPlanet = planet;
				}
			}
			Vector3 planetPullVector = closestPlanet.GetPullVector (transform.position);
			forceToApply += new Vector2 (planetPullVector.x, planetPullVector.y) * gravityModifier;

		}


		Vector3[] positionArray = new Vector3[] { transform.position, transform.position - new Vector3(rb.velocity.x,rb.velocity.y)};
		//lr.SetPositions (positionArray);



		//rb.AddRelativeForce (new Vector2 (0, engineVectorMagnitude_current));
	

		if ((Input.GetKey (KeyCode.Space)) && (currentEnergy > 0))
		{
			
				rb.AddRelativeForce (new Vector2 (0, engineForce));
				afterBurner.SetActive (true);
				currentEnergy -= energyUsePerFrame;

		}
		else
		{
			rb.AddForce (forceToApply);
			afterBurner.SetActive (false);
		}
	
		var angle = Mathf.Atan2(rb.velocity.x,rb.velocity.y) * Mathf.Rad2Deg;
		rb.rotation = -angle;




	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "PlanetGravity")
		{
			pullingPlanets.Add (col.gameObject.GetComponentInParent<Planet> ()); 
		}
	}


	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "PlanetGravity")
		{
			pullingPlanets.Remove (col.gameObject.GetComponentInParent<Planet> ()); 
		}
	}
}
