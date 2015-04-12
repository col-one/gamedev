using UnityEngine;
using System.Collections;


/*units sprites must be to 1
 * USE IT ONLY FOR PIXEL ART*/

public class CameraSizeFromRes : MonoBehaviour {
	public float maxPixelHeight = 320f;
	void Awake () {
		float scaleRatio = Screen.height / maxPixelHeight;
		Camera.main.orthographicSize = Screen.height / 2f / scaleRatio;
	}
}
