using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TextButton : MonoBehaviour {

	public static event Action <String> TextButtonPressedEvent = i => {};

	public void TextButtonPressed()
	{
		TextButtonPressedEvent (GetComponentInChildren<Text> ().text);
	}
}
