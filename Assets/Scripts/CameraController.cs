using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform rocket;

	public enum FollowType
	{
		FreeFollow, OnlyY, NoFollow

	}
	public FollowType followType = FollowType.OnlyY;
	public float maxSpeed = 5f;
	public float maxDistance = 4f;
	//Camera camera;


	// Use this for initialization
	void Awake () 
	{
		if (followType == FollowType.FreeFollow)
		{
			transform.position = rocket.position;
		}
		else if (followType == FollowType.OnlyY)
		{
			transform.position = new Vector3 (transform.position.x, rocket.position.y);
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (followType == FollowType.OnlyY) 
		{
			var yToRocket = rocket.position.y - transform.position.y;
			var speed = Mathf.Min (maxSpeed, (Mathf.Abs(yToRocket) / maxDistance) * maxSpeed);
			transform.position +=  new Vector3(0, Mathf.Sign(yToRocket)* speed,0);

		}
		else if (followType == FollowType.FreeFollow)
		{
			Vector3 vectorToRocket = rocket.position - transform.position;
			var speed = Mathf.Min (maxSpeed, (vectorToRocket.magnitude / maxDistance) * maxSpeed);
			transform.position += vectorToRocket.normalized * speed * Time.deltaTime;
		}

	}
}
