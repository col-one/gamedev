using UnityEngine;
using System.Collections;

public class BasePowerUp : MonoBehaviour {

	private BaseWeaponController2D weaponController;

	private LayerMask layerMaskPlayer;
	private LayerMask layerMaskBullet;

	void Awake()
	{
		layerMaskBullet = LayerMask.NameToLayer("Bullet");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer != layerMaskBullet){
			weaponController = other.GetComponentInChildren<BaseWeaponController2D>();
			weaponController.NextWeaponSlot(false);
		}
		Destroy(gameObject);
	}
}
