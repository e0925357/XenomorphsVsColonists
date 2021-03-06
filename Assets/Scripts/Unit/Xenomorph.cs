using UnityEngine;

public class Xenomorph : Unit {
	public Xenomorph(Vector2i position, PlayerManager playerManager, UnitManager unitManager) : base(14, 10, 10, UnitType.XENO, 2, position, new TileType[]{TileType.FLOOR, TileType.CABLE, TileType.VENT}, playerManager, unitManager) {
		//TODO add actions

		defaultFloorAction = UnitActionType.WALK.createAction(this);
		defaultEnemyAction = UnitActionType.SLASH.createAction(this);
		
		actions = new UnitAction[]{defaultFloorAction, defaultEnemyAction, UnitActionType.LAY_EGG.createAction(this), UnitActionType.BREED_EGG.createAction (this)};
	}
}
