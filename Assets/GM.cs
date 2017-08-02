using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {


	public float gameSpeed= 2f;

	RocketCollider rocketCollider;
	Rocket rocket;
	// Use this for initialization
	void Awake()
	{
		rocketCollider = FindObjectOfType <RocketCollider> ();
		rocketCollider.RocketCollision += RocketCollider_RocketCollision;
		rocket = FindObjectOfType <Rocket> ();
	}

	void RocketCollider_RocketCollision (GameObject obj)
	{
		if (obj.tag == "PlanetCollider")
		{
			rocket.Reset ();
			var fuels = FindObjectsOfType <Fuel> ();
			foreach (Fuel fuel in fuels)
			{
				fuel.Reset ();
			}
		}
			
		if (obj.tag == "Fuel")
		{
			obj.GetComponent <Fuel>().TurnOffGraphic ();

		}
		
	}

	void Start () {
		Time.timeScale = gameSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
