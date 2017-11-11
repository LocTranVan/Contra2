using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGift : MonoBehaviour {

	// Use this for initialization
	public GameObject Bullet;

	private bool hit;
	private float timeWait;
	void Start () {
		timeWait = Time.time;
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
	}
}
