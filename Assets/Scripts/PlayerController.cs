using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public float speed;

	private GameObject ship;
	private Rigidbody rbChild;
	private Rigidbody rbThis;

	void Start () {
		rbThis = GetComponent<Rigidbody> ();
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

		float translation = Input.GetAxis("Vertical") * speed;
		if (translation < 0) {
			translation = 0;
		}
		float rotation = Input.GetAxis ("Horizontal");
		//rb.AddForce (transform.forward * translation);

		rbThis.AddForce (transform.forward * translation);

		transform.Rotate (0, rotation / 5, 0);

		//rbThis.AddTorque(transform.up * rotation * 500000);
		
	}
}
