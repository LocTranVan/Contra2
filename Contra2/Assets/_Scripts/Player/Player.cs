using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	private bool shooting = false;
	public GameObject camera;
	Vector3 movement;              // The vector to store the direction of the player's movement.
	Rigidbody2D playerRigidbody;

	bool jump = false;
	bool onGround = false;
	// Use this for initialization
	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody2D>();
	}
	void Start () {
		base.Start();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Background")
		{
			jump = false;
			onGround = true;
		}
	}
	public override void OnTriggerEnter2D(Collider2D collision)
	{
		//	collision.gameObject.SetActive(false);
		camera.GetComponent<CameraMovement>().setMileStones();
	}
	private void FixedUpdate()
	{
		if (!IsDead)
		{
			if (shooting)
			{
				Shooting();
				mAnimator.SetTrigger("Shoot");
				shooting = false;
			}
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

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
		}
	}
	void Animating(float h, float v)
	{
		mAnimator.SetFloat("SHorizontal", Mathf.Abs(h));
		mAnimator.SetFloat("SVertical", v);
		mAnimator.SetBool("Jump", jump);
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
		if (!IsDead)
		{
			health -= 1;
			if (health == 0)
				IsDead = true;
		}
		else
		{
			mAnimator.SetTrigger("Dead");
		}
	}
}
