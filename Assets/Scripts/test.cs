using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	private Vector3 startPos;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			rb.useGravity = true;
			rb.velocity = new Vector3 (0, 0, 0);
			transform.position = startPos;
			Vector3 rndForce = new Vector3(Random.Range(-1000, -2000), Random.Range(100, 200), Random.Range(-500, 500));
			rb.AddForce (rndForce.x, rndForce.y, rndForce.z);
		}

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				rb.useGravity = true;
				rb.velocity = new Vector3 (0, 0, 0);
				transform.position = startPos;
				Vector3 rndForce = new Vector3(Random.Range(-1000, -2000), Random.Range(100, 200), Random.Range(-500, 500));
				rb.AddForce (rndForce.x, rndForce.y, rndForce.z);
			}
		}
	
	}
}
