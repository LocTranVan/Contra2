using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBotState : IEnemyState
{
	private Enemy enemy;

	private float walkingBotTimer;
	private float walkingBotDuration;
	//private float timeDelay;
	private float speedX = 0, speedY = -1;
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
				enemy.ChangeDirection();
				
			}
			else  if(!enemy.equalHorizontal())
			{
				enemy.ChangeState(new SlantBotState());
			}
			//enemy.setChange2();

			if (enemy.equalVertical() && !enemy.equalHorizontal())
			{
				enemy.ChangeState(new WalkingRightState());
			}
		}
		StartState();
		enemy.transform.position += new Vector3(0, -enemy.speed * Time.deltaTime, 0);


	}

	public void Enter(Enemy enemy)
	{
		walkingBotDuration = UnityEngine.Random.Range(1, 4);
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
		enemy.mAnimator.SetFloat("Horizontal", 0);
		enemy.mAnimator.SetFloat("Vertical", -1);

	}
}
