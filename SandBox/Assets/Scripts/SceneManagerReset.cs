using UnityEngine;
using System.Collections;

public class SceneManagerReset : MonoBehaviour {

	public SceneManager scnManager;
	// Use this for initialization
	void Start () {
		scnManager = (SceneManager) GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetLevel()
	{
		scnManager.ResetGame();
		scnManager.GoNextLevel();
	}

}
