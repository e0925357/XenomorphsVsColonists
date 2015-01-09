using UnityEngine;
using System.Collections;

public class Floor : Tile {
	
	public Floor(int x, int y, GameBoard gb) : base(TileType.FLOOR, 0, gb, x, y) {
	}
	
	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		//do nothing
	}
}
