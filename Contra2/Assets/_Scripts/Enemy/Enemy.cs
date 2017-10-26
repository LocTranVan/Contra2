using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {
	public Transform target;
	public bool canShoot = false;
	private IEnemyState currentState;
	public float offset;
	public LayerMask background;
	private const int IDLETOP = 1, IDLEBOT = -1, IDLERIGHT = 2, IDLESLANTTOP = 3, IDLESLANTBOT = -3;
	private int direction = -1;

	private int[] slantTOP = { IDLETOP, IDLERIGHT, IDLESLANTTOP }, slantBot = { IDLEBOT, IDLERIGHT, IDLESLANTBOT},
		slantTOP2 = { IDLETOP, -IDLERIGHT, -IDLESLANTTOP }, slantBot2 = { IDLEBOT, -IDLERIGHT, -IDLESLANTBOT };

	private IEnumerator coroutine, coroutine2;
	//private float speedx;
	private float speedX = 0, speedY = 0;
	private bool random = false, hit = false;
	private bool moveTop = true, moveBot = true, moveRight = true, moveLeft = true, changeDirect = true;

	public void Start () {

		base.Start();
		if (movementSpeedX == 0 && movementSpeedY == 0)
		{
			ChangeState(new IdleBotState());
		}else
		{
			ChangeState(new IdleBotState());
			coroutine2 = RandomeDirection(1.0f);
			StartCoroutine(coroutine2);
		}
		
		if (canShoot)
		{
			coroutine = Shoot(2.0f);
			StartCoroutine(coroutine);	
		}
		else
		{
		
		}
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	private void FixedUpdate()
	{
		float flip = transform.localScale.x;
		Vector2 distanceTarget = target.position - transform.position;

		if (movementSpeedX == 0 && movementSpeedY == 0)
		{
			CheckTarget(distanceTarget);
		}else if(movementSpeedX != 0 && movementSpeedY != 0)
		{
			checkPath();
			Move(movementSpeedX * speedX, movementSpeedY * speedY);
			if (!facingRight)
			{
				speedX = -speedX;
			}

		}

		ChangeState(getState(movementSpeedY));

		currentState.Execute();



		if(moveBot && changeDirect == true)
		if ((distanceTarget.x > offset && !facingRight) || (distanceTarget.x < -offset && facingRight) )
		{
				ChangeDirection();
		}
		



	}
	public void checkPath()
	{
		Vector2 start = transform.position;
		Vector2 endBot = start + new Vector2(0, -1);
		Vector2 endLeft = start + new Vector2(-1f, 0);
		Vector2 endRight = start + new Vector2(1f, 0);

	
		RaycastHit2D hitBot = Physics2D.Linecast(start, endBot, background);
		RaycastHit2D hitLeft = Physics2D.Linecast(start, endLeft, background);
		RaycastHit2D hitRight = Physics2D.Linecast(start, endRight, background);
		Debug.DrawLine(start, endRight, Color.red);
		Debug.DrawLine(start, endLeft, Color.red);
		Debug.DrawLine(start, endBot, Color.red);

		moveBot = (hitBot.transform != null) ? false : true;
		
		moveRight = (hitRight.transform != null) ? false : true;
		moveLeft = (hitLeft.transform != null) ? false : true;


	}
	private IEnumerator Shoot(float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			Shooting();

		}
	}
	public IEnumerator RandomeDirection(float w)
	{

		while (true)
		{
			yield return new WaitForSeconds(w);
			setDirection();
		//	random = true;
		}
	}
	public void CheckTarget(Vector2 disTarget)
	{
		if(Mathf.Abs(disTarget.x) <= offset)
		{
			direction = (disTarget.y > 0) ? IDLETOP : IDLEBOT;
		}else 
		{
			direction = (disTarget.y > 0) ? IDLESLANTTOP : IDLESLANTBOT;
		}

		if (Mathf.Abs(disTarget.y) <= offset)
		{
			direction = IDLERIGHT;
		}	
		
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(!(collision.gameObject.tag == "bullet"))
		{
		//	setDirection();
			//hit = true;
		}
	}
	public void setDirection()
	{
		int currentDirection = direction;
		int index = Random.Range(0, 3);
		direction = slantBot[index];
		//if(hit)
		float current = 0;
			if(!moveBot)
		{
			changeDirect = false;
			current =  transform.position.y;
		//	Debug.Log("bot");
			direction = IDLERIGHT;
			if (!moveRight)
			{
			//	Debug.Log("right");
				ChangeDirection();
			}
			if (!moveLeft)
			{
				Debug.Log("left");
				ChangeDirection();
			
			}
		}
		else if(moveBot && changeDirect == false)
		{
			direction = IDLEBOT;
			Debug.Log("can move");
			changeDirect = true;
		}

		//switch (direction)
		//	{
		//case IDLESLANTTOP:
		//direction =	 (facingRight == true) ? slantTOP[Random.Range(0, 4)] : slantTOP2[Random.Range(0,4)];

		//	break;
		//case IDLESLANTBOT:
		//direction = slantBot[Random.Range(0, 4)];
		//break;

		//}

	}
	public IEnemyState getState(float speed)
	{
		IEnemyState tl;
		switch (direction)
		{
			case IDLETOP:
				if (speed <= 0)
					tl = new IdleTopState();
				else tl = new WalkingTopState();
				return tl;
			case IDLEBOT:
				if (speed <= 0)
					tl = new IdleBotState();
				else tl = new WalkingBotState();
				return tl;
			case IDLERIGHT:
				if (speed <= 0)
					tl = new IdleRightState();
				else tl = new WalkingRightState();
				return tl;
			case IDLESLANTTOP:
				if (speed <= 0)
					tl = new IdleTopSlantState();
				else tl = new SlantTopState();
				return tl;
			case IDLESLANTBOT:
				if (speed <= 0)
					tl = new IdleBotSlantState();
				else tl = new SlantBotState();
				return tl;
			default:
				{
					Debug.Log("co day" + direction);
					return null;
				}
			
		}
		
	}

	public override void ChangeDirection()
	{
		//if (moveBot)
		//{
			facingRight = !facingRight;
			transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
		//}

		
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
	public void setSpeed(float x, float y)
	{
		this.speedX = x;
		this.speedY = y;
	}
}
