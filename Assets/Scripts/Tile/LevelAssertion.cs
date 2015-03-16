using UnityEngine;
using System.Collections.Generic;

public class LevelAssertion : MonoBehaviour {
	
	private static readonly HashSet<TileType> airDucts = new HashSet<TileType>();
	private static readonly HashSet<TileType> powerDucts = new HashSet<TileType>();
	private static readonly Vector2i[] neighbourOffsets = {new Vector2i(1, 0), new Vector2i(0, 1), new Vector2i(-1, 0), new Vector2i(0, -1)};

	static LevelAssertion() {
		airDucts.Add(TileType.VENT);

		powerDucts.Add(TileType.CABLE);
	}


	public GameBoard gameboard;
	
	private List<LevelAssertionError> assertionErrorList = new List<LevelAssertionError>();

	public void assertLevel() {
		assertionErrorList.Clear();
		gameboard.updateRoomList();
		
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
				if(!checkIfConnectedTo(roomTile, TileType.AIR, airDucts, true)) {
					assertionErrorList.Add(new LevelAssertionError("You must connect this room to an air-purificator!", LevelAssErrType.ROOM_CONNECTION, roomTile));
				}
			}
			
			if(roomTile.Type == TileType.AIR || roomTile.Type == TileType.MINE) {
				//connected to reactor?
				if(!checkIfConnectedTo(roomTile, TileType.REACTOR, powerDucts, true)) {
					assertionErrorList.Add(new LevelAssertionError("You must connect this room to a reactor!", LevelAssErrType.ROOM_CONNECTION, roomTile));
				}
			}
		}
		
		if(mineCont <= 0) {
			assertionErrorList.Add(new LevelAssertionError("You must build at least one mine!", LevelAssErrType.ROOM_MISSING));
		
		}
		
		 if(armoryCount <= 0) {
			assertionErrorList.Add(new LevelAssertionError("You must build at least one armory!", LevelAssErrType.ROOM_MISSING));
		}
	}
	
	private bool checkIfConnectedTo(Tile source, TileType target, HashSet<TileType> walkableTiles, bool skipOverOneFloor) {

		for(int offX = -1; offX <= source.Width; offX++) {
			for(int offY = -1; offY <= source.Height; offY++) {
				int x = source.X + offX;
				int y = source.Y + offY;

				if(!gameboard.isInside(x,y) || offX != -1 && offX != source.Width &&  offY != -1 && offY != source.Height || offX == offY) continue;

				TileType neighbourType = gameboard.tiles[x,y].Type;

				if(!walkableTiles.Contains(neighbourType) && (neighbourType != TileType.FLOOR || !skipOverOneFloor)) continue;

				if(checkIfConnectedTo(new Vector2i(x,y), target, walkableTiles, skipOverOneFloor)) return true;

			}
		}

		return false;
	}

   private bool checkIfConnectedTo(Vector2i startPos, TileType target, HashSet<TileType> walkableTiles, bool skipOverOneFloor) {
		Queue<Vector2i> openList = new Queue<Vector2i>();
		HashSet<Vector2i> visitedSet = new HashSet<Vector2i>();

		openList.Enqueue(startPos);

		while(openList.Count > 0) {
			Vector2i currentPos = openList.Dequeue();
			visitedSet.Add(currentPos);

			if(!gameboard.isInside(currentPos.x, currentPos.y)) continue;

			Tile currentTile = gameboard.tiles[currentPos.x, currentPos.y];

			if(!walkableTiles.Contains(currentTile.Type) && (currentTile.Type != TileType.FLOOR || !skipOverOneFloor)) continue;

			//Add neighbours to openList
			for(int i = 0; i < neighbourOffsets.Length; i++) {
				Vector2i nextPos = currentPos + neighbourOffsets[i];

				if(!gameboard.isInside(nextPos.x, nextPos.y)) continue;

				Tile nextTile = gameboard.tiles[nextPos.x, nextPos.y];

				//Have we reached our goal?
				if(nextTile.Type == target) return true;

				//nextTile.Type == TileType.FLOOR && currentTile.Type != TileType.FLOOR && skipOverOneFloor
				if(!walkableTiles.Contains(nextTile.Type) && (nextTile.Type != TileType.FLOOR || currentTile.Type == TileType.FLOOR || !skipOverOneFloor) ||
				   openList.Contains(nextPos) || visitedSet.Contains(nextPos)) continue;

				openList.Enqueue(nextPos);
			}
		}

		return false;
	}
	
	public bool IsLevelValid {
		get {
			return assertionErrorList.Count <= 0;
		}
	}

	public List<LevelAssertionError> AssertionErrorList {
		get {
			return assertionErrorList;
		}
	}
}

