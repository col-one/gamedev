/*/
Author : Colin Laubry
	This abstract class wrap classique methode of Input.Touch ...
	form a script who derived from this class TouchAbstract call
	methode OnTouchMoved, OnTouchEnded, OnTouchStationary, OnTouchBegan
	put this derived script on any object of the scene like Camera.

	Public Param : mask
		set the mask of touchable object.
	Public Param : touch2D
		set true for use with 2D physics
/*/


using System;
using UnityEngine;
using System.Collections;

public class TouchAbstract : MonoBehaviour {

	public LayerMask mask; //touchable mask
	public static int touchId; //id of current touch, accessible by other script
	private Ray ray; //ray for screentoscene
	private RaycastHit rayInfo; //get info from the raycast
	public static GameObject touchedObject; //get touched current object, accessible by other script
	public static float rayLength = Mathf.Infinity; //get ray length limit for rayCast, accessible by other script
	public bool touch2D = false; //public param for use with 2D physics
	private bool cast; //bool for cast
	private RaycastHit2D hit; //ray for hit 2D
	private Vector3 pos2D; //pos2D from screen to world

	void Update()
	{
		if (Input.touches.Length > 0) 
		{
			foreach(Touch touch in Input.touches)
			{
				touchId = touch.fingerId;
				if(touch2D)
				{
					pos2D = Camera.main.ScreenToWorldPoint(touch.position); //get world pos
					hit = Physics2D.Raycast(pos2D, Vector2.zero, rayLength, mask); //get the ray and hit
					if(hit.collider != null)
						touchedObject = hit.collider.transform.gameObject;
				}
				else //for 3D set up
				{
					ray = Camera.main.ScreenPointToRay(touch.position);
					cast = Physics.Raycast(ray, out rayInfo, rayLength, mask);
					if(cast) // double check if ray collide object 2D or weard things
						touchedObject = rayInfo.transform.gameObject;
				}

				if(hit.collider != null || cast && touchedObject != null)
				{
					switch(touch.phase)
					{
					case TouchPhase.Began :
						this.SendMessage("OnTouchBegan");
						break;
					case TouchPhase.Canceled :
						this.SendMessage("OnTouchCanceled");
						break;
					case TouchPhase.Ended :
						this.SendMessage("OnTouchEnded");
						break;
					case TouchPhase.Moved :
						this.SendMessage("OnTouchMoved");
						break;
					case TouchPhase.Stationary :
						this.SendMessage("OnTouchStationary");
						break;
					}
				}
			}
		}
	}
}
