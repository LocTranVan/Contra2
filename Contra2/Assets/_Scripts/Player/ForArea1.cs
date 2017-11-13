using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForArea1 : MonoBehaviour {

	private GameObject mainCamera, Player;
	public Transform Place1, Place2, Place3;

	private AudioSource audioSource;
	public AudioClip plane;
	public GameObject easyTouch, SpawEnemy;
	private Transform tranformPlayer;
	public Sprite idleRun, idleJump;
	private float journey, startTime, journeyCamera;
	private Vector3 startPosition, endPosition, startPCamera;
	public float speed;

	private bool startLerpCamera;
	private float timeRun;
	private SpriteRenderer spriteRenderer;
	private Animator anim;
	private bool pause = true;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		mainCamera = GameObject.Find("Main Camera");
		Player.GetComponent<Player>().IsDead = true;
		mainCamera.GetComponent<CameraMovement>().Pause = true;
		anim = Player.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		anim.enabled = false;

		easyTouch.SetActive(false);
		SpawEnemy.SetActive(false);
		spriteRenderer = Player.GetComponent<SpriteRenderer>();

		spriteRenderer.sprite = null;

		tranformPlayer = Player.GetComponent<Transform>();
		startPosition = tranformPlayer.position;
		endPosition = Place1.position;
		journey = Vector3.Distance(startPosition, endPosition);

		journeyCamera = Vector3.Distance(mainCamera.transform.position, Place3.position);
		startPCamera = mainCamera.transform.position;

		timeRun = Time.time;

		audioSource.PlayOneShot(plane);
	}
	private void FixedUpdate()
	{

		if (!pause)
		{
			if ((tranformPlayer.localScale.x < 0) && (timeRun < Time.time - 1.6f))
			{
				tranformPlayer.localScale = new Vector3(-tranformPlayer.localScale.x, tranformPlayer.localScale.y, tranformPlayer.localScale.z);
			}

			float timeJourney = (Time.time - startTime) * Time.deltaTime;
			float distance = (timeJourney) * speed / journey;

			tranformPlayer.position = Vector3.Lerp(startPosition, endPosition, distance);

			if (tranformPlayer.position == Place1.position)
			{
				startPosition = endPosition;
				endPosition = Place2.position;
				startTime = Time.time;
				journey = Vector3.Distance(startPosition, endPosition);
				startLerpCamera = true;



				mainCamera.GetComponent<CameraMovement>().setMileStones(new Vector3(0, Place3.position.y, 0));
			}

			if (startLerpCamera)
			{
				spriteRenderer.sprite = idleJump;
				float timeCamera = (Time.time - startTime) * Time.deltaTime;
				float distanceCamera = (timeCamera) * speed * 1.5f / journeyCamera;
				mainCamera.transform.position = Vector3.Lerp(startPCamera, new Vector3(startPCamera.x, Place3.position.y, startPCamera.z), distanceCamera);
			}
			if (mainCamera.transform.position.y == Place3.position.y)
				doSomething();
		}
		else if(timeRun < Time.time - 1.2f)
		{
			spriteRenderer.sprite = idleRun;
			pause = false;
			startTime = Time.time;
		}
	}
	private void doSomething()
	{
		easyTouch.SetActive(true);
		SpawEnemy.SetActive(true);
		Player.GetComponent<Player>().IsDead = false;
		anim.enabled = true;
		mainCamera.GetComponent<CameraMovement>().Pause = false;
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
