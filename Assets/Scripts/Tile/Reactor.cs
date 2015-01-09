using UnityEngine;
using System.Collections;

public class Reactor : Tile {
	
	public Reactor(int x, int y, GameBoard gb) : base(TileType.REACTOR, 0, gb, x, y, 6, 6) {
	}
	
	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		//do nothing
	}
}