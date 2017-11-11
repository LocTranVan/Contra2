using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	public float waitTimeEnable, leftSide;
	public LayerMask Camera;
	public float speed, speedY;
	public GameObject DoorOpen;
	public float offSet;
	private float journey;
	private Transform MainCamera;
	public bool canSpaw;
	public bool whatever;
	private AudioSource audioSource;
	public AudioClip ShowBoos;
	// Use this for initialization
	void Start() {
		MainCamera = GameObject.Find("Main Camera").transform;
		audioSource = GetComponent<AudioSource>();
		if (canSpaw)
		{
			StartCoroutine(EnableObject(waitTimeEnable));
			MainCamera.gameObject.GetComponent<CameraMovement>().setMileStones(new Vector3(transform.position.x - 6f, 0, 0));
		}else
		{
			MainCamera.gameObject.GetComponent<CameraMovement>().setMileStones(new Vector3(0,transform.position.y - 2f, 0));
		
		}
		if(whatever)
			MainCamera.gameObject.GetComponent<CameraMovement>().setMileStones(new Vector3(transform.position.x, 0, 0));
		MainCamera.gameObject.GetComponent<CameraMovement>().setBlock(true);
		audioSource.PlayOneShot(ShowBoos);

	}
	private IEnumerator EnableObject(float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			if (!DoorOpen.active)
			{
				DoorOpen.SetActive(true);
				//	DoorOpen.GetComponent<SpawTheoNhuCau>().LoopCoroutine();
			}
			else
			{
				DoorOpen.SetActive(false);
			}
		}

	}
	private void FixedUpdate()
	{
		Vector3 position = transform.position;
		/*
		if((position.x <= MainCamera.transform.position.x - 6f) || (position.x >= MainCamera.transform.position.x + 6f))
		{
			speed = -speed;
		}
		*/

		if (journey <= -offSet || journey >= offSet)
		{
			speedY = -speedY;
			journey = 0;
		}
		checkHit();
		journey += Time.deltaTime * speedY;
		position.x += Time.deltaTime * speed;

		position.y += journey;


		transform.position = position;
	}
	
	private void checkHit()
	{
		Vector3 position = transform.position - new Vector3(2, 0, 0);
		RaycastHit2D LefSide = Physics2D.Linecast(position, position - new Vector3(leftSide, 0, 0), Camera);
		RaycastHit2D RightSide = Physics2D.Linecast(position, position + new Vector3(leftSide, 0, 0), Camera);

		Debug.DrawLine(position, position - new Vector3(leftSide, 0, 0), Color.red);
	//	Debug.DrawLine(transform.position, transform.position + new Vector3(leftSide, 0, 0), Color.red);


		if (LefSide && speed < 0 || RightSide && speed > 0)
			speed = -speed;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
