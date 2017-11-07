using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IEnemyState
{
	private Enemy enemy;

	private float idleTimer;
	private float idleDuration;
	private float waitTime;
	private float speedX = 1, speedY = 0;
	public void Execute()
	{


		StartState();
		enemy.transform.position += new Vector3(enemy.transform.localScale.x * enemy.speed * Time.deltaTime, enemy.speed * Time.deltaTime * 5, 0);
		if (enemy.BotSide)
		{
			enemy.ChangeState(new WalkingRightState());
		}
	}

	public void Enter(Enemy enemy)
	{
		//idleDuration = UnityEngine.Random.Range(1, 10);
		this.enemy = enemy;
		
		//	StartState();

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
		enemy.mAnimator.SetTrigger("Jump");
		enemy.mAnimator.SetFloat("Horizontal", 0);
		enemy.mAnimator.SetFloat("Vertical", 0);

	}
}