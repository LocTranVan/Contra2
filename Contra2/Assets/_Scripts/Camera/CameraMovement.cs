using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	public Transform player;
	public Transform minPosition;
	public float cameraSpeedX, cameraSpeedY;
	private float offset;
	private bool inclinedPlane = false;
	private bool milesStone = false;
	private float distance;

	private float yMax, yMix;
	private void Awake()
	{

		offset = player.position.y - minPosition.position.y;
		yMax = 2 * offset;

	}
	void Start () {
		
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		
	}

	// Update is called once per frame
	void Update () {

	}
	private void FixedUpdate()
	{
		MoveCamera();
	}
	//bug
	void MoveCamera()
	{
		Vector3 position = transform.position;
		if (position.x < player.transform.position.x)
		{
			position.x += cameraSpeedX * Time.fixedDeltaTime;
		}
		if (position.y < player.transform.position.y)
		{
			position.y += cameraSpeedY * Time.fixedDeltaTime;
		}


		float distance = player.position.y - minPosition.position.y;

		if (distance >= yMax)
		{
			position.y += cameraSpeedX * Time.fixedDeltaTime;

		}

		if(distance <= offset)
		{
			yMax = 2 * offset;
		}


		transform.position = position;
	}
	public void setMileStones()
	{
		yMax = yMax - offset / 2;
	
	}

}
