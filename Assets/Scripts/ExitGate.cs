using UnityEngine;
using System.Collections;

public class ExitGate : MonoBehaviour {

	public GameObject gateCollider;
	GM gm;
	void Awake()
	{
		gm = FindObjectOfType <GM> ();
		gm.RestartLevelEvent += Gm_RestartLevelEvent;
	}

	void Gm_RestartLevelEvent ()
	{
		CloseGate ();
	}

	public void CloseGate()
	{
		gateCollider.SetActive (false);
	}

	public void OpenGate()
	{
		gateCollider.SetActive (true);
	}

	void OnDestroy()
	{
		gm.RestartLevelEvent -= Gm_RestartLevelEvent;
	}

}
