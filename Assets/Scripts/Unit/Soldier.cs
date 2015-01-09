using UnityEngine;

public class Soldier : Unit {
	public Soldier(Vector2i position, PlayerManager playerManager) : base(3, 2, UnitType.SOLDIER, 1, position, new TileType[]{TileType.FLOOR}, playerManager) {
		//TODO add actions
	}
}

