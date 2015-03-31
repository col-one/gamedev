using UnityEngine;
using System.Collections;

public class LoaderManager : MonoBehaviour {

	public GameObject sceneManager;
	private GameObject sceneManagerPre;
	private SceneManager sceneManagerScript;

	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (SceneManager.instance == null)
			//Instantiate gameManager prefab
			sceneManagerPre = Instantiate(sceneManager);
		else
			sceneManagerPre = GameObject.FindGameObjectWithTag("SceneManager");

		sceneManagerScript = sceneManagerPre.GetComponent<SceneManager>();
	}

	public void GoNextLevel()
	{
		sceneManagerScript.GoNextLevel();
	}
	
	public void LoadLevel(string sceneName)
	{
		sceneManagerScript.LoadLevel(sceneName);
	}
	
	//surcharge
	private void LoadLevel(int indexNum)
	{
		LoadLevel(indexNum);
	}
	
	public void ResetGame()
	{
		sceneManagerScript.ResetGame();
	}

	
}
