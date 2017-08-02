using UnityEngine;
using System.Collections;

public class Fuel : MonoBehaviour {

	public GameObject diamondGraphic;

	//RocketCollider rocketCollider;


	// Use this for initialization
	void Start () {
	
	}

	public void Reset()
	{
		diamondGraphic.SetActive (true);
		//rocketCollider = FindObjectOfType <RocketCollider> ();
		//rocketCollider.RocketCollision += RocketCollider_RocketCollision;
	}

	/*
	void RocketCollider_RocketCollision (GameObject obj)
	{

		if (obj.tag == "PlanetCollider")
			Reset ();
		if (obj == gameObject)
		{
			
			TurnOffGraphic ();
		}
			
	}
	*/

	public void TurnOffGraphic()
	{
		diamondGraphic.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
