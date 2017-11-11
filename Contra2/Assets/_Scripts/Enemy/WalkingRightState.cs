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
	
		if (!enemy.BotSide && !enemy.equalVertical())
		{
			enemy.ChangeState(new WalkingBotState());
		}else
		{

				if ((enemy.RightSide && enemy.transform.localScale.x > 0) || (enemy.LeftSide && enemy.transform.localScale.x < 0))
				{
					//	enemy.setChange2();
					if(enemy.mRigidbody.gravityScale == 0)
						enemy.transform.localScale = new Vector3(-enemy.transform.localScale.x, enemy.transform.localScale.y, 0);
					else
				{
					enemy.ChangeState(new Jump());
				}
				}

			enemy.transform.position += new Vector3(enemy.speed * enemy.transform.localScale.x * Time.deltaTime, 0, 0);
		}
	
		StartState();
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
	}

	public void OnTriggerEnter(Collider2D other)
	{

	}

	public void StartState()
	{
		//	enemy.mAnimator.SetInt("Speed", speed);
		enemy.mAnimator.SetFloat("Horizontal", 1);
		enemy.mAnimator.SetFloat("Vertical", 0);

	}
}

