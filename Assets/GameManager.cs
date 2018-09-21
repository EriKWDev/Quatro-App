using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager _GameManager;
	public List<GameObject> prefabs = new List<GameObject>();
	
	private void Initiate() {
		_GameManager = this.GetComponent<GameManager>();
	}
}
