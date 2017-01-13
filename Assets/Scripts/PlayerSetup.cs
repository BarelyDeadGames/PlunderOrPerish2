using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BoatTutorial;

public class PlayerSetup : MonoBehaviour {

	//private GameObject[] ships;
	private GameObject playerShip;

	private int currentShip;

	//declare shared scripts
	private Ship01Details s1d;
	//private Ship02Details s2d;
	//private Ship03Details s3d;
	private GameObject gc;
	private PlayerDetails pd;
	private GameManager gm;
	private Weapons weaponsScript;
	private List<WeaponDetails> weaponsList;
	private List<MunitionDetails> munitionsList;

	private List<GameObject> weaponsLL_Left;
	private List<GameObject> weaponsLL_Right;

	public GameObject ps_ExplosionSmall;
	public List<GameObject> list_ps_ExplosionSmall = new List<GameObject>();

	private PlayerController pc;

	void Start () {
		//target shared scripts
		gc = GameObject.FindGameObjectWithTag("GameController");
		pd = gc.GetComponent<PlayerDetails>();
		gm = gc.GetComponent<GameManager>();
		weaponsScript = gc.GetComponent<Weapons> ();
		weaponsList = weaponsScript.weaponsList;
		munitionsList = weaponsScript.munitionsList;

		weaponsLL_Left = new List<GameObject> ();
		weaponsLL_Right = new List<GameObject> ();

		currentShip = pd.shipNumber;

		pc = GetComponent<PlayerController> ();

		LoadShip ();

	}

	void LoadShip() {

		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = new Vector3 (0, -1.5f, -0.5f);

		LoadWeapons ();
	}

	void LoadWeapons() {

		s1d = GetComponent<Ship01Details> ();
		for (int i = 0; i < pd.lowerLevelWeaponSlots.Count; i++) { //cycle weapon slots
			if (pd.lowerLevelWeaponSlots [i] != null) {
				Vector3 offsetPos = s1d.weaponLLPositions [i];
				//offsetPos.y = offsetPos.y;// + 3f;
				for (int z = 0; z < weaponsList.Count; z++) { //cycle and match weapon names
					if (weaponsList [z].name == pd.lowerLevelWeaponSlots [i].name) {
						GameObject weapon = Instantiate (weaponsList [z].gameObject, offsetPos, Quaternion.Euler (0, 0, 0));
						weapon.transform.parent = this.transform;
						if (i > 5 / 2) {
							weapon.transform.localScale = new Vector3 (-weapon.transform.localScale.x, weapon.transform.localScale.y, weapon.transform.localScale.z);
							//right weapons
							weaponsLL_Right.Add(weapon);
						} else {
							//left weapons
							weaponsLL_Left.Add(weapon);
						}
						//load particleEffects
						if (weaponsList [z].name == "Cannon01") {
							GameObject ps_es = Instantiate (ps_ExplosionSmall);
							ps_es.transform.parent = GameObject.FindGameObjectWithTag ("ParticleSystems").transform;
							list_ps_ExplosionSmall.Add (ps_es);
						}
						//load munitions
						for (int y = 0; y < munitionsList.Count; y++) { //cycle and match munitions
							//print ("m: " + munitionsList [y].name);
							//print ("w: " + weaponsList [z].munition);
							if (munitionsList [y].name == weaponsList [z].munition) {
								for (int x = 0; x < 3; x++) { //add munitions * amount
									GameObject munition = Instantiate (munitionsList [y].gameObject, offsetPos, Quaternion.Euler (0, 0, 0)) as GameObject;
									munition.transform.parent = weapon.transform; //parent munition to weapon
									MunitionCannonball mc = munition.GetComponent<MunitionCannonball> ();
								}
							}
						}
					}
				}
			}
		}
	}

	void Update() {
		//FIRE LEFT
		if (Input.GetKeyDown(KeyCode.Q)) { 

			for (int i = 0; i < weaponsLL_Left.Count; i++) {
				float y = i;
				y = y / 10;
				StartCoroutine(FireLeft(y, i));

			}

		}
		//FIRE RIGHT
		if (Input.GetKeyDown (KeyCode.E)) {

			for (int i = 0; i < weaponsLL_Right.Count; i++) {
				float y = i;
				y = y / 10;
				StartCoroutine(FireRight(y, i));

			}
		}

	}

	IEnumerator FireLeft(float delay, int i) {

		yield return new WaitForSeconds(delay);

		if (weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active == false) {
			weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active = true;
			weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().useGravity = true;
			weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().AddForce (transform.right*-20000 + transform.up*1000);
		}
	}

	IEnumerator FireRight(float delay, int i) {

		yield return new WaitForSeconds (delay);

		if (weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active == false) {
			weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active = true;
			weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().useGravity = true;
			weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().AddForce (transform.right*20000 + transform.up*1000);
		}

	}


}


	/*
	public GameObject CannonBall;
	public GameObject Cannon1, Cannon2, Cannon3, Cannon4;
	private Rigidbody rb;

	private List<GameObject> Cannons;
	private List<GameObject> CannonBalls;
	public List<bool> ActiveBalls;
	public List<Vector3> OriginalPos;

	public float speed;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = new Vector3 (0, -2, 0);
		rb.useGravity = true;

		Cannons = new List<GameObject>{ Cannon1, Cannon2, Cannon3, Cannon4 };
		CannonBalls = new List<GameObject> ();
		ActiveBalls = new List<bool>{ false, false, false, false };
		OriginalPos = new List<Vector3> ();

		for (int i = 0; i < Cannons.Count; i++) {
			GameObject cball = Instantiate (CannonBall);
			cball.transform.position = Cannons [i].transform.position;
			OriginalPos.Add (cball.transform.position);
			CannonBalls.Add (cball);
			cball.transform.parent = this.transform;
		}
	}


	void Update () {
	
		//fire cannonballs
		if (Input.GetMouseButtonDown (0)) {
			for (int i = 0; i < CannonBalls.Count; i++) {
				if (ActiveBalls[i] == false) {
					Rigidbody cball_rb = CannonBalls[i].GetComponent<Rigidbody>();
					cball_rb.AddForce (Cannons [i].transform.forward * -25000);
					cball_rb.constraints = RigidbodyConstraints.None;

					//FauxGravityBody cball_fgb = CannonBalls [i].GetComponent<FauxGravityBody> ();
					//cball_fgb.enabled = true;

					ActiveBalls [i] = true;
					break;
				}
			}
		}


	}
}
*/