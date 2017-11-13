using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyticsAndLive : MonoBehaviour {
	public static JoyticsAndLive instance = null;
	private int lives;
	public GameObject[] imgLives;
	private AudioSource audioSource;
	public AudioClip cleaStage;
	public static JoyticsAndLive Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new JoyticsAndLive();
			}
			return instance;
		}
	}
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	private void updateLivesImg(int lives)
	{
		this.lives = lives;
		if(lives >= 4)
		{
			imgLives[0].SetActive(true);
			imgLives[1].SetActive(true);
			imgLives[2].SetActive(true);
			imgLives[3].SetActive(true);
		}
		else if(lives == 3)
		{
			imgLives[0].SetActive(true);
			imgLives[1].SetActive(true);
			imgLives[2].SetActive(true);
			imgLives[3].SetActive(false);
		}
		else if(lives == 2)
		{
			imgLives[0].SetActive(true);
			imgLives[1].SetActive(true);
			imgLives[2].SetActive(false);
			imgLives[3].SetActive(false);
		}
		else if(lives == 1)
		{
			imgLives[0].SetActive(true);
			imgLives[1].SetActive(false);
			imgLives[2].SetActive(false);
			imgLives[3].SetActive(false);
		}
	
	}
	public void enableSouldClear()
	{
		audioSource.PlayOneShot(cleaStage);
	}
	// Update is called once per frame
	private void Update()
	{
		if (lives != GameManager.instance.lives)
			updateLivesImg(GameManager.instance.lives);
	}
}
