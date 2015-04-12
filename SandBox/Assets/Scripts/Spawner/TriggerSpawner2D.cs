using UnityEngine;
using System.Collections;

public class TriggerSpawner2D : MonoBehaviour {

	public GameObject objectToSpawnOnTrigger;
	public Vector3 offsetPosition;
	public bool onlySpawnOnce;

	//bullet layer example
	public int layerToCauseTriggerHit = 13;

	private Transform myTransform;

	void Start()
	{
		//to test ... still weard
		Vector3 TEMPPos = transform.position;
		TEMPPos.y = Camera.main.transform.position.y;
		transform.position=TEMPPos;
		myTransform = transform;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer != layerToCauseTriggerHit)
			return;

		//instantiate the hit object
		Instantiate(objectToSpawnOnTrigger, myTransform.position+offsetPosition, Quaternion.identity);

		if(onlySpawnOnce)
			Destroy(gameObject);
	}
}
