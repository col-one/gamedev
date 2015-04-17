using UnityEngine;
using System.Collections;

public class SpawnFeedback : MonoBehaviour {

	public GameObject spriteFeedback;
	public Vector3 offset;
	private Transform feedBack;
	private bool created = false;

	public bool Created
	{
		get{return created;}
	}

	public void CreateFeedback (Vector3 pos) 
	{
		if(!created)
		{
			feedBack = SpawnController.Instance.Spawn(spriteFeedback, pos + offset, Quaternion.identity);
			created = true;
		}
	}

	public void FollowParent(Transform trans)
	{
		feedBack.position = trans.position;
	}

	public void Update()
	{
		if(created)
			FollowParent(feedBack.transform);
	}

	public void DestroyFeedback()
	{
		created = false;
		Destroy (feedBack.gameObject);
	}
}
