﻿using UnityEngine;
using System.Collections;

public class WorldOceanCollisons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.transform.tag == "CannonBall") {
			col.gameObject.SetActive (false);
		}
	}
}
