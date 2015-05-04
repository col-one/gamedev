using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;

	private float progress;
	private bool goingForward = false;
	public float delay = 0f;

	IEnumerator delayGoingForward(){
		yield return new WaitForSeconds (delay);
		goingForward = true;
	}

	private void Update () {
		if(!goingForward)
		{
			StartCoroutine("delayGoingForward");
		}


		if (goingForward) {
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
//		else {
//			progress -= Time.deltaTime / duration;
//			if (progress < 0f) {
//				progress = -progress;
//				goingForward = true;
//			}
//		}

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (lookForward) {
			Quaternion rotation = Quaternion.LookRotation
				(spline.GetDirection(progress), transform.TransformDirection(Vector3.up));
			transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
			//transform.Rotate(0,0,90);
			//transform.LookAt(position + spline.GetDirection(progress));
		}
	}
}