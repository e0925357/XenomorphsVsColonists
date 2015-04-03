using UnityEngine;
using System.Collections.Generic;

public class BreedEggAction : UnitAction
{
	private GameBoard gameboard;
	private UnitManager unitManager;
	private List<Unit> targetList = new List<Unit>(9);

	private static readonly Vector2i[] possibleTargetOffsets = {
		new Vector2i(-1,  1), new Vector2i( 0,  1), new Vector2i( 1,  1),
		new Vector2i(-1,  0), new Vector2i( 0,  0), new Vector2i( 1,  0),
		new Vector2i(-1, -1), new Vector2i( 0, -1), new Vector2i( 1, -1)};

	public BreedEggAction (Unit unit, HighlighterManager highlighterManager)
		: base(true, false, 8, unit, UnitActionType.BREED_EGG, highlighterManager)
	{
		gameboard = GameObject.Find("GameBoard").GetComponent<GameBoard>();
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager> ();
	}

	public override void doAction (Vector2i position)
	{
		Debug.LogWarning("Can't do BreedAction @ position");
	}

	public override void doAction (Unit target)
	{
		if (target.Type != UnitType.XENO_EGG) {
			Debug.LogWarning("Unit " + target + " is not breedable by current unit!");
			return;
		}

		if(unit.Ap < apCost) {
			Debug.LogWarning("Not enought AP to breed '" + target + "'!");
			return;
		}
		
		if(!targetList.Contains(target)) {
			Debug.LogWarning("The unit '" + target + "' is not breedable from here!");
			return;
		}
		
		unit.Ap -= apCost;
		
		XenomorphEgg eggUnit = (XenomorphEgg)target;
		eggUnit.breed ();
	}

	public override int apCostForTile (Vector2i position)
	{
		return apCost;
	}

	public override void actionSelected ()
	{
		highlighterManager.clearSelection();
		highlighterManager.setState(unit.Position.x, unit.Position.y, HighlighterState.SELECTED);
		targetList.Clear();
		
		foreach(Vector2i offset in possibleTargetOffsets) {
			Vector2i neighbour = unit.Position + offset;
			
			if(!gameboard.isInside(neighbour.x, neighbour.y)) {
				continue;
			}
			
			Unit neighbourUnit = unitManager.getUnit(neighbour);
			
			if(neighbourUnit != null) {
				if(neighbourUnit.Team == unit.Team) {
					highlighterManager.setState(neighbour.x, neighbour.y, HighlighterState.TEAMMATE);
					targetList.Add(neighbourUnit);
				}
				
			} else if(gameboard.tileTypes[neighbour.x, neighbour.y] != TileType.WALL) {
				highlighterManager.setState(neighbour.x, neighbour.y, HighlighterState.SUPPORTABLE);
			}
		}
	}
}