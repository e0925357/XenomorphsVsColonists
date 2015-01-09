using UnityEngine;

public class Xenomorph : Unit {
	public Xenomorph(Vector2i position, PlayerManager playerManager) : base(5, 10, UnitType.XENO, 2, position, new TileType[]{TileType.FLOOR, TileType.CABLE, TileType.VENT}, playerManager) {
		//TODO add actions
	}
}
