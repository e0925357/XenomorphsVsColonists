
public class Colonist : Unit {
	public Colonist(Vector2i position, PlayerManager playerManager, UnitManager unitManager) : base(5, 1, 7, UnitType.COLONIST, 1, position, new TileType[]{TileType.FLOOR}, playerManager, unitManager) {
		//TODO add actions
		
		defaultFloorAction = UnitActionType.WALK.createAction(this);
		
		actions = new UnitAction[]{defaultFloorAction};
	}
}

