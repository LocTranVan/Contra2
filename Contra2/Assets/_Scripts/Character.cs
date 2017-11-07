using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	[SerializeField]
	protected float movementSpeedX, movementSpeedY, jumpSpeed;

	[SerializeField]
	protected GameObject bullet;
	[SerializeField]
	private string defaultNameGun;

	[SerializeField]
	protected Transform[] arrPositionGun;
	[SerializeField]
	protected Transform bulletSpawn;

	[SerializeField]
	private List<string> damageSources;


	protected bool facingRight;
	protected bool invalid
	{
		get;
		set;
	}

	public Animator mAnimator
	{
		get; private set;
	}
	public Rigidbody2D mRigidbody
	{
		get; private set;
	}
	public bool Attack
	{
		get; set;
	}

	[SerializeField]
	protected int health;
	public  bool IsDead
	{
		get;
		set;
	}
	public bool TakingDamage
	{
		get; set;
	}
	public abstract void TakeDamage();

	// Use this for initialization
	public virtual void Start()
	{
		mAnimator = GetComponent<Animator>();
		mRigidbody = GetComponent<Rigidbody2D>();
		facingRight = true;
	//	playerHealth.Initialize();
	}

	//public abstract IEnumerator TakeDamage();
	//public abstract void Death();
	public virtual void ChangeDirection()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
	}
	public virtual void Move(float hInput, float vInput)
	{
		hInput = hInput * Time.deltaTime;
		vInput = vInput * Time.deltaTime;
		//mRigidbody.velocity = new Vector2 (hInput * movementSpeedX, (jumpSpeed == 0) ? vInput * movementSpeedY : mRigidbody.velocity.y);
		transform.position += new Vector3(hInput * movementSpeedX, (jumpSpeed == 0) ? vInput * movementSpeedY : 0);
	}

	public virtual void Jump()
	{
		mRigidbody.AddForce(new Vector2(0, jumpSpeed));
	}
	
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log(other.tag);
		if (damageSources.Contains(other.tag) && !invalid)
		{
			TakeDamage();
		}

		if (other.tag == "Milestones" && gameObject.tag == "Player")
		{
			Debug.Log(other.transform.position);
			GameObject camera = GameObject.Find("Main Camera");
			camera.GetComponent<CameraMovement>().setMileStones(other.transform.position);
			camera.GetComponent<CameraMovement>().setBlock(true);
		}
			
	}
	public virtual void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Milestones" && gameObject.tag == "Player")
		{
			Debug.Log(other.transform.position);
			GameObject camera = GameObject.Find("Main Camera");
			camera.GetComponent<CameraMovement>().setMileStones(other.transform.position);
			camera.GetComponent<CameraMovement>().setBlock(false);
		}
	}
	public virtual void Shooting()
	{
		Vector2 positionGun = Vector2.zero;
		float direc = transform.localScale.x;
		float speedX = 0, speedY = 0;

		if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Top"))
		{
			positionGun = arrPositionGun[0].position;
			speedY = 1;
		}else  if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Bot"))
		{
			positionGun = arrPositionGun[1].position;
			speedY = -1;
		}
		else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("SlantTop"))
		{
			positionGun = arrPositionGun[2].position;
			speedX = direc;
			speedY = 1;
		}
		else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("SlantBot"))
		{
			positionGun = arrPositionGun[3].position;
			speedX = direc;
			speedY = -1;
		}
		else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Right"))
		{
			positionGun = arrPositionGun[4].position;
			speedX = direc;
		}
		else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
		{
			positionGun = transform.position;
			speedX = direc;
		}
		else if (mAnimator.GetCurrentAnimatorStateInfo(0).IsTag("LayingDown"))
		{
			positionGun = arrPositionGun[5].position;
			speedX = direc;
		}


		GameObject bullets = Instantiate(bullet, positionGun, Quaternion.identity);
		bullets.GetComponent<Bullet>().setTagShoot(gameObject.tag);
	//	bullets.layer = gameObject.layer;
		bullets.GetComponent<Bullet>().setSpeed(new Vector2(speedX, speedY));
	}
	// Update is called once per frame
	void Update () {
		
	}
}
