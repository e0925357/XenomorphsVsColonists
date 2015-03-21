using UnityEngine;

public class XenomorphEgg : Unit
{
	public XenomorphEgg (Vector2i position, PlayerManager playerManager, UnitManager unitManager)
		: base(0, 1, 1, UnitType.XENO_EGG, 2, position, new TileType[]{}, playerManager, unitManager)
	{
		// I am just an egg.
		// So just don't drop me.
	}
}

