using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

	private GameObject cameraFollow;
	private GameObject playerShip;
	private Vector3 velocity = Vector3.zero;


	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraFollow == null) {
			cameraFollow = GameObject.FindGameObjectWithTag("CameraFollow");
		}
		if (playerShip == null) {
			playerShip = GameObject.FindGameObjectWithTag ("PlayerShip");
		}
			
		transform.position = Vector3.SmoothDamp(transform.position, cameraFollow.transform.position, ref velocity, 1);
		//transform.rotation = cameraFollow.transform.rotation;
		//transform.rotation = Quaternion.Slerp(transform.rotation, cameraFollow.transform.rotation, 2);
		transform.LookAt (playerShip.transform);

	}
}
