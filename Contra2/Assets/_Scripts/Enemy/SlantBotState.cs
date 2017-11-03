using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlantBotState : IEnemyState
{
	private Enemy enemy;
	private float walkingBotTimer;
	private float walkingBotDuration;
	private float idleTimer;
	private float idleDuration;
	public void Execute()
	{
		if (enemy.BotSide)
		{
			enemy.ChangeState(new WalkingRightState());
		}
		else
		{
			if (walkingBotTimer <= walkingBotDuration)
			{
				walkingBotTimer += Time.deltaTime * 2f;

				//enemy.mRigidbody.velocity = new Vector2(enemy.speed * enemy.transform.localScale.x, -enemy.speed);
				float speed = enemy.speed * Time.deltaTime;
				enemy.transform.position += new Vector3(speed * enemy.transform.localScale.x, -speed, 0);
			}
			else
			{
				enemy.ChangeState(new WalkingBotState());
			}
			enemy.setChange2();

		}

		StartState();
		if (enemy.equalVertical())
		{
			enemy.ChangeState(new WalkingRightState());
		}
		if (enemy.equalHorizontal())
		{
			enemy.ChangeState(new WalkingBotState());
		}
	}

	public void Enter(Enemy enemy)
	{
		walkingBotDuration = UnityEngine.Random.Range(1, 5);;
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
		enemy.mAnimator.SetInteger("Speed", -3);
		enemy.mAnimator.SetFloat("SpeedX", 1);

	}
}

