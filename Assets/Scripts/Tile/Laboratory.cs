using UnityEngine;
using System.Collections;

public class Laboratory : Tile {
	
	public Laboratory(int x, int y, GameBoard gb) : base(TileType.LAB, 0, gb, x, y, 6, 6) {
	}
	
	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		//do nothing
	}
}