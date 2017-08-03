using UnityEngine;
using System.Collections;
using System;

public class RocketCollider : MonoBehaviour {


	public event Action<GameObject> RocketCollision = gm => {};
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		RocketCollision (col.gameObject);
	}
}
