using UnityEngine;
using System.Collections;
using System;

public class InputListener : MonoBehaviour
{

	public event Action <Vector3> TouchScreen = v3 => {};
	public event Action <Vector3> ReleaseScreen = v3 => {};
	public event Action <Vector3> HoldScreen = v3 => {};

	//public Vector3 prevMousePosition;

	// Use this for initialization
	void Start ()
	{
		//prevMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update ()
	{

		#if UNITY_EDITOR
		UpdateMouseInput ();

		#else

		UpdateTouchInput();

		#endif
	}

	void UpdateMouseInput ()
	{
		
		//Vector3 mouseSpeed = Input.mousePosition - prevMousePosition;

		if ((Input.GetMouseButtonDown (0)) || Input.GetKeyDown (KeyCode.Space))
		{
			TouchScreen (Input.mousePosition);
		} else if ((Input.GetMouseButtonUp (0)) || Input.GetKeyUp (KeyCode.Space))
		{
			ReleaseScreen (Input.mousePosition);	
		} else if ((Input.GetMouseButton (0)) || Input.GetKey(KeyCode.Space))
		{
			HoldScreen (Input.mousePosition);	
		}

		//prevMousePosition = Input.mousePosition;
	}

	void UpdateTouchInput ()
	{
		

		if (Input.touches.Length == 1)
		{
			
			var touch = Input.GetTouch (0);
			Vector3 touchPosition = new Vector3 (touch.position.x, touch.position.y, 0);
			//Vector3 mouseSpeed = touchPosition - prevMousePosition;

			switch (touch.phase)
			{
				case TouchPhase.Began:
					TouchScreen (touchPosition);
				
					break;
				case TouchPhase.Moved:

					HoldScreen (touchPosition);	
					break;
				case TouchPhase.Ended:
					ReleaseScreen (touchPosition);

					break;
			}

			//prevMousePosition = touchPosition;

		}
	}



}

