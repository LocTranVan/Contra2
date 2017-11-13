using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnemyArea2 : MonoBehaviour
{
	public List<GameObject> Gift;
	public List<GameObject> listBaseStandingSniper;
	public List<Transform> listPositionSpaw;
	public float waitTime, waitTimeSpaw;
	private List<GameObject> removeList;
	public Transform Camera;
	public float offSet;
	public bool checkRayCast;
	private int numberActiveBSSniper = 0;
	public Transform startSpawPositon, endSpawPosition;
	public GameObject enemySolider;
	private IEnumerator coroutine;
	public LayerMask background;
	private bool block = false;
	// Use this for initialization
	void Start()
	{
		//ActiveEnemy();
		removeList = new List<GameObject>();

		//RaycastHit2D hitEnd = Physics2D.Raycast(new Vector2(startSpawPositon.position.x, startSpawPositon.position.y), Vector2.right, background);
		//	RaycastHit2D hitStart = Physics2D.Raycast(new Vector2(endSpawPosition.position.x, endSpawPosition.position.y), Vector2.left, background);

		//	Debug.Log(hitEnd.distance + " " + hitStart.distance);
			coroutine = spawEnemy(waitTime);
			StartCoroutine(coroutine);
			numberActiveBSSniper = listBaseStandingSniper.Count;
			if(Gift.Count != 0)
		{
			StartCoroutine(SpawGift(waitTimeSpaw));
		}
	}
	private IEnumerator SpawGift(float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			int number = Random.Range(1, Gift.Count);
			Vector2 pos = new Vector2(startSpawPositon.position.x, startSpawPositon.position.y +3);
			Instantiate(Gift[number - 1], pos, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(waitTime, waitTime * 20));
		}
	}
	private void SpawEnemy(Transform pTransform)
	{
			if(!block)
			Instantiate(enemySolider, pTransform.position, Quaternion.identity);
	}
	private IEnumerator spawEnemy(float time)
	{
		while (true && !block)
		{
			yield return new WaitForSeconds(time);
			getPosition();
			yield return new WaitForSeconds(Random.Range(1f, 2f));
		}
	}
	private void getPosition()
	{
		RaycastHit2D hitEnd = Physics2D.Raycast(new Vector2(endSpawPosition.position.x, endSpawPosition.position.y), Vector2.left, background);
		RaycastHit2D hitStart = Physics2D.Raycast(new Vector2(startSpawPositon.position.x, startSpawPositon.position.y), Vector2.right, background);
		if (hitStart.distance >= 5f)
		{
			Vector2 pos = new Vector2(Random.Range(startSpawPositon.position.x, startSpawPositon.position.x + 5f), startSpawPositon.position.y);
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(pos.x, pos.y), Vector2.down, background);
			if(hit.distance >= 1f)
			Instantiate(enemySolider, pos, Quaternion.identity);
		}
		if (hitEnd.distance >= 5f)
		{
			Vector2 pos = new Vector2(Random.Range(endSpawPosition.position.x - 5f, endSpawPosition.position.x), endSpawPosition.position.y);
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(pos.x, pos.y), Vector2.down, background);
			if (hit.distance >= 1f)
				Instantiate(enemySolider, pos, Quaternion.identity);
		}
		if (!checkRayCast)
		{
			Vector2 pos = new Vector2(Random.Range(startSpawPositon.position.x, startSpawPositon.position.x + 5f), startSpawPositon.position.y);
			Instantiate(enemySolider, pos, Quaternion.identity);
			
		}
	
	}

	// Update is called once per frame
	void Update()
	{

	//	if(numberActiveBSSniper != listBaseStandingSniper.Count)
			if(removeList.Count < numberActiveBSSniper)
				ActiveEnemy();
		else
		{
			block = true;
		}
	}
	private void ActiveEnemy()
	{
		foreach (GameObject enemy in listBaseStandingSniper)

			if (!enemy.active)
			{
				Vector2 pEnemy = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
				Vector2 pCamera = new Vector2(Camera.position.x, Camera.position.y);
				if (Vector2.Distance(pEnemy, pCamera) <= offSet)
				{
					enemy.SetActive(true);
					removeList.Add(enemy);
					//Debug.Log(numberActiveBSSniper);
					//numberActiveBSSniper++;
				}
			}
	
	//	if(numberActiveBSSniper > 0)
		//{
			foreach(GameObject enemy in removeList)
			{
				listBaseStandingSniper.Remove(enemy);
			}
		//}
		

	}
}

