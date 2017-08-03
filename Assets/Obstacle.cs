using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	//GM gm;
	// Use this for initialization

	public float velocity = 5;

	void Awake ()
	{
		//gm = FindObjectOfType <GM> ();
		//gm.RestartLevelEvent += Gm_RestartLevelEvent;
	}

	void Gm_RestartLevelEvent()
	{
		
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.position += transform.up * velocity;
	}
}
