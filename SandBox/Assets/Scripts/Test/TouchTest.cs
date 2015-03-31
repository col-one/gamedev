using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchTest : MonoBehaviour {
	
	private int layerMask;
	List<GameObject> touches = new List<GameObject> ();
	float length;
	int maxTouch = 2;
	bool[] dragging = new bool[2];
	bool switchTouch;
	private int id;
	
	void Awake () {
		layerMask = (1 << LayerMask.NameToLayer("Players"));
		touches.Add(GameObject.FindGameObjectsWithTag("player1")[0]);
		//touches.Add(GameObject.FindGameObjectsWithTag("player1")[0]);
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Touch touch in Input.touches) {
			id = touch.fingerId;
			if (id <= maxTouch && touch.phase == TouchPhase.Began) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position); 
				RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, layerMask);
				if(hit){
					if(touches.Contains(hit.collider.gameObject)){
						if(hit.collider.gameObject!=touches[id]){
							touches.Reverse();
						}
						dragging[id] = true;
						length = (hit.transform.position - Camera.main.transform.position).magnitude;
					}
				}
			}
			if (id <= maxTouch && touch.phase == TouchPhase.Moved && dragging[id]) {
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				touches[id].transform.position = ray.GetPoint(length);
			}
			if (id <= maxTouch && touch.phase == TouchPhase.Ended && dragging[id]) {
				dragging[id] = false;
			}
		}
	}
}








