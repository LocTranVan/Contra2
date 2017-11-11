using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStones : MonoBehaviour {
	private List<Transform> Stones;
	private float offSet = -0.6f;
	private GameObject Player;
	private GameObject MainCamera;
		
	public float distance;
	private float waitTime;
	private int[] Maps = { 2, 5, 1, 1, 3, 3, 1, 1, 5, 0, 0, 0, 5, 0, 5, 0, 5, 0, 5, 3, 3, 1, 1, 5, 3, 2, 0 };
	public int numberPushDown;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		MainCamera = GameObject.Find("Main Camera");
		if (numberPushDown == 2)
		{
			MainCamera.GetComponent<CameraMovement>().setChandong();
		}
	}
	private void FixedUpdate()
	{
		if (Player == null)
			return;
		if (Player.transform.position.x > transform.position.x - distance)
			PushDown();
		if (Player.transform.position.x > transform.position.x + 6)
			Destroy(gameObject);
		
	}
	// Update is called once per frame
	void Update () {
		
	}
	private void PushDown()
	{
		while (numberPushDown > 0) {
			transform.position += new Vector3(0, offSet, 0);
			waitTime = Time.time;
			if (Time.time - waitTime >= 1f)
				transform.position += new Vector3(0, offSet, 0);
			numberPushDown--;
		}
	}

}
