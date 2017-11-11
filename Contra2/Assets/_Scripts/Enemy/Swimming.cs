using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimming : IEnemyState
{
	private Enemy enemy;

	private float idleTimer;
	private float idleDuration;
	private float waitTime;
	private float speedX = 1, speedY = 0;
	public void Execute()
	{


		StartState();
		if (enemy.LeftSide || enemy.RightSide)
		{
	
		enemy.ChangeState(new Jump());
		}
		enemy.transform.position += new Vector3(enemy.speed * enemy.transform.localScale.x * Time.deltaTime, 0, 0);
	}

	public void Enter(Enemy enemy)
	{
		//idleDuration = UnityEngine.Random.Range(1, 10);
		this.enemy = enemy;

		//	StartState();

	}

	public void Exit()
	{
		enemy.mAnimator.SetFloat("Horizontal", 0);
		enemy.mAnimator.SetFloat("Vertical", 0);
		enemy.mAnimator.ResetTrigger("Jump");
		enemy.mAnimator.SetBool("Swimming", false);
	}

	public void OnTriggerEnter(Collider2D other)
	{

	}

	public void StartState()
	{
		//	enemy.mAnimator.SetInt("Speed", speed);
		enemy.mAnimator.SetFloat("Horizontal", 0);
		enemy.mAnimator.SetFloat("Vertical", 0);
		enemy.mAnimator.SetBool("Swimming", true);
			
	}
}