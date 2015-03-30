using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public string[] levelNames;
	public int gameLevelNum;

	//property membre varaible
	public static SceneManager instance;

	
	void Start () {
		//dont destroy cam/object
		DontDestroyOnLoad(gameObject);
		instance = this;
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
