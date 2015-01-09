using UnityEngine;
using System.Collections;

public class Mine : Tile {
	
	public Mine(int x, int y, GameBoard gb) : base(TileType.MINE, 0, gb, x, y, 6, 6) {
	}
	
	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		//do nothing
	}
}