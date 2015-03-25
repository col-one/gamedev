using UnityEngine;
using System.Collections;

public class FireTest : MonoBehaviour {

	public BaseWeaponController weapon;


	// Use this for initialization
	void Start () {
		weapon = (BaseWeaponController) GetComponent<BaseWeaponController>();
		//weapon.SetWeaponSlot(0);
	}
	
	// Update is called once per frame
	void Update () {

		weapon.Fire();
	}
}