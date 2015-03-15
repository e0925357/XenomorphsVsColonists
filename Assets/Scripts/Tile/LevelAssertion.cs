using UnityEngine;
using System.Collections.Generic;

public class LevelAssertion : MonoBehaviour {
	
	private static readonly TileType[] airDucts = {TileType.VENT};
	private static readonly TileType[] powerDucts = {TileType.CABLE};


	public GameBoard gameboard;
	
	private List<LevelAssertionError> assertionErrorList = new List<LevelAssertionError>();

	public void assertLevel() {
		assertionErrorList.Clear();
		
		int mineCont = 0;
		int armoryCount = 0;
		
		foreach(Vector2i roomPos in gameboard.rooms) {
			Tile roomTile = gameboard.tiles[roomPos.x, roomPos.y];
			
			if(roomTile.Type == TileType.MINE) {
				mineCont++;
			} else if(roomTile.Type == TileType.ARMORY) {
				armoryCount++;
			}
			
			/*
			 *  Check connections to imortant structures
			 */
			 
			if(roomTile.Type != TileType.AIR) {
				//connected to air-purifictation?
				if(!checkIfConnectedTo(TileType.AIR, airDucts, true)) {
					assertionErrorList.Add(new LevelAssertionError("You must connect this room to an air-purificator!", LevelAssErrType.ROOM_CONNECTION));
				}
			}
			
			if(roomTile.Type == TileType.AIR || roomTile.Type == TileType.MINE) {
				//connected to reactor?
				if(!checkIfConnectedTo(TileType.REACTOR, powerDucts, true)) {
					assertionErrorList.Add(new LevelAssertionError("You must connect this room to a reactor!", LevelAssErrType.ROOM_CONNECTION));
				}
			}
		}
		
		if(mineCont <= 0) {
			assertionErrorList.Add(new LevelAssertionError("You must build at least one mine!", LevelAssErrType.ROOM_MISSING));
		
		} else if(armoryCount <= 0) {
			assertionErrorList.Add(new LevelAssertionError("You must build at least one armory!", LevelAssErrType.ROOM_MISSING));
		}
	}
	
	private bool checkIfConnectedTo(TileType target, TileType[] walkableTiles, bool skipOverOneFloor) {
		return false;
	}
	
	public bool IsLevelValid {
		get {
			return assertionErrorList.Count <= 0;
		}
	}
}

