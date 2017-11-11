using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public static GameManager instance = null;
	private int level = 1;
	private string[] scenePaths = { "Area1", "Area2", "Area3" };
	void Start () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)

			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}
	public void LoadScene(int i)
	{
		Debug.Log("here");
		SceneManager.LoadScene(scenePaths[i]);
	}
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static public void CallbackInitialization()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		instance.level++;
		instance.InitGame();
	}


	//Initializes the game for each level.
	void InitGame()
	{
		// Update is called once per frame
		
		
	}
}
