using UnityEngine;
using System.Collections.Generic;

public class Wall : Tile {

	public Wall(int x, int y, GameBoard gb) : base(TileType.WALL, 0, gb, x, y) {
	}

	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		if(TileEvent.REPLACED.Equals(eventId)) {
			List<Tile> neighbours = gameboard.getNeighbours(x,y);
			bool render = false;

			foreach(Tile neighbour in neighbours) {
				if(neighbour.Type != TileType.WALL) {
					render = true;
					break;
				}
			}

			if(gObject != null) {
				MeshRenderer[] meshRenderers = gObject.GetComponentsInChildren<MeshRenderer>();

				foreach(MeshRenderer renderer in meshRenderers) {
					renderer.enabled = render;
				}
			}
		}
	}
}
