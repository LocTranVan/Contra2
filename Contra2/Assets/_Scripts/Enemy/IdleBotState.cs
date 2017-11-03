using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBotState : IEnemyState
{
	private Enemy enemy;

	private float idleTimer;
	private float idleDuration;
	private float speedX = 0, speedY = 0;
	public void Execute()
	{
		//Idle(1);
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
		enemy.mAnimator.SetInteger("Speed", -1);
		enemy.mAnimator.SetFloat("SpeedX", 0);

	}
}

