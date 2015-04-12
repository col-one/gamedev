using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharsController : MonoBehaviour {
	
	private int layerMask = 1 << 8;
	private List<GameObject> touches = new List<GameObject> ();
	private float length;
	private int maxTouch = 2;
	private bool[] dragging = new bool[2];
	private bool switchTouch;
	private int id;
	private Vector3 offset;
	private Vector2 tempVector;

	public int timeToCrash;
	public bool keepOffset;

	
//	IEnumerator killAfterSec(){
//		yield return new WaitForSeconds (timeToCrash);
//		twin_collider_trigger script = GetComponent< twin_collider_trigger >();
//		script.exploseTwin();
//	}

	void Awake () {
		layerMask = (1 << LayerMask.NameToLayer("Players"));
		touches.Add(GameObject.FindGameObjectsWithTag("player1")[0]);
		touches.Add(GameObject.FindGameObjectsWithTag("player2")[0]);
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Touch touch in Input.touches) {
			id = touch.fingerId;
			if (id <= maxTouch && touch.phase == TouchPhase.Began) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position); 
				RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, layerMask);
				if(hit)
				{
					if(keepOffset)
					{
						GameObject hitObject = hit.collider.gameObject;
						tempVector = new Vector2( hitObject.transform.position.x, hitObject.transform.position.y);
						offset = tempVector - hit.point;
					}
					if(touches.Contains(hit.collider.gameObject)){
						switchTouch = false;
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
				touches[id].transform.position = ray.GetPoint(length) + offset;
			}
			if (id <= maxTouch && touch.phase == TouchPhase.Ended && dragging[id]) {
				dragging[id] = false;
				switchTouch = true;
			}
//			if(switchTouch)
//				StartCoroutine("killAfterSec");
//			else
//				StopCoroutine("killAfterSec");
		}
	}
}








