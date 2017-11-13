using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGift : MonoBehaviour {

	// Use this for initialization
	public GameObject Bullet;

	private bool hit;
	private float timeWait;
	private Rigidbody2D rigidbody2D;
	void Start () {
		timeWait = Time.time;
		rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if(!hit)
			if (Time.time > timeWait + 10)
				Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.gameObject.tag);
		if (collision.gameObject.tag == "Player")
		{
			Debug.Log("player");
			if (Bullet != null)
			{
				Player.Instance.GetComponent<Player>().setBullet(Bullet);
			}
			hit = true;
			Destroy(gameObject);
		}
		else  if (collision.gameObject.tag == "Background")
		{
			Debug.Log("here");
			rigidbody2D.gravityScale = 0;
		}
	}
}
