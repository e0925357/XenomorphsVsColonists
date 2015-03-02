using UnityEngine;
using System.Collections;

public class Reactor : Room {
	
	public Reactor(int x, int y, GameBoard gb) : base(TileType.REACTOR, gb, x, y, 6, 6) {
	}
	
	protected override void receiveEventHook (TileEvent eventId, int originX, int originY) {
		//Do nothing
	}
}