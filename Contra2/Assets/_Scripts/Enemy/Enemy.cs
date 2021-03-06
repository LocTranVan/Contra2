﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	// Use this for initialization
	private Transform Target, Camera;
	public LayerMask background;
	public AudioClip shoot, Dead;
	private AudioSource audioSource;
	private Vector3 rightFoot, leftFoot, rightHead, leftHead;
	public float speed;
	public float offSet;
	public int numberOnceShoot;
	private int numberDirection;
	private IEnemyState currentState;
	public RaycastHit2D BotSide, LeftSide, RightSide;
	private float waitTime;
	private IEnumerator coroutine;
	private Vector2 currentDirection;
	private Collider2D collider;
	public bool canMove, canJump, canShoot;
	private float timeChange;
	void Start() {
		base.Start();
		if (canMove)
		{
			mAnimator.SetLayerWeight(1, 1);
			ChangeState(new WalkingRightState());
		}
		else
		{

			mAnimator.SetLayerWeight(1, 0);
			if (canShoot) {
				coroutine = Shoot(1f);
				StartCoroutine(coroutine);
			}
	
		}
		Target = GameObject.Find("Player").transform;
		Camera = GameObject.Find("Main Camera").transform;
		collider = gameObject.GetComponent<Collider2D>();
		audioSource = GetComponent<AudioSource>();
		//Debug.Log(rightFoot - transform.position);
		//	//Debug.Log("rightFoot" + rightFoot.position);
		//	Debug.Log("leftFoot" + leftFoot.position);
		//Debug.Log("rightHead" + rightHead.position);
		//Debug.Log("leftHead" + leftHead.position);

		rightFoot = transform.position + new Vector3(0.42f, 1f, 0);
		leftFoot = transform.position - new Vector3(0.42f, -1f, 0);
		leftHead = transform.position - new Vector3(0.58f, 1f, 0);
		rightHead = transform.position + new Vector3(0.58f, -1f, 0);

		//	IsDead = false;


	}
	public IEnumerator Shoot(float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(time);
			Shooting();
			int i = numberOnceShoot;
			while (i > 1)
			{
				yield return new WaitForSeconds(0.2f);
				Shooting();
				i--;
			}
		}
	}
	
	private void checkHit()
	{
		rightFoot = transform.position - new Vector3(0.42f, 0.8f, 0);
		leftFoot = transform.position + new Vector3(0.42f, -0.8f, 0);
		leftHead = transform.position - new Vector3(0.5f, 0.6f, 0);
		rightHead = transform.position + new Vector3(0.5f, -0.6f, 0);

		BotSide = Physics2D.Linecast(leftFoot, rightFoot, background);
		LeftSide = Physics2D.Linecast(leftHead, leftHead + new Vector3(0, 1f, 0), background);
		RightSide = Physics2D.Linecast(rightHead, rightHead + new Vector3(0, 1f, 0), background);


		//Debug.DrawLine(leftFoot, rightFoot, Color.red);
		//Debug.DrawLine(leftHead , leftHead + new Vector3(0, 1.2f, 0), Color.red);
		//Debug.DrawLine(rightHead, rightHead + new Vector3(0, 1f, 0), Color.red);

	}
	private void changeStateIdle()
	{
		Vector2 direction = Assets._Scripts.Helper.getIntance().GetDirection(transform.position - Target.position, 1.5f);
		mAnimator.SetFloat("Horizontal", Mathf.Abs(direction.x));
		mAnimator.SetFloat("Vertical", direction.y);

	}

	public bool equalVertical()
	{
		Vector2 direction = Assets._Scripts.Helper.getIntance().GetDirection(transform.position - Target.position, 1f);
		if (direction.y == 0)
			return true;
		return false;
	}
	public bool equalHorizontal()
	{
		Vector2 direction = Assets._Scripts.Helper.getIntance().GetDirection(transform.position - Target.position, 0.6f);
		if (direction.x == 0)
			return true;
		return false;
	}
	private void FixedUpdate()
	{

		if (!IsDead)
		{
			if (canMove)
			{
				checkHit();
				currentState.Execute();
			}
			else
			{
				ChangeDirection();
				changeStateIdle();
			}
			conditionDesTroy();
		}
		else
		{
			currentState.Execute();
		}
	}

	public void Nem()
	{
		GameObject bullets = Instantiate(bullet, arrPositionGun[0].position, Quaternion.identity);
		bullets.GetComponent<Bullet>().setTagShoot(gameObject.tag);
	}
	private void conditionDesTroy()
	{
		if(transform.position.y <=  (Target.position.y - 6f) || Mathf.Abs (transform.position.x - Camera.position.x) >= 10f)
		{
			Destroy(gameObject);
		}
	}

	public override void TakeDamage()
	{

		if (!IsDead)
		{
			health -= 1;
			if (health <= 0)
			{
				
				checkName();
				IsDead = true;
				if (!canMove)
				{
					mAnimator.SetFloat("Horizontal", 0);
					mAnimator.SetFloat("Vertical", 0);
					mAnimator.ResetTrigger("Jump");
				}
				
				ChangeState(new Dead());
				audioSource.PlayOneShot(Dead);
				if (health == 0)
					mAnimator.SetBool("Dead", true);
			}
		}
	}
	public void checkName()
	{
		//Debug.Log(gameObject.name);
		if (gameObject.name.Contains("Sandbag Sniper"))
		{
			GameManager.instance.changeResult(RefDefinition.SANDBAG_SNIPER, 1);
			GameManager.instance.changeResult(RefDefinition.SCORE, 3);
		}
		else if (gameObject.name.Contains("Outpost Unarmed Foot Soldier") || gameObject.name.Contains("Outpost Unarmed Foot SoldierPink"))
		{
			GameManager.instance.changeResult(RefDefinition.SOLDIER, 1);
			GameManager.instance.changeResult(RefDefinition.SCORE, 1);
		}
		else if (gameObject.name.Contains("Base Standing Sniper"))
		{
			GameManager.instance.changeResult(RefDefinition.SNIPER, 1);
			GameManager.instance.changeResult(RefDefinition.SCORE, 2);
		}
	}
	public void Boom()
	{
	//	Debug.Log("Booom");
		Destroy(gameObject);
	}
	public void SetTrigger()
	{
		collider.enabled = !collider.enabled;
	}
	public void setChange2()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	public override void ChangeDirection()
	{
		Vector2 direction = Assets._Scripts.Helper.getIntance().GetDirection(transform.position - Target.position, 1.5f);
		if (direction.x != 0)
		{
			transform.localScale = new Vector3(direction.x, 1, 1);
		}
	}
	
	public void ChangeState(IEnemyState newState)
	{
		if (currentState != null)
		{
			currentState.Exit();
		}

		currentState = newState;
		currentState.Enter(this);

	}
	// Update is called once per frame
	void Update () {
		
	}
}
