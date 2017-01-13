using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public float speed;
	public GameObject cameraFollow;

	private GameObject ship;
	private Rigidbody rbChild;
	private Rigidbody rbThis;

	private PlayerSetup ps;

	void Start () {
		rbThis = GetComponent<Rigidbody> ();
		ps = GetComponent<PlayerSetup> ();

		if (!isLocalPlayer) {
			tag = "Enemy";

		} else {
			GameObject cf = Instantiate (cameraFollow);
			cf.transform.parent = this.transform;
		}
	}
	

	void Update () {


		if (!isLocalPlayer)
		{
			return;
		}

		if (ship == null) {
			ship = gameObject.transform.GetChild (0).gameObject;
		}
		if (rbChild == null) {
			rbChild = ship.GetComponent<Rigidbody> ();
		}

		//MOVEMENT------------------
		float translation = Input.GetAxis("Vertical") * speed;
		if (translation < 0) {
			translation = 0;
		}
		float rotation = Input.GetAxis ("Horizontal");
		//rb.AddForce (transform.forward * translation);

		rbThis.AddForce (transform.forward * translation);

		transform.Rotate (0, rotation / 5, 0);

		//ATTACK-------------------

		//FIRE LEFT
		if (Input.GetKeyDown(KeyCode.Q)) { 

			for (int i = 0; i < ps.weaponsLL_Left.Count; i++) {
				float y = i;
				y = y / 10;
				StartCoroutine(FireLeft(y, i));

			}

		}
		//FIRE RIGHT
		if (Input.GetKeyDown (KeyCode.E)) {

			for (int i = 0; i < ps.weaponsLL_Right.Count; i++) {
				float y = i;
				y = y / 10;
				StartCoroutine(FireRight(y, i));

			}
		}

	}

	IEnumerator FireLeft(float delay, int i) {

		yield return new WaitForSeconds(delay);

		if (ps.weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active == false) {
			ps.weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active = true;
			ps.weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			ps.weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().useGravity = true;
			ps.weaponsLL_Left [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().AddForce (transform.right*-20000 + transform.up*1000);
		}
	}

	IEnumerator FireRight(float delay, int i) {

		yield return new WaitForSeconds (delay);

		if (ps.weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active == false) {
			ps.weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<MunitionCannonball> ().active = true;
			ps.weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			ps.weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().useGravity = true;
			ps.weaponsLL_Right [i].gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().AddForce (transform.right*20000 + transform.up*1000);
		}

	}


	}