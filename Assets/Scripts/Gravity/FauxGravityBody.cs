using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
public class FauxGravityBody : MonoBehaviour {

	FauxGravityAttractor planet;
	Rigidbody rb;

	void Awake () {
		//planet = GameObject.FindGameObjectWithTag("WorldOcean").GetComponent<FauxGravityAttractor>();
		rb = GetComponent<Rigidbody> ();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		rb.useGravity = false;
		//rb.constraints = RigidbodyConstraints.FreezeRotationY;
		if (rb.transform.tag == "CannonBall") {
			rb.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity
		//planet.Attract(rb);
	}
}
	

