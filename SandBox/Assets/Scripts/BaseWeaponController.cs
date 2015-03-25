using UnityEngine;
using System.Collections;

public class BaseWeaponController : MonoBehaviour {

	public GameObject[] weapons;

	public int selectedWeaponSlot;
	public int lastSelectedWeaponSlot;

	public Vector3 offsetWeaponSpawnPosition;

	public Transform forceParent;

	private ArrayList weaponSlots;
	private ArrayList weaponScripts;
	private BaseWeapon TEMPWeapon;
	private Vector3 TEMPVector3;
	private Quaternion TEMPRotation;
	private GameObject TEMPGo;

	private Transform myTransform;

	public bool useForceVectorDirection;
	public Vector3 forceVector;

	private Vector3 theDir;

	public enum ParentDir{X, Y, Z};
	public ParentDir fireDirections;
	
	private Vector3 localVelocity;

	public virtual void Start()
	{
		//set default slots
		selectedWeaponSlot = 0;
		lastSelectedWeaponSlot = -1;

		//clear init list
		weaponSlots = new ArrayList();
		weaponScripts = new ArrayList();

		//get transform
		myTransform = transform;

		if(forceParent==null)
			forceParent=myTransform;

		//set parent pos rot in temp var
		TEMPVector3 = forceParent.position;
		TEMPRotation = forceParent.rotation;

		//instantiate all weapons and hide them
		for(int i=0; i < weapons.Length; i++)
		{
			//instantiate
			TEMPGo = SpawnController.Instance.Spawn(weapons[i], TEMPVector3+offsetWeaponSpawnPosition,TEMPRotation)
				.gameObject;

			//set parent layer and po of the new weapons object
			TEMPGo.transform.parent = forceParent;
			TEMPGo.layer = forceParent.gameObject.layer;
			TEMPGo.transform.position = forceParent.position;
			TEMPGo.transform.rotation = forceParent.rotation;

			//add to weapons slot
			weaponSlots.Add(TEMPGo);

			//get weapon script
			TEMPWeapon = TEMPGo.GetComponent<BaseWeapon>();
			weaponScripts.Add(TEMPWeapon);

			//disable hide the weapon
			TEMPGo.SetActive(false);
		}

		//equipe first weapon
		SetWeaponSlot(0);

	}

	//function for equipe weapon id
	public virtual void SetWeaponSlot(int slotNum)
	{
		//check if this slot is already equiped
		if(slotNum==lastSelectedWeaponSlot)
			return;

		//disable the current weapon
		DisableCurrentWeapon();

		//set selected weapon slot
		selectedWeaponSlot = slotNum;

		//check value
		if(selectedWeaponSlot<0)
			selectedWeaponSlot= weaponSlots.Count-1;
		if(selectedWeaponSlot>weaponSlots.Count-1)
			selectedWeaponSlot= weaponSlots.Count-1;

		//store last slot as current slot
		lastSelectedWeaponSlot = selectedWeaponSlot;

		//enable new weapon
		EnableCurrentWeapon();

	}


	//set the next weapon on ask
	public virtual void NextWeaponSlot (bool shouldLoop)
	{
		DisableCurrentWeapon();
		selectedWeaponSlot++;

		//check slot id
		if(selectedWeaponSlot==weaponScripts.Count)
		{
			if(shouldLoop)
				selectedWeaponSlot = 0;
			else
				selectedWeaponSlot = weaponScripts.Count-1;
		}

		//store last weapon
		lastSelectedWeaponSlot = selectedWeaponSlot;

		//enable weapon
		EnableCurrentWeapon();

	}

	//previous weapon
	public virtual void PreviousWeaponSlot(bool shouldLoop)
	{
		DisableCurrentWeapon();
		selectedWeaponSlot--;

		if(selectedWeaponSlot<0)
		{
			if(shouldLoop)
				selectedWeaponSlot = weaponScripts.Count-1;
			else
				selectedWeaponSlot = 0;
		}

		lastSelectedWeaponSlot = selectedWeaponSlot;

		EnableCurrentWeapon();

	}


	//function for disable current weapon
	public virtual void DisableCurrentWeapon()
	{
		if(weaponScripts.Count == 0)
			return;

		//get temp weapon script of selectedslot
		TEMPWeapon = (BaseWeapon) weaponScripts[selectedWeaponSlot];

		//disable weapon
		TEMPWeapon.Disable();

		//get game object
		TEMPGo = (GameObject) weaponSlots[selectedWeaponSlot];

		//disable it
		TEMPGo.SetActive(false);
	}

	//same for enable current weapon
	public virtual void EnableCurrentWeapon()
	{
		if(weaponScripts.Count == 0)
			return;
		
		//get temp weapon script of selectedslot
		TEMPWeapon = (BaseWeapon) weaponScripts[selectedWeaponSlot];
		
		//enable weapon
		TEMPWeapon.Enable();
		
		//get game object
		TEMPGo = (GameObject) weaponSlots[selectedWeaponSlot];
		
		//enable it
		TEMPGo.SetActive(true);
	}

	//function fire
	public virtual void Fire()
	{
		//check lenght
		if(weaponScripts==null)
			return;
		if(weaponScripts.Count==0)
			return;

		//get weapon script
		TEMPWeapon = (BaseWeapon) weaponScripts[selectedWeaponSlot];

		theDir = GetDir();

		if(useForceVectorDirection)
			theDir = forceVector;

		//fire ! call function fire weapon
		TEMPWeapon.Fire(theDir);
	}

	public virtual Vector3 GetDir()
	{
		//get current weapon
		TEMPWeapon = (BaseWeapon) weaponScripts[selectedWeaponSlot];
		//get transformation from world to local of projectile
		localVelocity = transform.InverseTransformDirection(TEMPWeapon.projectileGo.rigidbody.velocity);
		//shoose veolicity dir
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
		//reset form local to world
		localVelocity = transform.TransformDirection(localVelocity);

		return localVelocity;
	}
}
