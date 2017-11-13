using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	//SetUp for Gun
	public Transform[] positionGun;
	//public Transform Father;
	public float waitTimeOnceFire, waitTimeOnceBullet;
	public int numberBulletOnceShoot;
	public GameObject bullet;
	public bool canTranform;
	public bool canRotate;
	public bool canChangeImage;
	public float jerkOnceShoot;
	public Sprite[] sprite;
	//public float rangeShoot;
	public AudioClip shoot;
	private AudioSource audioSource;

	private Transform Target;
	private IEnumerator coroutine;
	private bool desTroy = false;
	private Vector3 startTranform, endTranform;
	private float journeyTranform, startTimeShoot;
	private float speedBulletX, speedBulletY;
	private bool Shooting;
	private SpriteRenderer mSpriteRenderer;
	//private bool Effect;
	private Vector3 DOWN = new Vector3(0, 0, 0), RIGHT = new Vector3(0, 0, 90), SLANTBOT = new Vector3(0, 0, 45),
		TOP = new Vector3(0, 0, 180), SLANTTOP = new Vector3(0, 0, 135), LEFT = new Vector3(0, 0, 270);
	// Use this for initialization
	void Start () {
		Target = GameObject.Find("Player").transform;
		audioSource = GetComponent<AudioSource>();
		mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		init();

		coroutine = Shoot(waitTimeOnceFire);
		StartCoroutine(coroutine);


	}
	private void init()
	{
		startTranform = transform.localScale;
		endTranform = new Vector3(startTranform.x, startTranform.y - jerkOnceShoot, startTranform.z);
		journeyTranform = Vector3.Distance(startTranform, endTranform);
	
		if (canChangeImage)
		{
			StartCoroutine(ChangeSprite(0.5f));
		}
	}
	private IEnumerator ChangeSprite(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		mSpriteRenderer.sprite = sprite[0];

		yield return new WaitForSeconds(waitTime);
		mSpriteRenderer.sprite = sprite[1];
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
						if (audioSource != null)
							audioSource.PlayOneShot(shoot);
					startTimeShoot = Time.time;
					Shooting = true;
				}
			}
				yield return new WaitForSeconds(waitTimeOnceBullet);

			}
		}
	}
	public void Nem()
	{
		for (int i = 0; i < numberBulletOnceShoot; i++)
		{
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

					Shooting = true;
				}
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
	

	// Update is called once per frame
	void Update () {
		
	}
}
