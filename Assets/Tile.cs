using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	
	public enum Property {
		tall,
		small,
		cylinder,
		cube,
		hollow,
		solid,
		white,
		black,
		none
	}

	public List<Property> properties = new List<Property>();

	public bool selected = false;
	public bool selectable = true;
	public Color selectedColor;
	Color startColor;
	Color lerpedColor;
	Renderer myRenderer;
	public Vector3 bePosition;
	float smoothDampTime = 0.3f;
	Vector3 vel;

	private void Start() {
		myRenderer = GetComponent<Renderer>();
		startColor = myRenderer.material.color;
		StartCoroutine("Fading");
		bePosition = transform.position;
	}

	private void Update() {
		if (selected) {
			myRenderer.material.color = Color.Lerp(myRenderer.material.color, lerpedColor, Time.deltaTime * 8f);
		} else {
			myRenderer.material.color = Color.Lerp(myRenderer.material.color, startColor, Time.deltaTime * 15f);
		}

		transform.position = Vector3.SmoothDamp(transform.position, bePosition, ref vel, smoothDampTime);
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
