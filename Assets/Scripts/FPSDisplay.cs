using UnityEngine;
using System.Collections;
//using UnityStandardAssets.ImageEffects;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;

	void Awake() {
		//#if UNITY_ANDROID || UNITY_IOS

		//DepthOfField dof = GetComponent<DepthOfField>();
		//dof.enabled = false;

		//#endif

		//fps limit
		Application.targetFrameRate = 60;

	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		//style.normal.textColor = new Color (1.0f, .0f, .0f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		if (fps > 50) {
			//green
			style.normal.textColor = new Color (0.0f, 1.0f, 0f, 1.0f);
		}
		if (fps > 40 && fps <= 50) {
			//orange
			style.normal.textColor = new Color (1.0f, 1.0f, 0f, 1.0f);
		}
		if (fps <= 30) {
			//red
			style.normal.textColor = new Color (1.0f, 0.0f, 0.0f, 1.0f);
		}
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}

	//green (0.0f, 1.0f, 0f, 1.0f);
	//yellow (1.0f, 1.0f, 0f, 1.0f);
	//red (1.0f, 0.0f, 0.0f, 1.0f);
}