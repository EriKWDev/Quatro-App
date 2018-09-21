using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool selected = false;
	public Color selectedColor;
	Color startColor;
	Color lerpedColor;
	Renderer myRenderer;

	private void Start() {
		myRenderer = GetComponent<Renderer>();
		startColor = myRenderer.material.color;
		StartCoroutine("Fading");
	}

	private void Update() {
		if (selected) {
			myRenderer.material.color = Color.Lerp(myRenderer.material.color, lerpedColor, Time.deltaTime * 8f);
		} else {
			myRenderer.material.color = Color.Lerp(myRenderer.material.color, startColor, Time.deltaTime * 15f);
		}
	}

	public IEnumerator Fading() {
		while (true) {
			lerpedColor = selectedColor * startColor;
			yield return new WaitForSeconds(0.5f);
			lerpedColor = startColor;
			yield return new WaitForSeconds(0.5f);
		}
	}
}
