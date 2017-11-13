using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	public Transform player;
	public Transform minPosition;
	public float cameraSpeedX, cameraSpeedY;
	//private float offset;
	private bool inclinedPlane = false;
	private bool milesStone = false;
	private float distance;
	private bool inSance;
	private Vector3 newTarget;
	private Vector3 currentPosition;
	private bool block;
	private float yMax, yMix;
	private Collider2D collider;
	public float offSet;
	private float startTime, journeyLength, speed;
	public float insanceDistance;

	private float TARGET_WIDTH = 7;
	private float TARGET_HEIGHT = 6;
	int PIXELS_TO_UNITS = 30;
	public bool Pause
	{
		get; set;
	}
	private void Awake()
	{
		/*
		float desiredRatio = TARGET_WIDTH / TARGET_HEIGHT;
		
		float currentRatio = (float)Screen.width / (float)Screen.height;
		Debug.Log(currentRatio);
		if (currentRatio >= desiredRatio)
			Camera.main.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS;
		else
		{
			float differenceInSize = desiredRatio / currentRatio;
			Camera.main.orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS * differenceInSize;
		}
		*/
	}
	void Start() {
		collider = GetComponent<Collider2D>();
		newTarget = transform.position;
		currentPosition = transform.position;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{

	}

	// Update is called once per frame
	void Update() {

	}
	private void FixedUpdate()
	{
		if(!Pause)
		MoveCamera();
	}
	//bug
	void MoveCamera()
	{
		Vector3 position = transform.position;

		if (!block)
		{
			if (position.y < player.transform.position.y - offSet)
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

		if ((transform.position.y < newTarget.y) || (transform.position.x < newTarget.x))
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			if (position.x < player.transform.position.x)
			{
				newTarget.x += cameraSpeedX * Time.fixedDeltaTime;
			}
			transform.position = Vector3.Lerp(currentPosition, newTarget, fracJourney);
			Debug.Log("bug here");
		}
		if (inSance)
		{
			transform.position += new Vector3(0, insanceDistance, 0);
			insanceDistance = -insanceDistance;
		}
	
	}
	public void setBlock(bool block)
	{
		this.block = block;
		collider.enabled = true;
	}
	public void setChandong()
	{
		inSance = !inSance;
		transform.position += new Vector3(0, 0.5f, 0);
	}
	public void setMileStones(Vector3 position)
	{
		currentPosition = transform.position;
		if (position.x == 0)
			newTarget = new Vector3(transform.position.x, position.y, transform.position.z);
		else
			newTarget = new Vector3(position.x, transform.position.y, transform.position.z);
	
		//newTarget = new Vector3(position.x, transform.position.y, transform.position.z);
		journeyLength = Vector3.Distance(currentPosition, newTarget);
		startTime = Time.time;
		speed = 2f;

		Debug.Log(currentPosition +"Here" + newTarget);
	}

}
