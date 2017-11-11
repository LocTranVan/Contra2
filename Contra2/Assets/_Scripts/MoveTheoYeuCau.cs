using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTheoYeuCau : MonoBehaviour {

	// Use this for initialization
	public float speed;
	private float journey, timeStart, waitTime;
	private float journey1, journey2;
	private Vector3 endTranform, startTranform;
	public List<Transform> arrPosition;
	public List<GameObject> gameObject;
	private enum Direction
	{
		left, right, top, bot
	}
	private Direction move;
	void Start () {
		if (arrPosition.Count == 0)
		{
			startTranform = transform.localScale;
			endTranform = new Vector3(startTranform.x, startTranform.y / 2, startTranform.z);
			journey = Vector3.Distance(startTranform, endTranform);
			timeStart = Time.time;
		}
		else
		{
			 move = Direction.top;
			journey1 = Vector3.Distance(arrPosition[0].position, arrPosition[3].position);
			journey2 = Vector3.Distance(arrPosition[0].position, arrPosition[4].position);
		}
	}
	private void FixedUpdate()
	{
		if (Time.time > waitTime + 6f)
		{
			float timeJourney = (Time.time - timeStart) * speed;
			float distance = timeJourney * Time.deltaTime / journey;
			transform.localScale = Vector3.Lerp(startTranform, endTranform, distance);
			if (transform.localScale == endTranform)
			{
				Vector3 tam = startTranform;
				startTranform = endTranform;
				endTranform = tam;
				waitTime = Time.time;
				timeStart = waitTime + 6;
			}
		}
	}
	private void high()
	{
		
	}
	private void Move(GameObject Object, Vector3 startTranform, Vector3 endTranform)
	{
		Object.transform.position = Vector3.Lerp(startTranform, endTranform, Time.deltaTime * speed);
	}
	private List<Vector3> position(Direction move)
	{
		List<Vector3> listTam = new List<Vector3>();
		switch (move)
		{
			case Direction.left:
				listTam.Add(arrPosition[5].position);
				listTam.Add(arrPosition[6].position);
				listTam.Add(arrPosition[7].position);
				listTam.Add(arrPosition[8].position);
				break;

			case Direction.top:
				listTam.Add(arrPosition[3].position);
				listTam.Add(arrPosition[2].position);
				listTam.Add(arrPosition[0].position);
				listTam.Add(arrPosition[1].position);
				break;
			
		}
		return listTam;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
