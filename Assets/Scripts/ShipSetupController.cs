using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSetupController : MonoBehaviour {

	private bool rotateLeft;
	private bool rotateRight;
	float l = 0;
	float r = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float Horizontal = Input.GetAxis ("Horizontal");
		transform.Rotate (0, Horizontal, 0);

		//Vector3 startPos = new Vector3(0,0,0);
		//Vector3 currentPos = new Vector3(0,0,0);
		//Vector3 endPos = new Vector3(0,0,0);
	
		/*
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				startPos = Input.GetTouch (i).position;
			}
			if (Input.GetTouch (i).phase == TouchPhase.Moved) {
				currentPos = Input.GetTouch (i).position;
			}
			if (Input.GetTouch (i).phase == TouchPhase.Ended) {
				endPos = Input.GetTouch (i).position;
				startPos = new Vector3(0,0,0);
				currentPos = new Vector3(0,0,0);
				endPos = new Vector3(0,0,0);
			}
		}
		float difference = (startPos.y - currentPos.y) / 100;
		transform.Rotate (0, difference, 0);
		*/

		if (rotateLeft == true) {
			if (l < 1) {
				l = l + 0.05f;
			}
			transform.Rotate (0, l, 0);
		} 
		if (rotateLeft == false) {
			if (l > 0) {
				l = l - 0.05f;
			}
			if (l < 0) {
				l = 0;
			}
		}

		if (rotateRight == true) {
			if (r > -1) {
				r = r - 0.05f;
			}
			transform.Rotate (0, r, 0);
		}
		if (rotateRight == false) {
			if (r < 0) {
				r = r + 0.05f;
			}
			if (r > 0) {
				r = 0;
			}
		}

		
	}

	public void RotateLeft() {
		rotateLeft = true;
	}

	public void RotateLeftUp() {
		rotateLeft = false;
	}

	public void RotateRight() {
		rotateRight = true;
	}

	public void RotateRightUp() {
		rotateRight = false;
	}
}
