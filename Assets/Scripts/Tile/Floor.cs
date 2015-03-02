using UnityEngine;
using System.Collections;

public class Floor : Transporters {
	
	public Floor(int x, int y, GameBoard gb) : base(TileType.FLOOR, gb, x, y) {
	}
	
	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		//do nothing
	}
}
