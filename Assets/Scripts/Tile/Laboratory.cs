using UnityEngine;
using System.Collections;

public class Laboratory : Room {
	
	public Laboratory(int x, int y, GameBoard gb) : base(TileType.LAB, gb, x, y, 6, 6) {
	}
	
	protected override void receiveEventHook (TileEvent eventId, int originX, int originY) {
		//Do nothing
	}
}