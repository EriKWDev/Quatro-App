using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	[SerializeField]
	public Dictionary<string, GameObject> board = new Dictionary<string, GameObject>();
	GameManager gameManager;
	public GameObject selectedObject;

	private void Start() {
		BoardTile[] children = transform.GetComponentsInChildren<BoardTile>();
		int k = 0;
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				board[i + "" + j] = children[k].gameObject;
				children[k].gameObject.name = i + "" + j;
				children[k].gameObject.GetComponent<BoardTile> ().id = i + "" + j;
				k++;
			}
		}
		gameManager = GameObject.FindObjectOfType<GameManager>();
		print(gameManager.prefabs[2].name);
		// board["12"].GetComponent<Renderer>().material.color = Color.blue;

		int zi = 0;
		foreach (GameObject t in gameManager.prefabs) {
			t.transform.position = new Vector3 (-5f, 1.2f, (zi*2.3f)-8f);
			GameObject.Instantiate(t);
			zi++;
		}
	}

	private void DeselectAllTiles () {
		foreach (GameObject t in GameObject.FindGameObjectsWithTag("Tile")) {
			t.GetComponent<Tile>().selected = false;
		}
	}

	private void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown (1)) {
			
			if (hit.collider.gameObject.layer == 9 && hit.collider.gameObject.GetComponent<BoardTile> ().occupied == false) {
				GameObject boardTile = hit.collider.gameObject;
				boardTile.GetComponent<Renderer>().material.color = Color.red;
				selectedObject.transform.position = hit.transform.position + Vector3.up * 1.2f;
				DeselectAllTiles ();
				selectedObject.GetComponent<Tile> ().selectable = false;
				selectedObject = null;
				hit.collider.gameObject.GetComponent<BoardTile> ().occupied = true;
			}
			
			if (hit.collider.gameObject.layer == 10 && hit.collider.gameObject.GetComponent<Tile> ().selectable == true) {
				print(hit.collider.name);
				GameObject tile = hit.collider.gameObject;
				DeselectAllTiles();

				tile.GetComponent<Tile>().selected = true;
				selectedObject = tile.gameObject;
			}
		}
	}
}
