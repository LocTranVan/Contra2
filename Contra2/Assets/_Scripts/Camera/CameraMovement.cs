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
	private void Awake()
	{


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
