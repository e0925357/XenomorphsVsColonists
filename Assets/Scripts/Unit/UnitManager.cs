using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {

	private GameBoard gameBoard;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("GameBoard");
		gameBoard = go.GetComponent<GameBoard>();

		bool alienCreated = false;

		foreach(Vector2i roomPos in gameBoard.rooms) {
			Tile roomTile = gameBoard.tiles[roomPos.x, roomPos.y];

			if(roomTile.Type == TileType.LAB) {
				//Create soldier
				UnitType.SOLDIER.createUnit(roomPos.x, roomPos.y).createGameObject();
			} else if(!alienCreated && roomTile.Type ==TileType.MINE) {
				//Create Xenomorph
				UnitType.XENO.createUnit(roomPos.x, roomPos.y).createGameObject();
				alienCreated = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
