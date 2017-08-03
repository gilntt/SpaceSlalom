using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour {


	public List<GameObject> levelPrefabs = new List<GameObject> ();

	public GameObject buttonPrefab;
	public GameObject canvas;
	public float buttonFirstX = 75f;
	public float buttonFirstY = -75f;
	public float buttonWidth = 200f;
	public float buttonHeight = 200f;
	public float buttonWidthMargin = 150f;
	public float buttonHeightMargin = 150f;
	public int maxHorizontalButtons = 5;
	//public float button


	void Start () {
		CreateLevelButtons ();
	}

	void CreateLevelButtons()
	{
		int currentRow = 0;
		int currentColumn = 0;
		for (int i = 0; i < levelPrefabs.Count; i++)
		{
			var newButtonX = buttonFirstX + currentColumn * buttonWidthMargin;
			var newButtonY = buttonFirstY - currentRow * buttonHeightMargin;
			var newButton = Instantiate (buttonPrefab, Vector3.zero,Quaternion.identity) as GameObject;
			newButton.transform.SetParent (canvas.transform);
			var	newButtonRectTransform = newButton.GetComponent <RectTransform> ();
			//newButtonRectTransform.anchoredPosition= new Vector3(newButtonX,newButtonY);
			newButtonRectTransform.anchoredPosition = new Vector2 (newButtonX, newButtonY);
			newButtonRectTransform.sizeDelta = new Vector2 (buttonWidth, buttonHeight);
			newButton.GetComponentInChildren <Text>().text = i.ToString ();
			if (currentColumn<maxHorizontalButtons-1)
			{
				currentColumn++;
			}
			else
			{
				currentColumn = 0;
				currentRow++;
			}
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
