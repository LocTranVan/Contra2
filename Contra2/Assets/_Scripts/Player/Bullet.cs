using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	private Vector2 startPosition;
	private Vector3 endPosition;
	private Animator animator;
	private string nameGun;
	private string nameTag;

	private AudioSource audio;
	public AudioClip hitBackground;
	private float speedX, speedY;
	private float rangeShoot, speedBullet;
	private Rigidbody2D rigidbody2D;
	private bool boom = false;
	void Start () {
		nameGun = gameObject.tag;
		startPosition = transform.position;
		animator = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		speedBullet = Assets._Scripts.ManagerGun.getIntance().getSpeedBullet(nameGun);
		rangeShoot = Assets._Scripts.ManagerGun.getIntance().getRangeShoot(nameGun);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void FixedUpdate()
	{
		if (!boom)
		{
			float speedB = speedBullet * Time.deltaTime;
			transform.position += new Vector3(speedX * speedB, speedY * speedB, 0);
			if (Vector2.Distance(startPosition, transform.position) >= rangeShoot)
			{
				if (gameObject.tag == "Gift")
					Destroy(gameObject);
				else
				{
					if(animator != null)
					animator.SetTrigger("Hit");
					boom = true;
				}
			}
		}
	}
	public void Boom()
	{
		Destroy(gameObject);
	}
	public void playHit()
	{
		audio = GetComponent<AudioSource>();
	//	audio.PlayOneShot(hitBackground);
		audio.Play();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
			if(collision.gameObject.tag != nameTag)
			{
			//		boom = true;
			//	rigidbody2D.velocity = Vector2.zero;
			//rigidbody2D.Sleep();
					Animator anim = GetComponent<Animator>();
					if(animator != null)
		
					animator.SetTrigger("Hit");
			}
		
		
	}
	public void setSpeed(Vector2 speed)
	{
		if (speed.x == 0 && speed.y == 0)
			Destroy(gameObject);
		this.speedX = speed.x;
		this.speedY = speed.y;
	}
	public Vector2 getSpeed()
	{
		return new Vector2(speedX, speedY);
	}
	public void setTagShoot(string nameTag)
	{
		this.nameTag = nameTag;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//Destroy(gameObject);

	}
}
