using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManage : MonoBehaviour
{

	// Use this for initialization
	public GameObject defaultBullet;
	public static gameManage instance = null;
	private int level = 1;
	private string[] scenePaths = { "Area1", "Area2", "Area3" };
	private int currentArea;

	public int lives
	{
		get; set;

	}
	public GameObject Bullet
	{
		get; set;
	}
	public bool immortal
	{
		get; set;
	}
	public int Score
	{
		get; set;
	}
	void Start()
	{

		if (instance == null)
			instance = this;
		else if (instance != this)

			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void setCurrentArea(int newCurrent)
	{
		currentArea = newCurrent;
	}
	public void init()
	{

	}
}
