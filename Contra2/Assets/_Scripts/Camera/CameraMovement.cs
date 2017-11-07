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

	private Vector3 newTarget;
	private Vector3 currentPosition;
	private bool block;
	private float yMax, yMix;
	private float startTime, journeyLength, speed;
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
		
		if (!block)
		{
			if (position.y < player.transform.position.y)
			{
				position.y += cameraSpeedY * Time.fixedDeltaTime;
			}
			if (position.x < player.transform.position.x)
			{
				position.x += cameraSpeedX * Time.fixedDeltaTime;
			}
			transform.position = position;
		}
		float distance = player.position.y - minPosition.position.y;

		if (block && (transform.position.y < newTarget.y))
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(currentPosition, newTarget, fracJourney);
			if (position.x < player.transform.position.x)
			{
				position.x += cameraSpeedX * Time.fixedDeltaTime;
			}
			//Debug.Log(Vector3.Lerp(currentPosition, newTarget, fracJourney));
			//position.y += cameraSpeedX * Time.fixedDeltaTime;
			//position.y += cameraSpeedY * Time.fixedDeltaTime;
		}
		/*
		else if (transform.position.y >= newTarget.y)
		{
			block = false;
		}
		*/




	}
	public void setBlock(bool block)
	{
		this.block = block;
	}
	public void setMileStones(Vector3 position)
	{
		currentPosition = transform.position;
		newTarget = new Vector3(transform.position.x, position.y, transform.position.z);
		journeyLength = Vector3.Distance(currentPosition, newTarget);
		startTime = Time.time;
		speed = 2;
		Debug.Log(currentPosition + " " + newTarget);

	}

}
