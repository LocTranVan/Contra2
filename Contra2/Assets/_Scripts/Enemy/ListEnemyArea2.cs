using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnemyArea2 : MonoBehaviour
{
	public List<GameObject> listBaseStandingSniper;
	private List<GameObject> removeList;
	public Transform Camera;
	public float offSet;
	private int numberActiveBSSniper = 0;
	public Transform startSpawPositon, endSpawPosition;
	public GameObject enemySolider;
	private IEnumerator coroutine;
	public LayerMask background;
	// Use this for initialization
	void Start()
	{
		//ActiveEnemy();
		removeList = new List<GameObject>();

		//RaycastHit2D hitEnd = Physics2D.Raycast(new Vector2(startSpawPositon.position.x, startSpawPositon.position.y), Vector2.right, background);
	//	RaycastHit2D hitStart = Physics2D.Raycast(new Vector2(endSpawPosition.position.x, endSpawPosition.position.y), Vector2.left, background);

	//	Debug.Log(hitEnd.distance + " " + hitStart.distance);
		coroutine = spawEnemy(5f);
		StartCoroutine(coroutine);
	}
	
	private IEnumerator spawEnemy(float time)
	{
		while (true)
		{
			yield return new WaitForSeconds(time);
			getPosition();
		}
	}
	private void getPosition()
	{
		RaycastHit2D hitEnd = Physics2D.Raycast(new Vector2(endSpawPosition.position.x, endSpawPosition.position.y), Vector2.left, background);
		RaycastHit2D hitStart = Physics2D.Raycast(new Vector2(startSpawPositon.position.x, startSpawPositon.position.y), Vector2.right, background);
		if (hitStart.distance >= 5f)
		{
			Vector2 pos = new Vector2(Random.Range(startSpawPositon.position.x, startSpawPositon.position.x + 5f), startSpawPositon.position.y);
			Instantiate(enemySolider, pos, Quaternion.identity);
		}
		if (hitEnd.distance >= 5f)
		{
			Vector2 pos = new Vector2(Random.Range(endSpawPosition.position.x - 5f, endSpawPosition.position.x), endSpawPosition.position.y);
			Instantiate(enemySolider, pos, Quaternion.identity);
		}
	
	}

	// Update is called once per frame
	void Update()
	{

		if(numberActiveBSSniper != listBaseStandingSniper.Count)
			ActiveEnemy();
	}
	private void ActiveEnemy()
	{
		foreach (GameObject enemy in listBaseStandingSniper)

			if (!enemy.active)
			{
				Vector2 pEnemy = new Vector2(0, enemy.transform.position.y);
				Vector2 pCamera = new Vector2(0, Camera.position.y);
				if (Vector2.Distance(pEnemy, pCamera) <= offSet)
				{
					enemy.SetActive(true);
					removeList.Add(enemy);

					numberActiveBSSniper++;
				}
			}
		if(numberActiveBSSniper > 0)
		{
			foreach(GameObject enemy in removeList)
			{
				listBaseStandingSniper.Remove(enemy);
			}
		}

	}
}

