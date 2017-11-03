using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	// Use this for initialization
	private Transform Target;
	public LayerMask background;
	private Vector3 rightFoot, leftFoot, rightHead, leftHead;
	public float speed;
	public float offSet;
	private int numberDirection;
	private IEnemyState currentState;
	public RaycastHit2D BotSide, LeftSide, RightSide;
	private float waitTime;
	private IEnumerator coroutine;
	public bool canMove;
	void Start () {
		base.Start();
		if (canMove)
			ChangeState(new WalkingBotState());
		else { 

			ChangeState(new IdleTopState());
			coroutine = Shoot(1f);
			StartCoroutine(coroutine);
		}
		Target = GameObject.FindGameObjectWithTag("Player").transform;

		//Debug.Log(rightFoot - transform.position);
	//	//Debug.Log("rightFoot" + rightFoot.position);
	//	Debug.Log("leftFoot" + leftFoot.position);
		//Debug.Log("rightHead" + rightHead.position);
		//Debug.Log("leftHead" + leftHead.position);

		rightFoot= transform.position + new Vector3(0.42f, 1f, 0);
		leftFoot= transform.position - new Vector3(0.42f, -1f, 0);
		leftHead = transform.position - new Vector3(0.58f, 1f, 0);
		rightHead = transform.position + new Vector3(0.58f, -1f, 0);

		IsDead = false;


	}
	public IEnumerator Shoot(float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(time);
			Shooting();
		}
	}

	public bool equalHorizontal()
	{
		Vector3 disTaget = transform.position - Target.position;
		if (Mathf.Abs(disTaget.x) <= offSet)
		{
			return true;
		}
		return false;
		
		
	}
	public bool equalVertical()
	{
		Vector3 disTaget = transform.position - Target.position;
		if (Mathf.Abs(disTaget.y) <= offSet)
		{
			return true;
		}
		return false;


	}
	private void checkHit()
	{
		rightFoot = transform.position - new Vector3(0.42f, 0.8f, 0);
		leftFoot = transform.position + new Vector3(0.42f, -0.8f, 0);
		leftHead = transform.position - new Vector3(0.5f, 0.6f, 0);
		rightHead = transform.position + new Vector3(0.5f, -0.6f, 0);

		BotSide = Physics2D.Linecast(leftFoot, rightFoot, background);
		LeftSide = Physics2D.Linecast(leftHead , leftHead + new Vector3(0, 1f, 0), background);
		RightSide = Physics2D.Linecast(rightHead, rightHead + new Vector3(0, 1f, 0), background);

		//Debug.DrawLine(leftFoot, rightFoot, Color.red);
		//Debug.DrawLine(leftHead , leftHead + new Vector3(0, 1.2f, 0), Color.red);
		//Debug.DrawLine(rightHead, rightHead + new Vector3(0, 1f, 0), Color.red);

	}
	private void changeStateIdle()
	{
		Vector3 position = transform.position - Target.position;
		if (equalHorizontal())
		{
			if (position.y < 0)
				ChangeState(new IdleTopState());
			else
				ChangeState(new IdleBotState());
			return;
		}
		if (equalVertical())
		{
			ChangeState(new IdleRightState());
			return;
		}
		if (position.y > 0)
			ChangeState(new IdleBotSlantState());
		else
			ChangeState(new IdleTopSlantState());
				
	}
	
	private void FixedUpdate()
	{
		if (!IsDead)
		{
			if (canMove)
			{
				checkHit();
			}
			else
			{
				changeStateIdle();
				if (transform.position.x >= Target.position.x && facingRight == true || transform.position.x < Target.position.x && facingRight == false)
				{
					ChangeDirection();
				}
			}


			currentState.Execute();
			conditionDesTroy();
		}
	}
	
	private void conditionDesTroy()
	{
		if(IsDead || transform.position.y <= (Target.position.y - 6f))
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
				IsDead = true;
				Destroy(gameObject);
			}
		}
	}
	public void setChange2()
	{
		if (transform.position.x >= Target.position.x && facingRight == true || transform.position.x < Target.position.x && facingRight == false)
		{
			ChangeDirection();
		}
	}
	public override void ChangeDirection()
	{

		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
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
