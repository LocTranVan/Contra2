using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	//SetUp for Gun
	public Transform[] positionGun;
	//public Transform Father;
	public float waitTimeOnceFire;
	public int numberBulletOnceShoot;
	public GameObject bullet;
	public bool canTranform;
	public bool canRotate;
	public float jerkOnceShoot;
	//public float rangeShoot;

	private Transform Target;
	private IEnumerator coroutine;
	private bool desTroy = false;
	private Vector3 startTranform, endTranform;
	private float journeyTranform, startTimeShoot;
	private float speedBulletX, speedBulletY;
	private bool Shooting;
	//private bool Effect;
	private Vector3 DOWN = new Vector3(0, 0, 0), RIGHT = new Vector3(0, 0, 90), SLANTBOT = new Vector3(0, 0, 45), 
		TOP = new Vector3(0, 0, 180), SLANTTOP = new Vector3(0, 0, 135), LEFT = new Vector3(0, 0, 270), UPSIDETOP = new Vector3(0, 0, 230),
		UPSIDEBOT = new Vector3(0, 0, -45);
	// Use this for initialization
	void Start () {
		Target = GameObject.Find("Player").transform;
		//Debug.Log(Target.position);
		init();

		coroutine = Shoot(waitTimeOnceFire);
		StartCoroutine(coroutine);

	}
	private void init()
	{
		startTranform = transform.localScale;
		endTranform = new Vector3(startTranform.x, startTranform.y - jerkOnceShoot, startTranform.z);
		journeyTranform = Vector3.Distance(startTranform, endTranform);
	}
	private void FixedUpdate()
	{
		if(Target != null && !desTroy)
		{
			if (canTranform && Shooting)
				tranformGUn();
			if (canRotate)
				Rotate();
			ChangeDirection();
		}
	}
	private void tranformGUn()
	{
		float timeJourney = (Time.time - startTimeShoot) * 10;
		float distance = timeJourney / journeyTranform;

		transform.localScale = Vector3.Lerp(startTranform, endTranform, timeJourney);
		if(timeJourney >= 1)
		{
			transform.localScale = Vector3.Lerp(endTranform, startTranform, timeJourney);
			Shooting = false;
		}

	}
	private void Rotate()
	{
		Vector2 targetDir = Assets._Scripts.Helper.getIntance().GetDirection(transform.position - Target.position, 1f);
		Debug.Log(targetDir);
		if (targetDir.x == 1 && targetDir.y == 0)
		{
			transform.eulerAngles = RIGHT;
			return;
		}
		if (targetDir.x == -1 && targetDir.y == 0)
		{
			transform.eulerAngles = LEFT;
			return;
		}
		if (targetDir.x == -1)
		{
			transform.eulerAngles = (targetDir.y == -1) ? UPSIDEBOT : UPSIDETOP;
			return;
		}
		if (targetDir.x == 0)
		{
			transform.eulerAngles = (targetDir.y == 1) ? TOP : DOWN;
			return;
		}

		if (targetDir.x == 1)
		{
			transform.eulerAngles = (targetDir.y == -1) ? SLANTBOT: SLANTTOP ;
			return;
		}
	}
	private bool TargetInHorizontal(Vector3 targetDir)
	{
		if (Mathf.Abs(targetDir.y) <= 3f)
		{
			return true;
		}
		return false;
	}
	private bool TargetInVertical(Vector3 targetDir)
	{


		if (Mathf.Abs(targetDir.x) <= 3f)
		{
			return true;
		}
		return false;
	}
	private void ChangeDirection()
	{
		if (((Target.position.x >= transform.position.x) && transform.eulerAngles.z < 0) ||
				(Target.position.x < transform.position.x) && transform.eulerAngles.z > 0)

			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -transform.eulerAngles.z);


	}
	private IEnumerator Shoot(float waitTime)
	{
		while (!desTroy)
		{
			yield return new WaitForSeconds(waitTime);
			for(int i = 0; i < numberBulletOnceShoot; i++) { 
				foreach (Transform pGun in positionGun)
			{
				if (pGun != null)
				{
					GameObject bullets = Instantiate(bullet, pGun.position, Quaternion.identity);
					if (pGun.gameObject.GetComponent<SpriteRenderer>() != null)
					{
						bool Effect = false;
						StartCoroutine(EffectShoot(pGun.gameObject, Effect));

					}
					bullets.GetComponent<Bullet>().setTagShoot(gameObject.tag);
						//bullets.GetComponent<Bullet>().setSpeed(getForceGun(pGun.gameObject.name));
						Vector2 directionBullet = (canRotate) ? Assets._Scripts.Helper.getIntance().GetDirection(transform.position - Target.position, 1f) :
							Assets._Scripts.Helper.getIntance().GetDirection(transform.position - pGun.position, 0.2f);

					bullets.GetComponent<Bullet>().setSpeed(directionBullet);

						startTimeShoot = Time.time;
					Shooting = true;
				}
			}
				yield return new WaitForSeconds(0.2f);

			}
		}
	}
	private IEnumerator EffectShoot(GameObject gameObject, bool Effect)
	{
		while (!Effect)
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
			yield return new WaitForSeconds(0.1f);
			gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
			Effect = true;
		}

	}
	
	/**
	private Vector2 getForceGun(string name)
	{
		if (name.Contains("2") || name.Contains("3"))
		{
			 speed = (name.Contains("2")) ? new Vector2(-0.2f, -1) : new Vector2(0.2f, -1);
		
		}
		return speed;
	}
	*/
	// Update is called once per frame
	void Update () {
		
	}
}
