using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship02Details : MonoBehaviour {

	public Vector3[] weapon01Positions;
	public Vector3[] weapon02Positions;
	public Vector3[] crewPositions;
	public Vector3 topPosition;

	void Start () {
		weapon01Positions = new Vector3[6];
		weapon01Positions [0] = new Vector3 (-256f, 68.8f, 395.2f);
		weapon01Positions [1] = new Vector3 (-291f, 54f, 77.3f);
		weapon01Positions [2] = new Vector3 (-303f, 57f, 452f);
		weapon01Positions [3] = new Vector3 (-weapon01Positions[0].x, weapon01Positions[0].y, weapon01Positions[0].z);
		weapon01Positions [4] = new Vector3 (-weapon01Positions[0].x, weapon01Positions[0].y, weapon01Positions[0].z);
		weapon01Positions [5] = new Vector3 (-weapon01Positions[0].x, weapon01Positions[0].y, weapon01Positions[0].z);
	}

	void Update () {
		
	}
}
