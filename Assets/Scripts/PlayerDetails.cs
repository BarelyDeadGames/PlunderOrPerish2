using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class PlayerDetails : MonoBehaviour {


	public int lives;
	public int gold;
	public int shipNumber;

	public List<GameObject> inventory = new List<GameObject> ();
	public List<GameObject> lowerLevelWeaponSlots = new List<GameObject>();
	public List<GameObject> upperLevelWeaponSlots = new List<GameObject>();
	public List<GameObject> otherWeapons = new List<GameObject> ();
	public List<GameObject> crew = new List<GameObject> ();
	public List<string> int_names = new List<string> ();
	// Use this for initialization
	void Start () {
		int_names.Add ("PlayerLives");
		int_names.Add ("PlayerGold");
		int_names.Add ("ShipNumber");

		GameManager gm = GetComponent<GameManager> ();

		foreach (GameObject value in gm.items_go.Values) {
			lowerLevelWeaponSlots.Add (value);
		}


		for (int i = 0; i < int_names.Count; i++) {
			int temp;
			if (gm.items_int.TryGetValue (int_names[i], out temp)) {
				if (int_names [i] == "PlayerLives") {
					lives = temp;
				}
				if (int_names [i] == "PlayerGold") {
					gold = temp;
				}
				if (int_names [i] == "ShipNumber") {
					shipNumber = temp;
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}