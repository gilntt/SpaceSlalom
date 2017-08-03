using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	public static event Action <int> LevelButtonPressedEvent = i => {};

	public void LevelButtonPressed()
	{
		var levelName = int.Parse (GetComponentInChildren<Text> ().text);
		LevelButtonPressedEvent (levelName);
	}
}
