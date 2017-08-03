using UnityEngine;
using System.Collections;

public class Turner : MonoBehaviour {

	public float rotationSpeed = 1f;
	GM gm;
	Quaternion origRotation;
	void Awake () 
	{
		gm = FindObjectOfType <GM> ();
		gm.RestartLevelEvent += Gm_RestartLevelEvent;
		origRotation = transform.rotation;

	}

	void Gm_RestartLevelEvent()
	{
		transform.rotation = origRotation;
	}

	void OnDestroy()
	{
		gm.RestartLevelEvent -= Gm_RestartLevelEvent;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3(0,0,rotationSpeed));
	}
}
