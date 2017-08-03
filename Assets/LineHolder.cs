using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LineHolder : MonoBehaviour {


	GM gm;
	public GameObject linePrefab;
	public int maxLines = 3;
	RocketLine currentLine;
	public List<RocketLine> rocketLines = new List<RocketLine>();
	Rocket rocket;
	public int EngineLineMaxLength = 50;
	public  int EngineLineLengthIncreaseSpeed = 2;

	void Awake()
	{
		gm = FindObjectOfType <GM> ();
		gm.RestartLevelEvent += Gm_RestartLevelEvent;
		gm.EndLevelEvent += Gm_EndLevelEvent;
	}

	void Gm_EndLevelEvent ()
	{
		while (rocketLines.Count > 0)
		{
			var lineToRemove = rocketLines [0];
			rocketLines.Remove (lineToRemove);
			Destroy (lineToRemove.gameObject);
		}
	}

	void Gm_RestartLevelEvent ()
	{
		rocket = FindObjectOfType <Rocket> ();
		StartNewLine ();
	}

	void StartNewLine()
	{
		while (rocketLines.Count >= maxLines)
		{
			var lineToRemove = rocketLines [0];
			rocketLines.Remove (lineToRemove);
			Destroy (lineToRemove.gameObject);
		}

		var newLineGameObject = Instantiate (linePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		var newLine = newLineGameObject.GetComponent <RocketLine> ();
		currentLine = newLine;
		rocketLines.Add (newLine);

	}

	void OnDestroy()
	{
		gm.RestartLevelEvent -= Gm_RestartLevelEvent;
		gm.EndLevelEvent -= Gm_EndLevelEvent;
	}

	void FixedUpdate()
	{
		if (gm.gmState != GM.GMState.Gameplay)
			return;

		if (rocket.engineOn)
		{
			if (currentLine.maxLength < EngineLineMaxLength)
			{
				currentLine.maxLength+= EngineLineLengthIncreaseSpeed;
			}
		}
		else
		{
			if (currentLine.maxLength > 10)
			{
				currentLine.maxLength--;
			}
		}


		if (gm.gmState == GM.GMState.Gameplay)
			currentLine.UpdateLine (rocket.trailStart.position);
	}
}
