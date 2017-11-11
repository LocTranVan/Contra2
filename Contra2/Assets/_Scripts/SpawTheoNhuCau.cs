using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawTheoNhuCau : MonoBehaviour {
	public GameObject spawObject;
	public float waitTime, coutTime;
	public int numberSpaw;
	private Transform Camera;
	private bool isActive;
	private IEnumerator corutine;
	// Use this for initialization
	void Start () {
		Camera = GameObject.Find("Main Camera").transform;
		Debug.Log("spaw");
		isActive = true;
		corutine = Spaws(waitTime);
		//StartCoroutine(corutine);
	
	}
	private IEnumerator Spaws(float waitTime)
	{
		while (true && isActive)
		{
			yield return new WaitForSeconds(waitTime);
			Instantiate(spawObject, transform.position, Quaternion.identity);
		
		}
	}
	private void FixedUpdate()
	{
		if(coutTime > waitTime)
		{
			Instantiate(spawObject, transform.position, Quaternion.identity);
			coutTime = 0;
		}else
		{
			coutTime += Time.deltaTime;
		}
		//conditionDesTroy();

	}
	private void conditionDesTroy()
	{
		if (Camera != null) {
			if (Mathf.Abs(transform.position.x - Camera.position.x) >= 6f)
			{
				Destroy(gameObject);
			}
		}else
		{
			Debug.Log("Null Camera");
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
