using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public GameObject defaultBullet;
	public static GameManager instance = null;
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
	public int numberSolider
	{
		get; set;
	}
	void Start () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)

			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}
	public void LoadArea()
	{
		//Debug.Log("here");
		SceneManager.LoadScene(scenePaths[currentArea]);
		currentArea++;
	}
	public void setCurrentArea(int newCurrent)
	{
		currentArea = newCurrent;
	}
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static public void CallbackInitialization()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		instance.level++;
		//instance.InitGame();
		instance.InitGameOffLine();
	}

	
	//Initializes the game for each level.
	void InitGame()
	{
		LoadArea();	
	}
	void InitGameOffLine()
	{
		lives = 3;
		Bullet = defaultBullet;
		immortal = false;
		LoadArea();
	}
}
