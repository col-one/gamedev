using UnityEngine;
using System.Collections;

public class FireTest : MonoBehaviour {

	public BaseWeapon weapon;
	
	public enum ParentDir{X, Y, Z};
	public ParentDir fireDirections;

	private Vector3 localVelocity;

	// Use this for initialization
	void Start () {
		weapon = GetComponent<BaseWeapon>();
		weapon.Enable();
		weapon.Reloaded();
	}
	
	// Update is called once per frame
	void Update () {

		localVelocity = transform.InverseTransformDirection(weapon.projectileGo.rigidbody.velocity);

		switch(fireDirections)
		{
		case ParentDir.X:
			localVelocity.x = 1;
			break;
		case ParentDir.Y:
			localVelocity.y = 1;
			break;
		case ParentDir.Z:
			localVelocity.z = 1;
			break;
		default:
			localVelocity = new Vector3(0,0,0);
			break;
		}

		localVelocity = transform.TransformDirection(localVelocity);

		weapon.Fire(localVelocity);
	}
}