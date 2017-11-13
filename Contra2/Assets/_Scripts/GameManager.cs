using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
    
    public GameObject[] bulletPrefabs;
	public static GameManager instance = null;
	public string[] scenePaths = { "Area1", "Area2", "Area3" };
	public int currentArea; //0, 1, 2
    public Dictionary<string, int> gameResult;
    public bool isGameOver;
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

    
	void Start () {
		
		if (instance == null)
			instance = this;
		else if (instance != this)

			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		init();
	}
	// Test
	
	public void init()
	{
		lives = 3;
		immortal = true;
		Bullet = bulletPrefabs[0];
		gameResult = new Dictionary<string, int>();
		SceneManager.LoadScene("Area1");
	}
	
	

    public void setResult(string key, int value)
    {
        int val;
        if (gameResult.TryGetValue(key, out val))
        {
            // yay, value exists!
            gameResult[key] = value;
        }
        else
        {
            // darn, lets add the value
            gameResult.Add(key, value);
        }
    }
	public void changeResult(string key, int offset)
	{
		int val;
		if (gameResult.TryGetValue(key, out val))
		{
			// yay, value exists!
			gameResult[key] = val + offset;
			//Debug.Log(key + val);
		}
		else
		{
			// darn, lets add the value
			gameResult.Add(key, offset);
			//Debug.Log("key"+key + val);
		}
	}

}
