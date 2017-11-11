using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
	public int Health;
	public float speedMovement;
	//public bool IsBoss;
	private bool IsBroken;
	private GameObject Camera;
	private int LimitHelth;
	public float speed;
	public GameObject Brooken;
	public bool canMove;
	public LayerMask Player;
	private Collider2D collision;
	private GameObject Target;
	[SerializeField]
	private List<string> damageSources;
	// Use this for initialization
	void Start () {
		//	Debug.Log(gameObject.GetComponentsInChildren(typeof(Transform), true)[1].gameObject);
		LimitHelth = Health / 2;
		Target = GameObject.Find("Player");
		collision = GetComponent<Collider2D>();
		Camera = GameObject.Find("Main Camera");
		if (gameObject.tag == "Tank")
		{

			Camera.GetComponent<CameraMovement>().setMileStones(new Vector3(0,transform.position.y - 4f, 0));
			Camera.GetComponent<CameraMovement>().setBlock(true);

		}
	}
	private void FixedUpdate()
	{
		if (canMove)
			Move();
	}
	public void Move()
	{
		/*
		collision.enabled = false;
		RaycastHit2D LeftSide = Physics2D.Raycast(transform.position, Vector2.right, Player);
		collision.enabled = true;
		*/
		if (transform.position.x > Target.transform.position.x)
		{
			transform.position += Vector3.left * Time.deltaTime * speedMovement;
		}
		else
		{
			transform.position += Vector3.right * Time.deltaTime * speedMovement;
		}
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

		if(Health <= 0)
		{
			if(gameObject.tag == "Boss")
			{
				Camera.GetComponent<CameraMovement>().setBlock(false);
				Debug.Log("here");
				
				GameManager.instance.LoadScene(0);
				Destroy(gameObject); 
			}
			foreach(Transform tranform in gameObject.GetComponentsInChildren(typeof(Transform), true))
			{
				if (gameObject.GetComponentsInChildren(typeof(Transform), true).Length == 2) {
					Destroy(gameObject);
				}
				else if(tranform != gameObject.GetComponentsInChildren(typeof(Transform), true)[0])
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
