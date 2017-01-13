using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship01Details : MonoBehaviour {

	public int health = 1000;
	public Vector3[] weaponLLPositions;
	public Vector3[] weaponULPositions;
	public Vector3[] crewPositions;
	public Vector3[] otherPositions;

	void Start () {
		weaponLLPositions = new Vector3[6];
		weaponLLPositions [0] = new Vector3 (-2.56f, .688f, 3.952f);
		weaponLLPositions [1] = new Vector3 (-2.91f, .54f, .773f);
		weaponLLPositions [2] = new Vector3 (-3.03f, .57f, -4.52f);
		weaponLLPositions [3] = new Vector3 (-weaponLLPositions[0].x, weaponLLPositions[0].y, weaponLLPositions[0].z);
		weaponLLPositions [4] = new Vector3 (-weaponLLPositions[1].x, weaponLLPositions[1].y, weaponLLPositions[1].z);
		weaponLLPositions [5] = new Vector3 (-weaponLLPositions[2].x, weaponLLPositions[2].y, weaponLLPositions[2].z);

		weaponULPositions = new Vector3[6];
		weaponULPositions [0] = new Vector3 (-2.56f, 1.688f, 3.952f);
		weaponULPositions [1] = new Vector3 (-2.91f, 1.54f, .773f);
		weaponULPositions [2] = new Vector3 (-3.03f, 1.57f, -4.52f);
		weaponULPositions [3] = new Vector3 (-weaponULPositions[0].x, weaponULPositions[0].y, weaponULPositions[0].z);
		weaponULPositions [4] = new Vector3 (-weaponULPositions[1].x, weaponULPositions[1].y, weaponULPositions[1].z);
		weaponULPositions [5] = new Vector3 (-weaponULPositions[2].x, weaponULPositions[2].y, weaponULPositions[2].z);

		crewPositions = new Vector3[6];
		crewPositions [0] = new Vector3 (-2.56f, 1.688f, 3.952f);
		crewPositions [1] = new Vector3 (-2.91f, 1.54f, .773f);
		crewPositions [2] = new Vector3 (-3.03f, 1.57f, -4.52f);
		crewPositions [3] = new Vector3 (-crewPositions[0].x, crewPositions[0].y, crewPositions[0].z);
		crewPositions [4] = new Vector3 (-crewPositions[1].x, crewPositions[1].y, crewPositions[1].z);
		crewPositions [5] = new Vector3 (-crewPositions[2].x, crewPositions[2].y, crewPositions[2].z);

	}

	void Update () {
		
	}
}
