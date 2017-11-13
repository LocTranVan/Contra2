using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	private Animation anim;
	private Transform Target, Camera;
	// Use this for initialization
	void Start () {
		Target = GameObject.Find("Player").transform;
		Camera = GameObject.Find("Main Camera").transform;
	}
	private void FixedUpdate()
	{
		if (transform.position.y <= (Target.position.y - 6f) || Mathf.Abs(transform.position.x - Camera.position.x) >= 10f)
		{
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
