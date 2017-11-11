using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabol : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D mrigidbody2D;
	//public Vector2 position;
	public float angleStart, angleEnd, SpeedX, SpeedY;
	public bool prabol, cos;
	public float speed;
	public GameObject Gift;
	private Vector2 direction;
	void Start () {
		mrigidbody2D = GetComponent<Rigidbody2D>();
		if(GetComponent<Bullet>() != null)
		{
			direction = GetComponent<Bullet>().getSpeed();
		}
		if (prabol)
		{
			transform.eulerAngles = new Vector3(0, 0, Random.Range(angleStart, angleEnd));
			//
			Vector2 t = gameObject.GetComponent<Bullet>().getSpeed();
			//mrigidbody2D.AddForce(new Vector2(Random.Range(SpeedX * t.x ,  SpeedY *t.x), Random.Range(Mathf.Abs(SpeedX) , Mathf.Abs(SpeedY))));
			mrigidbody2D.AddForce(new Vector2(Random.Range(SpeedX, 100 * SpeedY), Random.Range(Mathf.Abs(100 * SpeedX),Mathf.Abs(600 * SpeedX))));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if (cos)
		{
			Vector3 position = transform.position;
			position.x += Time.deltaTime * speed;
			float y = Mathf.Cos(position.x * 3);
			position.y += y / 4;
			transform.position = position;
		}
	}
	public void intanceGift()
	{
		Instantiate(Gift, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
		Destroy(gameObject);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
	}

}
