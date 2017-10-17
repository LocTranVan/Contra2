using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float speed;            // The speed that the player will move at.

	Vector3 movement;              // The vector to store the direction of the player's movement.
	Animator anim;
	Rigidbody2D playerRigidbody;
	bool jump = false;
	bool onGround = false;
	void Awake()
	{
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody2D>();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		jump = false;
		onGround = true;
		
	}
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Animating(h, v);
		Move(h, v);	
		if (h != 0)
			Turning(h, v);
	}
	void Move(float h, float v)
	{
		playerRigidbody.velocity = new Vector2(h * speed, playerRigidbody.velocity.y);
		if (jump && onGround)
		{
			playerRigidbody.AddForce(new Vector2(0, 400));
			onGround = false;
		}
	}
	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetFloat("SHorizontal", Mathf.Abs(h));
		anim.SetFloat("SVertical", v);

		anim.SetBool("Jump", jump);
	}
	
	
	
	void Update()
	{

		HandleInput();
	}
	void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}
	}
	void Turning(float h, float v)
	{	
			transform.localRotation = Quaternion.Euler(new Vector3(0, (h > 0) ? 0 : -180, 0));
	}
}
