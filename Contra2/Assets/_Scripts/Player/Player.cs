using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	private static Player instance;
	public AudioClip shoot, Dead, item;
	private AudioSource audioSource;
	public static Player Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<Player>();
			}
			return instance;
		}
	}
	private bool shooting = false;
	public GameObject camera;
	Vector3 movement;              // The vector to store the direction of the player's movement.
	Rigidbody2D playerRigidbody;
	Collider2D playerCollider;
	private SpriteRenderer spriteRenderer;
	bool jump = false;
	bool onGround = false;
	float waitTime;
	[SerializeField]
	private int lives = 3;
	public bool liveForever;
	int fullHealth;
	private int Score;
	private ETCJoystick eTCJoystick;
	private bool blockgravity;
	// Use this for initialization
	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerCollider = GetComponent<Collider2D>();

		eTCJoystick = FindObjectOfType<ETCJoystick>();
		if (playerRigidbody.gravityScale == 0)
		{
			blockgravity = true;
		}
	}
	void Start () {
		base.Start();
		audioSource = GetComponent<AudioSource>();
		fullHealth = health;

		init();
	}
	private void init()
	{
		lives = GameManager.instance.lives;
		bullet = GameManager.instance.Bullet;
		liveForever = GameManager.instance.immortal;


	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!blockgravity && collision.gameObject.tag == "Background")
		{
			jump = false;
			onGround = true;
			mRigidbody.gravityScale = 0;
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (!blockgravity && collision.gameObject.tag == "Background")
		{
			mRigidbody.gravityScale = 1;
		}
	}
	private void FixedUpdate()
	{
		if (!IsDead)
		{

			//float h = Input.GetAxisRaw("Horizontal");
			//float v = Input.GetAxisRaw("Vertical");
			float h = ETCInput.GetAxis("Horizontal");
			float v = ETCInput.GetAxis("Vertical");

			Animating(h, v);
			Move(h, v);
			if (jump && onGround)
			{
				Jump();
				onGround = !onGround;
			}

			if (h > 0 && !facingRight || h < 0 && facingRight)
			{
				ChangeDirection();
			}
			
			if (invalid)
				IndicateImmortal();
			
		}
		
		else if (invalid && (Time.time - waitTime) >= 2f)
		{
			reSetGame();
			
		}

	}
	private void IndicateImmortal()
	{
		if ((Time.time - waitTime) >= 6f && !liveForever)
		{
			invalid = false;
			Physics2D.IgnoreLayerCollision(9, 12, false);
			spriteRenderer.enabled = true;
			return;
		}
			spriteRenderer.enabled = !spriteRenderer.enabled;
	}
	private void reSetGame()
	{
		//invalid = false;
		IsDead = false;
		health = fullHealth;
		bullet = defaultBullets;
		if (mAnimator.isInitialized)
				mAnimator.Rebind();
		
	}
	void Animating(float h, float v)
	{
		mAnimator.SetFloat("SHorizontal", Mathf.Abs(h));
		mAnimator.SetFloat("SVertical", v);
		mAnimator.SetBool("Jump", jump);
		if (shooting)
		{
			Shooting();
			mAnimator.SetTrigger("Shoot");
			shooting = false;
		}
	}

	void Update()
	{
		HandleInput();
	}
	void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Shoot();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			JumpUp();

		}
	}
	public void Shoot()
	{
		mAnimator.ResetTrigger("Shoot");
		audioSource.PlayOneShot(shoot);
		shooting = true;
	}
	public void JumpUp()
	{
		jump = true;
	}
	
	public void setBullet(GameObject bullet)
	{
		audioSource.PlayOneShot(item);
		this.bullet = bullet;

	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Milestones")
		{
			GameObject camera = GameObject.Find("Main Camera");
			//camera.GetComponent<CameraMovement>().setMileStones(other.transform.position);
		//	camera.GetComponent<CameraMovement>().setBlock(false);
		}

		if (other.tag == "UnderWater" )
		{

			mAnimator.SetLayerWeight(1, 0);
		}
	}
	public void setSocre(int score)
	{
		Score = Score + score;
		Debug.Log(Score);
	}
	public override void TakeDamage()
	{
		if (!IsDead )
		{
			health -= 1;
			if (health == 0)
			{
				IsDead = true;
				lives--;
				GameManager.instance.lives = lives;

				mAnimator.ResetTrigger("Jump");
				mAnimator.SetTrigger("Dead");
				audioSource.PlayOneShot(Dead);
				Physics2D.IgnoreLayerCollision(9, 12, true);
				if(lives > 0) { 
					invalid = true;
					waitTime = Time.time;
				}
			}
		}
	}
}
