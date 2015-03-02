using UnityEngine;
using System.Collections;

public class Mine : Room {
	
	public Mine(int x, int y, GameBoard gb) : base(TileType.MINE, gb, x, y, 6, 6) {
	}
	
	protected override void receiveEventHook (TileEvent eventId, int originX, int originY) {
		//Do nothing
	}
}