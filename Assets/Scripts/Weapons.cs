using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetails { 
	public string name;
	public GameObject gameObject;
	public int price;
	public bool purchase;
	public int health;
	public int damage;
	public int level;
	public string munition;
}

public class MunitionDetails {
	public string name;
	public GameObject gameObject;
	public Vector3 originalPos;
}

public class Weapons : MonoBehaviour {

	//retrieve gameobjects
	public GameObject cannon01Prefab;
	public GameObject cannon02Prefab;
	//retrieve munitions
	public GameObject cannonballPrefab;

	//declare weapons
	public WeaponDetails Cannon01 = new WeaponDetails();
	public WeaponDetails Cannon02 = new WeaponDetails();
	//declare munitions
	public MunitionDetails Cannonball = new MunitionDetails();

	//create weapons list
	public List<WeaponDetails> weaponsList = new List<WeaponDetails>();
	//munitions list
	public List<MunitionDetails> munitionsList = new List<MunitionDetails>();


	void Start () {

		//set weapon properties
		Cannon01.name = "Cannon01";
		Cannon01.gameObject = cannon01Prefab;
		Cannon01.price = 500;
		Cannon01.purchase = false;
		Cannon01.level = 0;
		Cannon01.health = 500;
		Cannon01.damage = 10; 
		Cannon01.munition = "Cannonball";

		Cannon02.name = "Cannon02";
		Cannon02.gameObject = cannon02Prefab;
		Cannon02.price = 500;
		Cannon02.purchase = false;
		Cannon02.level = 0;
		Cannon02.health = 500;
		Cannon02.damage = 10; 
		Cannon02.munition = "Cannonball";
	
		//weapons list
		weaponsList.Add (Cannon01);
		weaponsList.Add (Cannon02);

		//munitions
		Cannonball.name = "Cannonball";
		Cannonball.gameObject = cannonballPrefab;
		Cannonball.originalPos = new Vector3 (0, 0, 0);

		//munitions list
		munitionsList.Add (Cannonball);

	}
}
