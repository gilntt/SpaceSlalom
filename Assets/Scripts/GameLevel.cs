using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameLevel : MonoBehaviour
{

	public GameObject launchPad;
	public List <Planet> planetsInLevel = new List<Planet> ();
	public List <Fuel> fuelInLevel = new List<Fuel> ();
	public ExitGate exitGate;
	public float levelFuelTime = 2f;
	GM gm;
	// Use this for initialization
	void Awake ()
	{
		InitLevel ();
		gm = FindObjectOfType <GM> ();
		gm.RestartLevelEvent += Gm_RestartLevelEvent;
	}


	void Gm_RestartLevelEvent ()
	{
		InitLevel ();
	}


	public void InitLevel ()
	{
		planetsInLevel = GetComponentsInChildren <Planet> ().ToList ();
		fuelInLevel = GetComponentsInChildren <Fuel> ().ToList ();
	}

	void OnDestroy()
	{
		gm.RestartLevelEvent -= Gm_RestartLevelEvent;
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
