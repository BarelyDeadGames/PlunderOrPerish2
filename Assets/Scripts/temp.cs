using UnityEngine;
using System.Collections;

public class temp : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = new Vector3 (0, -2, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
