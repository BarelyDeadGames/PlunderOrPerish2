using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShipSetup : MonoBehaviour {

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = new Vector3 (0, -2, 0);
		rb.useGravity = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
