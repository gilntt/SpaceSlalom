using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {

	Rocket rocket;
	public GameObject bar;

	void Awake()
	{
		rocket = FindObjectOfType <Rocket> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bar.transform.localScale = new Vector3(1,rocket.GetEnergyRatio (),1);
	}


}
