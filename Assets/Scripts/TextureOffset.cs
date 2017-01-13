using UnityEngine;
using System.Collections;

public class TextureOffset : MonoBehaviour {
	private float scrollSpeed = 0.1F;
	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}

	void Update() {
		float offset = Time.time * scrollSpeed;

		rend.material.SetTextureOffset("_MainTex", new Vector2(offset / 2, offset / -2));
	}
}