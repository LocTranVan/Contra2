using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : IEnemyState
{
	private Enemy enemy;

	private float idleTimer;
	private float idleDuration;
	private float speedX = 0, speedY = 0;

	public void Execute()
	{
		if(enemy.mRigidbody.gravityScale != 0) 
		enemy.transform.position += new Vector3(-enemy.transform.localScale.x * enemy.speed * Time.deltaTime, enemy.speed * Time.deltaTime * 3, 0);
		StartState();
	}

	public void Enter(Enemy enemy)
	{
		//idleDuration = UnityEngine.Random.Range(1, 10);
		this.enemy = enemy;
	}

	public void Exit()
	{

	}

	public void OnTriggerEnter(Collider2D other)
	{

	}

	public void StartState()
	{
		//enemy.mAnimator.SetInteger("Speed", 0);
		//enemy.mAnimator.SetFloat("SpeedX", 0);
		//enemy.mAnimator.SetTrigger("Dead");
		enemy.mAnimator.SetBool("Dead", true);
	}
}

