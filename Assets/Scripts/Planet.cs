using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

//	GameObject collider;
	GameObject gravity;
	GM gm;
	public float maxGravity = 5f;

	public bool setGravityRange = true;

	public float minGravity = 0.1f;
	public float gravityRange = 3f;
	 float radius;
	TextMesh planetText;
	// Use this for initialization
	void Awake ()
	{
		gm = FindObjectOfType <GM> ();
		gm.RestartLevelEvent += Gm_RestartLevelEvent;
		//collider = transform.Find ("PlanetCollider").gameObject;
		gravity = transform.Find ("PlanetGravity").gameObject;
		//planetText.text = "";

		radius = transform.localScale.x / 2;

		if (setGravityRange == true)
		{
			gravity.transform.localScale = new Vector3 (((radius + gravityRange) * 2)/transform.localScale.x , ((radius + gravityRange) * 2)/transform.localScale.x , 1);
		}
		else
		{
			gravityRange = gravity.transform.localScale.x * transform.localScale.x * 0.5f - radius;
		}


		planetText = GetComponentInChildren <TextMesh> ();
	}

	void Gm_RestartLevelEvent ()
	{
		if (gm.showGravity)
		{
			gravity.GetComponent <SpriteRenderer>().enabled = true;
		}
		else
		{
			gravity.GetComponent <SpriteRenderer> ().enabled = false;
		}
		planetText.text = "";
	}




	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnDestroy()
	{
		gm.RestartLevelEvent -= Gm_RestartLevelEvent;
	}

	public Vector3 GetPullVector(Vector3 targetPos)
	{
		float distanceToSurface = (targetPos - transform.position).magnitude - radius;
	
		float pullRatio = 1 - distanceToSurface / (gravityRange);
		float pullMagnitude = Mathf.Clamp ( pullRatio * maxGravity, minGravity,maxGravity);

		Vector3 normalizedPullVector = (transform.position - targetPos).normalized;
		Vector3 pullVector = normalizedPullVector * pullMagnitude;
		//planetText.text = pullMagnitude.ToString ();
		return pullVector;
		/*
		var localTargetPos = transform.InverseTransformPoint (targetPos);
		//Debug.Log (localTargetPos.y);
		float pullRatio = localTargetPos.y / platformGravity.transform.localScale.y;
		Vector3 actualPullVector = pullVector * (1 - pullRatio)*(1 - pullRatio) * maxPullForce;
		//platformText.text = "v: " + actualPullVector.ToString ();
		return actualPullVector ;
		*/
	}
}
