using UnityEngine;
using System.Collections;

public class BaseWeapon2D : MonoBehaviour {
	public float pp=0.0f;

	[System.NonSerialized]
	public bool canFire;

	public int ammo = 100;
	public int maxAmmo = 100;

	public bool isInfinitAmmo;
	public GameObject projectileGo;
	public Collider2D parentCollider;

	private Vector3 fireVector;

	[System.NonSerialized]
	public Transform myTransform;

	private int myLayer;

	public Vector3 spawnPosOffset;
	public float forwardOffset = 1.5f;
	public float reloadTime = 0.2f;
	public bool inheritVelocity;

	[System.NonSerialized]
	public Transform theProjectile;

	private GameObject theProjectileGo;
	private bool isLoaded;
	//private ProjectileController theProjectileController;

	public virtual void Start()
	{
		Init();
	}

	//get transform and layer of the weapon
	public virtual void Init()
	{
		myTransform = transform;
		myLayer= gameObject.layer;
		Reloaded();
	}

	//enable / disable weapon
	public virtual void Disable()
	{
		if(canFire==false)
			return;
		canFire =false;
	}
	public virtual void Enable()
	{
		if(canFire==true)
			return;
		canFire =true;
	}

	//reload weapon
	public virtual void Reloaded()
	{
		//ready to fire ?
		isLoaded= true;
	}

	//set parent collider
	public virtual void SetCollider(Collider2D aCollider)
	{
		parentCollider = aCollider;
	}

	public virtual void Fire(Vector3 aDirection)
	{
		if(!canFire)
			return;

		if(!isLoaded)
			return;

		if(ammo<=0 && !isInfinitAmmo)
			return;

		ammo--;
		//fire the projectile
		FireProjectile(aDirection);

		//time out for fire rate
		isLoaded = false;

		//Invoke function Reloaded for do timeout
		CancelInvoke("Reloaded"); //be sure there is no invoke reloaded
		Invoke("Reloaded", reloadTime);
	}

	//function for set projectil and set velocity
	public virtual void FireProjectile(Vector3 fireDirection)
	{
		//make projectile
		theProjectile = MakeProjectile();

		//set lookat
		//not use in 2d mode
		//theProjectile.LookAt(theProjectile.position+fireDirection);

		//set speed velocity reigidbody
		theProjectile.GetComponent<Rigidbody2D>().velocity = fireDirection * pp;
	}

	public virtual Transform MakeProjectile()
	{
		//instantiate projectile with spawner
		theProjectile= SpawnController.Instance.Spawn(projectileGo, myTransform.position + spawnPosOffset + 
		                                              (myTransform.up * forwardOffset), Quaternion.identity);
		theProjectileGo= theProjectile.gameObject;
		theProjectileGo.layer = myLayer;

		//ignore layers
		Physics2D.IgnoreLayerCollision(myTransform.gameObject.layer, myLayer);

		//ignore colision parent
		if(parentCollider != null)
		{
			Physics2D.IgnoreCollision(theProjectile.GetComponent<Collider2D>(), parentCollider);
		}

		return theProjectile;
	}

}
