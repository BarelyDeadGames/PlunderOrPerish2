using UnityEngine;
using System.Collections;

public class FauxGravityAttractor : MonoBehaviour {

	public float gravity = -9.8f;


	public void Attract(Rigidbody body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;

		Vector3 localUp = body.transform.up;

		// Apply downwards gravity to body
		body.AddForce(gravityUp * gravity * (body.mass / 2)); //50 000 static for ship
		// Allign bodies up axis with the centre of planet
		body.rotation = Quaternion.FromToRotation(localUp,gravityUp) * body.rotation;
	}  
}