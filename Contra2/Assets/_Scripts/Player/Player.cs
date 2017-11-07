using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
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
	
	int fullHealth;
	// Use this for initialization
	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerCollider = GetComponent<Collider2D>();
	}
	void Start () {
		base.Start();
		fullHealth = health;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Background")
		{
			jump = false;
			onGround = true;
		}
	}
	private void FixedUpdate()
	{
		if (!IsDead)
		{

			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");

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
		if ((Time.time - waitTime) >= 6f)
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
			shooting = true;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}
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
				mAnimator.SetTrigger("Dead");
				Physics2D.IgnoreLayerCollision(9, 12, true);
				if(lives > 0) { 
					invalid = true;
					waitTime = Time.time;
				}
			}
		}
	}
}
