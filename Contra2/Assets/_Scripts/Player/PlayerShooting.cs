using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	// Use this for initialization
	public Transform pGunStanding, pGunStandingUP, pGunLayingDown, pGunSlant;
	public GameObject bullet;
	private Animator anim;
	private bool shooting = false;
	public float speedBullet;
	private bool check = false;
	void Start () {
		anim = GetComponent<Animator>();
		
	}	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	private void FixedUpdate()
	{
		Animating();
		if (check)
		{
			AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
			int direction = (transform.rotation.y == 0) ? 1 : -1;
			if (animState.IsName("PlayerStandingUp"))
			{
				anim.SetTrigger("Shoot");
				GameObject bullets = Instantiate(bullet, pGunStandingUP.position, Quaternion.identity);
				bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speedBullet));
			}
			else if (animState.IsName("PlayerStanding"))
			{
				anim.SetTrigger("Shoot");
				GameObject bullets = Instantiate(bullet, pGunStanding.position, Quaternion.identity);
				bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedBullet * direction, 0));
			}
			else if (animState.IsName("PlayerLayingDown"))
			{
				GameObject bullets = Instantiate(bullet, pGunLayingDown.position, Quaternion.identity);
				bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedBullet * direction, 0));
			}
			else if (animState.IsName("shootandrun"))
			{
				GameObject bullets = Instantiate(bullet, pGunSlant.position, Quaternion.identity);
				bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedBullet * direction, speedBullet));

			}else if (animState.IsName("PlayerWalking"))
			{
				anim.SetTrigger("Shoot");
				GameObject bullets = Instantiate(bullet, pGunStanding.position, Quaternion.identity);
				bullets.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedBullet * direction, 0));

			}
			check = false;
		}
	}
	void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			check = true;
		}
	}
	void Animating()
	{
	
	}



}
