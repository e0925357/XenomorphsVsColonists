using UnityEngine;

public class XenomorphEgg : Unit
{
	private const uint hatchingStage = 2;
	private uint breedStage = 0;

	public XenomorphEgg (Vector2i position, PlayerManager playerManager, UnitManager unitManager)
		: base(0, 1, 1, UnitType.XENO_EGG, 2, position, new TileType[]{}, playerManager, unitManager)
	{
		// I am just an egg.
		// So just don't drop me.

		actions = new UnitAction[]{};
		// Happy happy!
	}

	/// <summary>
	/// Breed this instance. Apparently this instance is an egg. If the egg was bread enough, it will hatch and make you happy with a brand new instance of a minion -> Happy happy!
	/// </summary>
	public void breed() {
		++breedStage;

		Debug.Log ("Egg was breed: " + (hatchingStage - breedStage) + " more breedings to go!");

		if (breedStage >= hatchingStage) {
			// Hatch!

			Health = 0;

			Unit newUnit = UnitType.XENO_MINION.createUnit (position.x, position.y);
			
			if (!unitManager.registerUnit(newUnit)) {
				Debug.LogError("Failed to register Xenomorph-Minion at unitManager!");
			}

			Debug.Log ("Egg hatched!");
		}
	}
}

