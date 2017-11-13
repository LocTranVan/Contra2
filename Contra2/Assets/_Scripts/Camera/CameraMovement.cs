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
	private float CAMERA_SIZE = 5;
	int PIXELS_TO_UNITS = 30;
	public bool Pause
	{
		get; set;
	}

	void Start() {
		float targetaspect = 7 / 6;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;
		// 6*(float)Screen.width / (7*(float)Screen.height)
		Vector3 scale = transform.localScale;
		scale.x *= 6 * (float)Screen.width / (7 * (float)Screen.height);
		//transform.localScale = scale;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();
		

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
		//	rect.height = scaleheight;
			rect.height = 6 * (float)Screen.width / (7 * (float)Screen.height);
			rect.x = 0;
			rect.y = (1.0f - 6 * (float)Screen.width / (7 * (float)Screen.height)) / 2.0f;

			camera.rect = rect;

		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			// (7*(float)Screen.height)/6 * (float)Screen.width 
			Rect rect = camera.rect;

			//rect.width = scalewidth;
			rect.width = (7 * (float)Screen.height) / 6 * (float)Screen.width;
			rect.height = 1.0f;
			//	rect.x = (1.0f - scalewidth) /2;
			rect.x = (1.0f - (7 * (float)Screen.height) / 6 * (float)Screen.width) / 2;
			rect.y = 0;

			camera.rect = rect;
		}




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

	//	Debug.Log(currentPosition +"Here" + newTarget);
	}

}
