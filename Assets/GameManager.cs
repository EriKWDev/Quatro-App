using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager _GameManager;
	public List<GameObject> prefabs = new List<GameObject>();
	
	private void Initiate() {
		_GameManager = this.GetComponent<GameManager>();
	}

	private void SetPropertiesBasedOnNames (List<GameObject> _prefabs) {
		foreach (GameObject o in _prefabs) {
			o.GetComponent<Tile>().properties = new List<Tile.Property>();
			foreach (string s in o.name.Split(' '))  {
				o.GetComponent<Tile>().properties.Add ((Tile.Property)System.Enum.Parse(typeof(Tile.Property), s.ToLower ()));
			}
		}
	}

	private void Start() {
		SetPropertiesBasedOnNames(prefabs);
	}

	public bool CheckWin (Dictionary<string, Tile> board) {
		try {
			if (CheckProperties(board["00"].properties, board["11"].properties, board["22"].properties, board["33"].properties)) {
				return true;
			}
		} catch { }
		
		try {
			if (CheckProperties(board["03"].properties, board["12"].properties, board["21"].properties, board["30"].properties)) {
				return true;
			}
		} catch { }
		
		for (int i = 0; i < 4; i++) {
			try {
				if (CheckProperties(board[i + "0"].properties, board[i + "1"].properties, board[i + "2"].properties, board[i + "3"].properties)) {
					return true;
				}
			} catch { }
			try {
				if (CheckProperties(board["0" + i].properties, board["0" + i].properties, board["0" + i].properties, board["0" + i].properties)) {
					return true;
				}
			}
			catch { }
		}

		return false;
	}

	public bool CheckProperties(List<Tile.Property> ps1, List<Tile.Property> ps2, List<Tile.Property> ps3, List<Tile.Property> ps4) {
		if (ps1 == ps2 || ps2 == ps3 || ps3 == ps4) {
			return false;
		}
	
		foreach (Tile.Property p1 in ps1) {
			foreach (Tile.Property p2 in ps2) {
				foreach (Tile.Property p3 in ps3) {
					foreach (Tile.Property p4 in ps4) {
						if (p1 == p2 && p2 == p3 & p3 == p4) {
							print("All tiles are " + p1.ToString ());
							return true;
						}
					}
				}
			}
		}
		return false;
	}
}
