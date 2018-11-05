using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	[SerializeField]
	public Dictionary<string, BoardTile> board = new Dictionary<string, BoardTile>();
	public Dictionary<string, Tile> tileBoard = new Dictionary<string, Tile>();
	GameManager gameManager;
	public GameObject selectedObject;
	public bool hasWon = false;
	public GameObject fireworkPrefab;

	private void Start() {
		BoardTile[] children = transform.GetComponentsInChildren<BoardTile>();
		int k = 0;
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				board[i + "" + j] = children[k].GetComponent<BoardTile> ();
				children[k].name = i + "" + j;
				children[k].GetComponent<BoardTile> ().id = i + "" + j;
				k++;
			}
		}

		gameManager = GameObject.FindObjectOfType<GameManager>();

		int zi = 0;
		float spacing = 2.5f;
		foreach (GameObject t in gameManager.prefabs) {
			t.transform.position = new Vector3 (-15, 2.2f, (zi*spacing)-((15f*spacing)/2f));
			GameObject newT = GameObject.Instantiate(t);
			newT.name = t.name;
			zi++;
		}
	}

	private void DeselectAllTiles () {
		foreach (GameObject t in GameObject.FindGameObjectsWithTag("Tile")) {
			t.GetComponent<Tile>().selected = false;
		}
	}

	private void Update() {
		if (!hasWon) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

			if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(1)) {

				if (selectedObject != null && hit.collider.gameObject.layer == 9 && hit.collider.gameObject.GetComponent<BoardTile>().occupied == false) {
					GameObject boardTile = hit.collider.gameObject;
					boardTile.GetComponent<Renderer>().material.color = Color.red;
					selectedObject.GetComponent<Tile> ().bePosition = hit.transform.position + Vector3.up * 1.2f;
					boardTile.GetComponent<BoardTile>().tile = selectedObject.GetComponent<Tile>();
					tileBoard[boardTile.name] = boardTile.GetComponent<BoardTile>().tile;

					DeselectAllTiles();
					selectedObject.GetComponent<Tile>().selectable = false;
					selectedObject = null;
					hit.collider.gameObject.GetComponent<BoardTile>().occupied = true;
					AddedNewTile();
				}

				if (hit.collider.gameObject.layer == 10 && hit.collider.gameObject.GetComponent<Tile>().selectable == true) {
					GameObject tile = hit.collider.gameObject;
					DeselectAllTiles();

					tile.GetComponent<Tile>().selected = true;
					selectedObject = tile.gameObject;
				}
			}
		}
	}

	public void AddedNewTile() {
		hasWon = gameManager.CheckWin(tileBoard);
		if (hasWon) {
			Instantiate(fireworkPrefab);
		}
	}
}
