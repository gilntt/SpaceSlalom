using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
public class RocketLine : MonoBehaviour {


	LineRenderer lr;
	List<Vector3> positions = new List<Vector3>();
	public int maxLength = 200;
	//Vector3[] positionArray = new Vector3[];

	void OnEnable()
	{
		lr = GetComponent <LineRenderer> ();

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void UpdateLine (Vector3 pos) 
	{

		while (positions.Count > maxLength)
		{
			positions.Remove (positions [0]);
		}

		positions.Add (pos);
		Vector3[] positionArray = positions.ToArray ();

		//Debug.Log (positions);
		lr.SetVertexCount (positionArray.Length);
		lr.SetPositions (positionArray);
	}
}
