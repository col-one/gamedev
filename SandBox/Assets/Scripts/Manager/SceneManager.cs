﻿using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public string[] levelNames;
	public int gameLevelNum;

	//property membre varaible
	public static SceneManager instance = null;
	
	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this)
			Destroy(gameObject);    
		
		DontDestroyOnLoad(gameObject);
	}

	public void GoNextLevel()
	{
		if(gameLevelNum >= levelNames.Length)
			gameLevelNum = 0;

		LoadLevel(gameLevelNum);

		gameLevelNum++;
	}

	public void LoadLevel(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}

	//surcharge
	private void LoadLevel(int indexNum)
	{
		LoadLevel(levelNames[indexNum]);
	}

	public void ResetGame()
	{
		gameLevelNum=0;
	}
}
