using UnityEngine;

public class Soldier : Unit {
	public Soldier(Vector2i position, PlayerManager playerManager, UnitManager unitManager) : base(8, 2, 8, UnitType.SOLDIER, 1, position, new TileType[]{TileType.FLOOR}, playerManager, unitManager) {
		//TODO add actions

		defaultFloorAction = UnitActionType.WALK.createAction(this);
		defaultEnemyAction = UnitActionType.SHOOT.createAction(this);

		actions = new UnitAction[]{defaultFloorAction, defaultEnemyAction};
	}
}

