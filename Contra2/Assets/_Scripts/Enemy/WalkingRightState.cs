using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingRightState : IEnemyState
{
	private Enemy enemy;

	private float idleTimer;
	private float idleDuration;
	private float waitTime;
	private float speedX = 1, speedY = 0;
	public void Execute()
	{
		if (!enemy.BotSide)
		{
			enemy.ChangeState(new WalkingBotState());
		}else
		{
			if ((enemy.RightSide && enemy.transform.localScale.x > 0) || (enemy.LeftSide && enemy.transform.localScale.x < 0))
			{
				enemy.ChangeDirection();
			}
			enemy.transform.position += new Vector3(enemy.speed * enemy.transform.localScale.x * Time.deltaTime, 0, 0);
		}

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
		//	enemy.mAnimator.SetInt("Speed", speed);
		enemy.mAnimator.SetInteger("Speed", 2);
		enemy.mAnimator.SetFloat("SpeedX", 1);

	}
}

