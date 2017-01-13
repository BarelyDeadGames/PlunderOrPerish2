using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;
	
public class GameManager : MonoBehaviour {

	public float gameVersion = 0.1f;

	private List<GameObject> Players = new List<GameObject>();
	private string path;
	public string fileName;
	public Camera myCam;
	private GameObject previousPlayer;

	//------
	//	private Dictionary<string, GameObject> weaponsLookup = new Dictionary<string, GameObject>();
	private XmlNode[] loadPlayerDetails;

	public Dictionary<string, int> items_int = new Dictionary<string, int> ();
	public Dictionary<string, GameObject> items_go = new Dictionary<string, GameObject> ();

	//weapons script
	private Weapons weaponsScript;
	List<WeaponDetails> weaponsList;

	void Start () {

		weaponsScript = GetComponent<Weapons> ();
		weaponsList = weaponsScript.weaponsList;
		//weapons script

		items_int.Add ("PlayerGold", 1000);
		items_int.Add ("PlayerLives", 5);
		items_int.Add ("ShipNumber", 0);
		items_go.Add ("LLWeaponSlot01", null);
		items_go.Add ("LLWeaponSlot02", weaponsList[0].gameObject);
		items_go.Add ("LLWeaponSlot03", null);
		items_go.Add ("LLWeaponSlot04", null);
		items_go.Add ("LLWeaponSlot05", weaponsList[0].gameObject);
		items_go.Add ("LLWeaponSlot06", null);
		items_go.Add ("LLWeaponSlot07", null);
		items_go.Add ("LLWeaponSlot08", null);
		items_go.Add ("LLWeaponSlot09", null);
		items_go.Add ("LLWeaponSlot10", null);

		/*
		weaponsLookup.Add ("Cannon01", cannon01);
		weaponsLookup.Add ("FlameThrower", flameThrower);
*/
		loadPlayerDetails = new XmlNode[13];
		Load ();
	}

	/*
	public void LoadGameScene() {
		SceneManager.LoadScene ("GameScene");
	}*/

	public void Load()
	{
		if (System.IO.File.Exists (getPath ())) {
			path = getPath ();

			XmlReader reader = XmlReader.Create (path);
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (reader);
			XmlNodeList Data = xmlDoc.GetElementsByTagName ("Data");
			for (int i = 0; i < Data.Count; i++) {
				// getting data
				XmlNode DataChilds = Data.Item (i); 

				// getting all gameObjects stored inside data
				XmlNodeList allGameObjects = DataChilds.ChildNodes;


				for (int j = 0; j < allGameObjects.Count; j++) {
					XmlNode game_Objects = allGameObjects.Item (j); //get outer tags

					if (game_Objects.Name == "GameDetails") { 

					}
					if (game_Objects.Name == "PlayerDetails") {

						XmlNodeList xmlPlayerDetails = game_Objects.ChildNodes; //convert inner tags into a xml list
						for (int n = 0; n < loadPlayerDetails.Length; n++){
							loadPlayerDetails [n] = xmlPlayerDetails.Item (n); //convert to xmlnode list
							string st = loadPlayerDetails [n].InnerText; //store tag values as string
							int num = -666; //create abritrary int, chose a number that is not 0 as we may need to parse 0
							bool parsed = int.TryParse (st, out num); //check if parsed with bool
							if (parsed) { //save parsed into correct int
								items_int[loadPlayerDetails[n].Name] = num;
							} else { //use string to look up gameobject
							//	GameObject temp;
							//if(weaponsLookup.TryGetValue(st, out temp)) {
							//	items_go [loadPlayerDetails [n].Name] = temp;
							//}
								for (int num2 = 0; num2 < weaponsList.Count; num2++) {
									if (weaponsList[num2].name == st) {
										items_go [loadPlayerDetails[n].Name] = weaponsList[num2].gameObject;
									}
								}
							}

						}
					}
				}
			}
			reader.Close ();
		} else {
			print ("File does not exist");
			Save ();
		}
	}

	public void CopyNewDetails() {
		PlayerDetails pd;
		pd = GetComponent<PlayerDetails> ();

		for (int i = 0; i < pd.int_names.Count; i++) {
			int temp;
			if (items_int.TryGetValue (pd.int_names[i], out temp)) {
				if (pd.int_names [i] == "PlayerLives") {
					items_int [pd.int_names [i]] = pd.lives;
				}
				if (pd.int_names [i] == "PlayerGold") {
					items_int [pd.int_names [i]] = pd.gold;
				}
				if (pd.int_names [i] == "ShipNumber") {
					items_int [pd.int_names [i]] = pd.shipNumber;
				}
			}
		}

		int num2 = 0;
		List<string> keys2 = new List<string> (items_go.Keys);
		foreach (string key in keys2) {
			items_go[key] = pd.lowerLevelWeaponSlots[num2];
			num2++;
		}

		Save ();
	}

	public void Save(){


		path = getPath();
		XmlDocument xmlDoc = new XmlDocument();

		XmlElement elmRoot = xmlDoc.CreateElement("Data");
		xmlDoc.AppendChild(elmRoot);
		// dont change until here

		//GAME DETAILS
		string name1 = "GameDetails";
		XmlElement Game_Details = xmlDoc.CreateElement (name1);

		XmlElement Game_Version = xmlDoc.CreateElement ("GameVersion");
		//Game_Version.InnerText = "0";
		Game_Version.InnerText = gameVersion.ToString ();
		Game_Details.AppendChild (Game_Version);
		elmRoot.AppendChild (Game_Details);

		//PLAYER DETAILS
		string name2 = "PlayerDetails";
		XmlElement Player_Details = xmlDoc.CreateElement(name2);

		XmlElement Player_Gold = xmlDoc.CreateElement ("PlayerGold");
		//Player_Gold.InnerText = "1000";
		Player_Gold.InnerText = items_int["PlayerGold"].ToString();

		XmlElement Player_lives = xmlDoc.CreateElement ("PlayerLives");
		//Player_lives.InnerText = "5";
		Player_lives.InnerText = items_int["PlayerLives"].ToString();

		XmlElement Player_ShipNum = xmlDoc.CreateElement("ShipNumber");
		//Player_ShipNum.InnerText = "0";
		Player_ShipNum.InnerText = items_int["ShipNumber"].ToString();

		XmlElement Player_LLW01 = xmlDoc.CreateElement("LLWeaponSlot01");
		XmlElement Player_LLW02 = xmlDoc.CreateElement("LLWeaponSlot02");
		XmlElement Player_LLW03 = xmlDoc.CreateElement("LLWeaponSlot03");
		XmlElement Player_LLW04 = xmlDoc.CreateElement("LLWeaponSlot04");
		XmlElement Player_LLW05 = xmlDoc.CreateElement("LLWeaponSlot05");
		XmlElement Player_LLW06 = xmlDoc.CreateElement("LLWeaponSlot06");
		XmlElement Player_LLW07 = xmlDoc.CreateElement("LLWeaponSlot07");
		XmlElement Player_LLW08 = xmlDoc.CreateElement("LLWeaponSlot08");
		XmlElement Player_LLW09 = xmlDoc.CreateElement("LLWeaponSlot09");
		XmlElement Player_LLW10 = xmlDoc.CreateElement("LLWeaponSlot10");


		if (items_go ["LLWeaponSlot01"] != null) {
			Player_LLW01.InnerText = items_go ["LLWeaponSlot01"].name;
		} else {
			Player_LLW01.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot02"] != null) {
			Player_LLW02.InnerText = items_go ["LLWeaponSlot02"].name;
		} else {
			Player_LLW02.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot03"] != null) {
			Player_LLW03.InnerText = items_go ["LLWeaponSlot03"].name;
		} else {
			Player_LLW03.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot04"] != null) {
			Player_LLW04.InnerText = items_go ["LLWeaponSlot04"].name;
		} else {
			Player_LLW04.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot05"] != null) {
			Player_LLW05.InnerText = items_go ["LLWeaponSlot05"].name;
		} else {
			Player_LLW05.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot06"] != null) {
			Player_LLW06.InnerText = items_go ["LLWeaponSlot06"].name;
		} else {
			Player_LLW06.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot07"] != null) {
			Player_LLW07.InnerText = items_go ["LLWeaponSlot07"].name;
		} else {
			Player_LLW07.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot08"] != null) {
			Player_LLW08.InnerText = items_go ["LLWeaponSlot08"].name;
		} else {
			Player_LLW08.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot09"] != null) {
			Player_LLW09.InnerText = items_go ["LLWeaponSlot09"].name;
		} else {
			Player_LLW09.InnerText = "null";
		}
		if (items_go ["LLWeaponSlot10"] != null) {
			Player_LLW10.InnerText = items_go ["LLWeaponSlot10"].name;
		} else {
			Player_LLW10.InnerText = "null";
		}

		Player_Details.AppendChild (Player_Gold);
		Player_Details.AppendChild (Player_lives);
		Player_Details.AppendChild(Player_ShipNum);
		Player_Details.AppendChild(Player_LLW01);
		Player_Details.AppendChild(Player_LLW02);
		Player_Details.AppendChild(Player_LLW03);
		Player_Details.AppendChild(Player_LLW04);
		Player_Details.AppendChild(Player_LLW05);
		Player_Details.AppendChild(Player_LLW06);
		Player_Details.AppendChild(Player_LLW07);
		Player_Details.AppendChild(Player_LLW08);
		Player_Details.AppendChild(Player_LLW09);
		Player_Details.AppendChild(Player_LLW10);

		elmRoot.AppendChild(Player_Details);

		//dont change from here
		StreamWriter outStream = System.IO.File.CreateText(path);

		xmlDoc.Save(outStream);
		outStream.Close();

		print ("Saved");
		Load ();
	}

	// Following method is used to retrive the relative path as device platform
	private string getPath(){
		#if UNITY_EDITOR
		return Application.dataPath +"/Resources/"+fileName;
		#elif UNITY_ANDROID
		return Application.persistentDataPath+fileName;
		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+fileName;
		#else
		return Application.dataPath +"/"+ fileName;
		#endif
	}
}
