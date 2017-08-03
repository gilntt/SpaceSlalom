using UnityEngine;
using System.Collections;

public class Fuel : MonoBehaviour {

	public GameObject diamondGraphic;
	GM gm;
	public bool available = true;
	//RocketCollider rocketCollider;


	// Use this for initialization
	void Awake()
	{
		gm = FindObjectOfType <GM> ();
		gm.RestartLevelEvent += Gm_RestartLevelEvent;
	}

	void Gm_RestartLevelEvent ()
	{
		Reset ();
	}
		
	void Reset()
	{
		SetDiamondToAvailable ();

	}

	public void SetDiamondToAvailable()
	{
		diamondGraphic.SetActive (true);
		available = true;
	}

	public void SetDiamondToCollected()
	{
			diamondGraphic.SetActive (false);
			available = false;
	}
		
	void OnDestroy()
	{
		gm.RestartLevelEvent -= Gm_RestartLevelEvent;
	}

	void Update () {
	
	}
}
