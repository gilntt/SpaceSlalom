using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class GM : MonoBehaviour {

	public enum GMState
	{
		LevelSelection,
		Wait,
		Gameplay
	}

	public bool autoStart = true;
	[HideInInspector]
	public GMState gmState = GMState.Wait;
	public LevelSelector levelSelector;
	public GameObject gameplayMenu;
	public event Action RestartLevelEvent = () => {};
	public event Action EndLevelEvent = () => {};
	public bool showGravity = false;
	public float gameSpeed= 2f;
	public GameLevel currentLevel;
	RocketCollider rocketCollider;
 	Rocket rocket;
	// Use this for initialization



	void Awake()
	{
		rocketCollider = FindObjectOfType <RocketCollider> ();
		rocketCollider.RocketCollision += RocketCollider_RocketCollision;
		LevelButton.LevelButtonPressedEvent += LevelButton_LevelButtonPressedEvent;
		TextButton.TextButtonPressedEvent += TextButton_TextButtonPressedEvent;
		rocket = FindObjectOfType <Rocket> ();
	}

	void TextButton_TextButtonPressedEvent (string obj)
	{
		if (obj == "Back")
			EndLevel ();
		else if (obj == "Restart")
			RestartLevel ();
	}

	void LevelButton_LevelButtonPressedEvent (int obj)
	{
		var chosenLevel = levelSelector.levelPrefabs [obj];
		ChangeToLevelGameplayState (chosenLevel);
	}

	void Start()
	{
		ChangeToLevelSelectionState ();
		if (autoStart)
		{
			LevelButton_LevelButtonPressedEvent (0);
		}
	}

	void ChangeToLevelGameplayState (GameObject chosenLevel)
	{
		levelSelector.gameObject.SetActive (false);
		var newLevel = Instantiate (chosenLevel, Vector3.zero, Quaternion.identity) as GameObject;
		currentLevel = newLevel.GetComponent <GameLevel> ();
		rocket.gameObject.SetActive (true);
		gameplayMenu.SetActive (true);
		gmState = GMState.Gameplay;
		RestartLevelEvent ();
	}

	void ChangeToLevelSelectionState()
	{
		if (currentLevel != null)
		{
			Destroy (currentLevel.gameObject);
		}
		levelSelector.gameObject.SetActive (true);
		rocket.gameObject.SetActive (false);
		gameplayMenu.SetActive (false);
		gmState = GMState.LevelSelection;
	}

	void RocketCollider_RocketCollision (GameObject obj)
	{
		if (obj.tag == "PlanetCollider")
		{
			RestartLevel ();
		}
			
		if (obj.tag == "Exit")
		{
			EndLevel ();
		}

		if (obj.tag == "Fuel")
		{
			obj.GetComponentInParent <Fuel>().SetDiamondToCollected ();
			if (IsAllFuelCollected ())
				currentLevel.exitGate.OpenGate ();
		}
	}

	void EndLevel()
	{
		EndLevelEvent ();
		ChangeToLevelSelectionState ();
	}

	bool IsAllFuelCollected()
	{
		var availableFuel = currentLevel.fuelInLevel.FindAll (x => x.available == true).ToList ().Count;

		//Debug.LogFormat ("availableFuel::{0}", availableFuel);
		if (availableFuel == 0) {
			return true;
		} else
			return false;
	}


	void RestartLevel()
	{
		Time.timeScale = gameSpeed;
		RestartLevelEvent ();

	}



	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R))
		{
			RestartLevel ();
		}	

		if (Input.GetKeyDown (KeyCode.Q))
		{
			ChangeToLevelSelectionState ();
		}
	}
}
