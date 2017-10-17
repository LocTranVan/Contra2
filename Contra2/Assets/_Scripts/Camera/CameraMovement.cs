using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public float cameraSpeed;
	private float offset;
	private bool inclinedPlane = false;
	private void Awake()
	{
		offset = Mathf.Abs(transform.position.y - player.transform.position.y);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	private void FixedUpdate()
	{
		MoveCamera();
		//checkVision();

	}
	void checkVision()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 5f);	
		inclinedPlane = (hit.collider != null && hit.collider.tag == "Background") ? true : false;
		
	}
	void MoveCamera()
	{
		Vector3 position = transform.position;
		if (position.x < player.transform.position.x) { 
			position.x += cameraSpeed * Time.fixedDeltaTime;
		}	

		if((player.transform.position.y - position.y) >= offset)
		{
			position.y += cameraSpeed * Time.fixedDeltaTime;
		}


		transform.position = position;
	}
	
}
