using UnityEngine;
using System.Collections;

public class FireTest : MonoBehaviour {

	public BaseWeaponController2D weapon;


	// Use this for initialization
	void Start () {
		weapon = (BaseWeaponController2D) GetComponent<BaseWeaponController2D>();
		//weapon.SetWeaponSlot(0);
	}
	
	// Update is called once per frame
	void Update () {

		weapon.Fire();
	}
}