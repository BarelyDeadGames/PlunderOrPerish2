using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSetup : MonoBehaviour {

	public GameObject ship01Prefab;
	public GameObject ship02Prefab;
	public GameObject ship03Prefab;

	private GameObject[] ships;

	private PlayerDetails pd;

	private int currentShip;
	private GameObject ship;

	private Ship01Details s1d;
	private Ship02Details s2d;
	//private Ship03Details s3d;
	//----

	private bool initialInstantiate = true;
	private int currentWeaponPos = 100;
	private GameObject currentWeaponPurchase;
	private string currentWeaponPurchase_string;

	//Hud items
	private GameObject Hud;
	private GameObject weaponSelectionOverlay;
	private GameObject buttonCloseWeaponSelection;
	private GameObject weaponSelectionParent;
	private GameObject text_AvailableGold;
	private Text availableGoldText;
	private GameObject button_messageBox_OK;
	private GameObject button_messageBox_OK_Text;
	private Text button_messageBox_OK_TextBox;
	private Vector3 bmb_ok;
	private GameObject button_messageBox_Cancel;
	private GameObject button_messageBox_Cancel_Text;
	private Text button_messageBox_Cancel_TextBox;
	private Vector3 bmb_cancel;
	private GameObject overlay2;

	private GameObject currentWeapon;

	private GameObject inventory01;
	private GameObject inventory02;
	private GameObject inventory03;
	private GameObject inventory04;
	private GameObject inventory05;
	private GameObject inventory06;

	private GameObject purchaseCannon01;
	//end hud items
	private bool hasWeaponInCurrentSlot;
	private bool updateDetails = true;

	private Dictionary<string, bool> selectionsActive = new Dictionary<string, bool> ();

	public int activeSelection = 0;

	private GameObject[] selectionSquaresAllLL;
	private GameObject[] weapon01Handles;
	private GameObject[] selectionSquaresLL;
	private List<GameObject> unusedselectionSquares;

	private GameManager gm;
	//weapons script
	private Weapons weaponsScript;
	List<WeaponDetails> weaponsList;

	void Start () {

		weaponsScript = GetComponent<Weapons> ();
		weaponsList = weaponsScript.weaponsList;
		//weapons script
		gm = GetComponent<GameManager> ();

		selectionsActive.Add ("LowerLevel", true);
		selectionsActive.Add ("UpperLevel", false);
		selectionsActive.Add ("Crew", false);
		selectionsActive.Add ("Other", false);

		unusedselectionSquares = new List<GameObject> ();

		ships = new GameObject[3];
		ships [0] = ship01Prefab;
		ships [1] = ship02Prefab;
		ships [2] = ship03Prefab;

		pd = GetComponent<PlayerDetails> ();
		currentShip = pd.shipNumber;

		ship = Instantiate (ships [currentShip], transform.position = new Vector3(0,0,0), Quaternion.Euler(0,0,0));
		Rigidbody rb = ship.GetComponent<Rigidbody> ();
		rb.useGravity = false;

		Hud = GameObject.FindGameObjectWithTag("HUD");
		weaponSelectionOverlay = GameObject.FindGameObjectWithTag ("WeaponSelectionOverlay");
		weaponSelectionOverlay.SetActive (false);
		buttonCloseWeaponSelection = GameObject.FindGameObjectWithTag ("CloseWeaponSelection");
		buttonCloseWeaponSelection.SetActive (false);
		text_AvailableGold = GameObject.Find ("AvailableGold");
		availableGoldText = text_AvailableGold.GetComponent<Text> ();

		button_messageBox_OK = GameObject.Find ("Button_MessageBox_OK");
		button_messageBox_OK_Text = button_messageBox_OK.transform.GetChild (0).gameObject;
		button_messageBox_OK_TextBox = button_messageBox_OK_Text.GetComponent<Text> ();
		bmb_ok = button_messageBox_OK.GetComponent<RectTransform>().position;
		button_messageBox_OK.SetActive (false);

		button_messageBox_Cancel = GameObject.Find ("Button_MessageBox_Cancel");
		button_messageBox_Cancel_Text = button_messageBox_Cancel.transform.GetChild (0).gameObject;
		button_messageBox_Cancel_TextBox = button_messageBox_Cancel_Text.GetComponent<Text> ();
		bmb_cancel = button_messageBox_Cancel.GetComponent<RectTransform> ().position;
		button_messageBox_Cancel.SetActive (false);

		overlay2 = GameObject.Find ("Overlay2");
		overlay2.SetActive (false);

		weaponSelectionParent = GameObject.Find ("WeaponSelectionParent");
		currentWeapon = GameObject.Find ("CurrentWeapon");

		weaponSelectionParent.SetActive (false);


		if (currentShip == 0) {
			s1d = GetComponent<Ship01Details> ();
			SetupShip01 ();
		}
		/*	
		if (currentShip == 1) {
			s2d = GetComponent<Ship02Details> ();
			SetupShip02();
		}
		*/
		/*
		if (currentShip == 2) {
			s3d = GetComponent<Ship03Details> ();
			SetupShip03();
		}
		*/

	}

	void SetupShip01(){

		int arrayLength = s1d.weaponLLPositions.Length;
		weapon01Handles = new GameObject[arrayLength];

		for (int i = 0; i < arrayLength; i++) {
			Vector3 pos = new Vector3 (s1d.weaponLLPositions [i].x, s1d.weaponLLPositions [i].y, s1d.weaponLLPositions [i].z);
			//GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			GameObject empty = new GameObject ("WeaponPos");
			//sphere.transform.position = pos;
			empty.transform.position = pos;
			//SphereCollider sc = sphere.GetComponent<SphereCollider> ();
			//sc.enabled = false;
			//sphere.transform.parent = ship.transform;
			empty.transform.parent = ship.transform;
			//weapon01Handles [i] = sphere;
			weapon01Handles [i] = empty;

		};

		//all LL selection squares
		selectionSquaresAllLL = new GameObject[10];
		for (int i = 0; i < selectionSquaresAllLL.Length; i++) {
			GameObject LLcontainer = GameObject.FindGameObjectWithTag ("WeaponSelectionLL");
			GameObject sel = LLcontainer.transform.GetChild (i).gameObject;
			selectionSquaresAllLL [i] = sel;
		}

		//used lover level
		selectionSquaresLL = new GameObject[arrayLength];
		for (int i = 0; i < selectionSquaresLL.Length; i++) {
			selectionSquaresLL [i] = selectionSquaresAllLL [i];
			Image im = selectionSquaresLL [i].GetComponent<Image> ();
			if (pd.lowerLevelWeaponSlots [i] != null) {
				im.color = new Color32 (0, 160, 0, 200);
			} else {
				im.color = new Color32 (0, 0, 0, 200);
			}

		}

		//unused all
		for (int i = selectionSquaresLL.Length; i < selectionSquaresAllLL.Length; i++) {
			unusedselectionSquares.Add(selectionSquaresAllLL [i]);
			int v = i - selectionSquaresLL.Length;
			unusedselectionSquares [v].SetActive (false);
		}

		InstantiateWeapons ();
	
	}

	void SetupShip02() {

	}

	void SetupShip03() {

	}

	void InstantiateWeapons() {

		if (!initialInstantiate) {
			GameObject cannonNew = Instantiate (currentWeaponPurchase, s1d.weaponLLPositions [currentWeaponPos], Quaternion.Euler (0, 0, 0));
				cannonNew.transform.parent = ship.transform;
			if (currentWeaponPos > selectionSquaresLL.Length / 2) {
					cannonNew.transform.localScale = new Vector3 (-1, 1, 1);
				}
			//pd.lowerLevelWeaponSlots [currentWeaponPos] = currentWeaponPurchase;

			//add weapon to player details
			foreach (KeyValuePair<string, bool> kvp in selectionsActive) {
				if (kvp.Value == true) {
					if (kvp.Key == "LowerLevel") {
						pd.lowerLevelWeaponSlots [currentWeaponPos] = currentWeaponPurchase;
						break;
					}
					if (kvp.Key == "UpperLevel") {

						break;
					}
					if (kvp.Key == "Other") {

						break;
					}
					if (kvp.Key == "Crew") {

						break;
					}
					if (kvp.Key == "Inventory") {

						break;
					}
				}

				//turn off purchase bools
				for (int i = 0; i < weaponsList.Count; i++) {
					weaponsList [0].purchase = false;
				}
			}
			gm.CopyNewDetails ();
			toggleWeaponSelection (100);

		} else {
			for (int i = 0; i < selectionSquaresLL.Length; i++) {
				if (pd.lowerLevelWeaponSlots [i] != null) {
					
					GameObject cannon = Instantiate (pd.lowerLevelWeaponSlots [i].gameObject, s1d.weaponLLPositions [i], Quaternion.Euler (0, 0, 0));
					cannon.transform.parent = ship.transform;
					if (i > selectionSquaresLL.Length / 2) {
						cannon.transform.localScale = new Vector3 (-1, 1, 1);
					}
				}
			}
			initialInstantiate = false;
		}

	}
		
	public void deselectAll(int num) {
		activeSelection = num;
		for (int i = 0; i < selectionSquaresLL.Length; i++) {
			selectionSquaresLL [i].SetActive (false);
		}
		weaponSelectionOverlay.SetActive (false);

		toggleLevelSelection ();
	}


	void toggleLevelSelection() {
		switch (activeSelection)
		{
		case 4: //other level

			break;
		case 3: //crew level

			break;
		case 2: //upper level

			break;
		case 1: //lower level
			for (int i = 0; i < selectionSquaresLL.Length; i++) {
				selectionSquaresLL [i].SetActive (true);
			}
			break;
		default: //lower level
			for (int i = 0; i < selectionSquaresLL.Length; i++) {
				selectionSquaresLL [i].SetActive (true);
			}
			break;
		}
	}

	public void toggleWeaponSelection(int weaponPos) {
		currentWeaponPos = weaponPos;

		if (weaponSelectionOverlay.activeSelf == false) {
			buttonCloseWeaponSelection.SetActive (true);
			weaponSelectionOverlay.SetActive (true);
			weaponSelectionParent.SetActive (true);

			GameObject cwText = currentWeapon.transform.GetChild (0).gameObject;
			Text text = cwText.GetComponent<Text> ();

			if (pd.lowerLevelWeaponSlots [weaponPos] != null) {
				text.text = "" + pd.lowerLevelWeaponSlots [weaponPos].name;
				hasWeaponInCurrentSlot = true;
				//Button b = cwText.GetComponent<Button> ();
				//b.enabled = false;
			} else { 
				text.text = "Empty";
				hasWeaponInCurrentSlot = false;
				//Button b = cwText.GetComponent<Button> ();
				//b.enabled = false;
			}
				
			return;
		} else if (weaponSelectionOverlay.activeSelf == true) {
			buttonCloseWeaponSelection.SetActive (false);
			weaponSelectionOverlay.SetActive (false);
			weaponSelectionParent.SetActive (false);
		}


	}

	public void PurchaseAsset(string weapon) {

		int num = 0;
		for (int i = 0; i < weaponsList.Count; i++) {
			if (weaponsList [i].name == weapon) {
				currentWeaponPurchase = weaponsList [i].gameObject;
				currentWeaponPurchase_string = weapon;
				num = i;
				break;
			}
		}

		if (hasWeaponInCurrentSlot) {
			overlay2.SetActive (true);
			button_messageBox_OK.SetActive (true);
			button_messageBox_OK.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
			button_messageBox_OK_TextBox.text = "Cannot purchase. \n Add item to inventory \n to free up slot \n \n <OK>";
		} else {
			overlay2.SetActive (true);
			button_messageBox_OK.SetActive (true);
			button_messageBox_OK_TextBox.text = "Purchase item \n <$" + weaponsList[num].price + ">";
			button_messageBox_Cancel.SetActive (true);
			button_messageBox_Cancel_TextBox.text = "Cancel\nPurchase";
			weaponsList [num].purchase = true; //set selected weapon purchase bool
		}
	}

	public void CloseButtonMessageBox_Both(int i) {
		button_messageBox_OK.GetComponent<RectTransform> ().position = bmb_ok;
		button_messageBox_OK.SetActive (false);
		button_messageBox_Cancel.GetComponent<RectTransform> ().position = bmb_cancel;
		button_messageBox_Cancel.SetActive (false);
		overlay2.SetActive (false);
		if (i == 0) { //0 == OK
			for (int num = 0; num < weaponsList.Count; num++ ){
				if (weaponsList[num].purchase == true) {
					pd.gold = pd.gold - weaponsList[num].price;
					updateDetails = true;
					InstantiateWeapons();
					break;
				}
			}
		}
		if (i == 1) { //1 == Cancel

		}
	}

	void Update () {

		if (updateDetails == true) {
			updateDetails = false;
			availableGoldText.text = "Gold: " + pd.gold;

			for (int i = 0; i < selectionSquaresLL.Length; i++) {
				selectionSquaresLL [i] = selectionSquaresAllLL [i];
				Image im = selectionSquaresLL [i].GetComponent<Image> ();
				if (pd.lowerLevelWeaponSlots [i] != null) {
					im.color = new Color32 (0, 160, 0, 200);
				} else {
					im.color = new Color32 (0, 0, 0, 200);
				}

			}
			//GameManager gm = GetComponent<GameManager> ();
			//gm.Save ();
		}
			
	

		for (int i = 0; i < weapon01Handles.Length; i++) {
			//position
			RectTransform sqRT = selectionSquaresLL [i].GetComponent<RectTransform> ();
			RectTransform hudRT = Hud.GetComponent<RectTransform> ();

			Vector2 ViewportPosition=Camera.main.WorldToViewportPoint(weapon01Handles [i].transform.position);
			Vector2 WorldObject_ScreenPosition=new Vector2(
				((ViewportPosition.x*hudRT.sizeDelta.x)-(hudRT.sizeDelta.x*0.5f)),
				((ViewportPosition.y*hudRT.sizeDelta.y)-(hudRT.sizeDelta.y*0.5f)));

			sqRT.anchoredPosition=WorldObject_ScreenPosition;

			//scale
			Transform sq = weapon01Handles[i].gameObject.transform;
			float dist = Vector3.Distance(sq.position, Camera.main.transform.position);
			sqRT.localScale = new Vector3(26, 26, 26) / dist;
		}

	}

}
