using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
	public int Health;
	
	private bool IsBroken;
	private GameObject Camera;
	private int LimitHelth;
	public GameObject Brooken;
	[SerializeField]
	private List<string> damageSources;
	// Use this for initialization
	void Start () {
		//	Debug.Log(gameObject.GetComponentsInChildren(typeof(Transform), true)[1].gameObject);
		LimitHelth = Health / 2;
		if(gameObject.tag == "Tank")
		{
			Camera = GameObject.Find("Main Camera");
			Camera.GetComponent<CameraMovement>().setMileStones(transform.position - new Vector3(0, 4f, 0));
			Camera.GetComponent<CameraMovement>().setBlock(true);
		}
	}
	private void FixedUpdate()
	{
		
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (damageSources.Contains(collision.tag) && !IsBroken)
		{
			TakeDamge(collision.tag);
		}
	}
	private void TakeDamge(string Gun)
	{
		Health -= Assets._Scripts.ManagerGun.getIntance().getDame(Gun);
		if(Health <= LimitHelth)
		{
			Instantiate(Brooken, new Vector3(Random.Range(transform.position.x - 1f, transform.position.x + 1f), Random.Range(transform.position.y - 1f, transform.position.y + 1f), 0), Quaternion.identity);
		}

		if(Health == 0)
		{
			foreach(Transform tranform in gameObject.GetComponentsInChildren(typeof(Transform), true))
			{
				if(tranform != gameObject.GetComponentsInChildren(typeof(Transform), true)[0])
				{
					if (gameObject.tag == "Tank")
					{
						Camera.GetComponent<CameraMovement>().setBlock(false);
					}
					Destroy(tranform.gameObject);
				}
			}
			IsBroken = true;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
