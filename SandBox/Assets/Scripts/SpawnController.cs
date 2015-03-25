using UnityEngine;
using System.Collections;

public class SpawnController : ScriptableObject {

	private ArrayList playerTransforms;
	private ArrayList playerGameObjects;
	private ArrayList objectList;

	private Transform TEMPTrans;
	private GameObject TEMPGo;

	private GameObject[] playerPrefabList;
	private Vector3[] startPositions;
	private Quaternion[] startRotations;


	//property membre varaible
	private static SpawnController instance;

	//constructor for instantiate only one spawncontroller
	public SpawnController ()
	{
		if (instance != null)
		{
			Debug.LogWarning("generate more than one spawn controller, return");
			return;
		}
		instance = this;
	}

	//property instance function get / set
	//all others script call this for deal with
	public static SpawnController Instance
	{
		get
		{
			if(instance == null)
			{
				ScriptableObject.CreateInstance<SpawnController>();
			}
			return instance;
		}
	}

	//clear array list
	public void Restart()
	{
		playerTransforms = new ArrayList();
		playerGameObjects = new ArrayList();
	}

	//set how many players and their position / rotation
	public void SetUpPlayers (GameObject[] playerPrefabs, Vector3[] playerStartPositions,
	                          Quaternion[] playerStartRotations, Transform theParentObj, 
	                          int totalPlayers)
	{
		playerPrefabList = playerPrefabs;
		startPositions = playerStartPositions;
		startRotations = playerStartRotations;
		CreatePlayers(theParentObj, totalPlayers);
	}

	//instanciates players with setupplayers info
	public void CreatePlayers(Transform theParent, int totalPlayers)
	{
		//clear arry
		playerTransforms = new ArrayList();
		playerGameObjects = new ArrayList();

		for (int i = 0; i < totalPlayers; i++)
		{
			//spawn i
			TEMPTrans = Spawn(playerPrefabList[i], startPositions[i], startRotations[i]);

			//add to the parent go with init positions
			if (theParent != null)
			{
				TEMPTrans.parent = theParent;
				TEMPTrans.localPosition = startPositions[i];
			}

			playerTransforms.Add(TEMPTrans);
			playerGameObjects.Add(TEMPTrans.gameObject);
		}
	}

	//simply for function for get go players
	public GameObject GetPlayerGO(int indexNum)
	{
		return (GameObject)playerGameObjects[indexNum];
	}
	//simply for function for get transform players
	public Transform GetPlayerTrans(int indexNum)
	{
		return (Transform)playerTransforms[indexNum];
	}

	//spawn function return Transform
	public Transform Spawn(GameObject anObject, Vector3 aPosition, Quaternion aRotation)
	{
		if(objectList==null)
			objectList=new ArrayList();

		TEMPGo = (GameObject) Instantiate(anObject, aPosition, aRotation);
		TEMPTrans = TEMPGo.transform;

		objectList.Add(TEMPTrans);

		return TEMPTrans;
	}
	//spawn function return Transform
	public GameObject SpawnGo(GameObject anObject, Vector3 aPosition, Quaternion aRotation)
	{
		if(objectList==null)
			objectList=new ArrayList();
		
		TEMPGo = (GameObject) Instantiate(anObject, aPosition, aRotation);
		TEMPTrans = TEMPGo.transform;
		
		objectList.Add(TEMPTrans);
		
		return TEMPGo;
	}

	//function return objectList
	public ArrayList GetAllSpawnedTrans()
	{
		return objectList;
	}
}















