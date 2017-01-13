using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerOfMass : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = new Vector3 (0, -1.5f, 0);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
