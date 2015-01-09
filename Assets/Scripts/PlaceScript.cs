using UnityEngine;
using System.Collections;

public class PlaceScript : MonoBehaviour {
	public GameBoard gameBoard;
	public MouseManager mouseManager;

	private GameObject currentBlueprint = null;
	private int nextTileId = -1;
	private Tile nextTile = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(nextTileId == -1) return;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(!mouseManager.overGUI && Physics.Raycast(ray, out hit)) {
			Tile tile = hit.collider.GetComponent<TileData>().tile;

			currentBlueprint.transform.position = new Vector3(tile.X*2, 0, tile.Y*2);

			MeshRenderer[] meshRenderers = currentBlueprint.GetComponentsInChildren<MeshRenderer>();
			
			foreach(MeshRenderer renderer in meshRenderers) {
				renderer.enabled = true;
			}

			if(Input.GetMouseButton(0) && nextTile.isSpawnPositionValid(tile.X, tile.Y) && tile.Type != nextTile.Type) {
				TileType type = TileType.getById(nextTileId);

				if(type == null) {
					//Oh noes!
					Debug.LogError("nextTileId is invalid! value=" + nextTileId);
					return;
				}

				Tile newTile = type.createTile(tile.X, tile.Y);

				if(newTile == null) {
					//Oh noes!
					Debug.LogError("nextTileId is invalid! value=" + nextTileId);
					return;
				}

				gameBoard.swapTiles(newTile);
			}

		} else {
			MeshRenderer[] meshRenderers = currentBlueprint.GetComponentsInChildren<MeshRenderer>();
			
			foreach(MeshRenderer renderer in meshRenderers) {
				renderer.enabled = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)) {
			//Destroy blueprint
			nextTileId = -1;
			GameObject.Destroy(currentBlueprint);
			currentBlueprint = null;
			nextTile = null;
		}
	}

	public int NextTileId {
		get {
			return this.nextTileId;
		}
		set {
			if(currentBlueprint != null) {
				GameObject.Destroy(currentBlueprint);
				currentBlueprint = null;
			}

			nextTileId = value;

			TileType type = TileType.getById(value);

			if(type != null && type.PreviewPrefab != null) {

				currentBlueprint = (GameObject)GameObject.Instantiate(type.PreviewPrefab);
				nextTile = type.createTile(0,0);

				MeshRenderer renderer = currentBlueprint.transform.GetChild(0).GetComponent<MeshRenderer>();
				renderer.enabled = false;
			} else {
				nextTileId = -1;
			}
		}
	}
}
