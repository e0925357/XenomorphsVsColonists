using UnityEngine;

public class XenomorphMinion : Unit {
	public XenomorphMinion(Vector2i position, PlayerManager playerManager, UnitManager unitManager) : base(7, 5, 5, UnitType.XENO_MINION, 2, position, new TileType[]{TileType.FLOOR, TileType.CABLE, TileType.VENT}, playerManager, unitManager) {
		//TODO add actions

		defaultFloorAction = UnitActionType.WALK.createAction(this);
		defaultEnemyAction = UnitActionType.WEAK_SHLASH.createAction(this);
		
		actions = new UnitAction[]{defaultFloorAction, defaultEnemyAction};
	}
}
