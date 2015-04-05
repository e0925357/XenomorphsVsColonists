using UnityEngine;
using System.Collections.Generic;

public class LayEggAction : UnitAction
{
	private static HashSet<TileType> breedingTiles;

	static LayEggAction() {
		breedingTiles = new HashSet<TileType> ();
		breedingTiles.Add(TileType.FLOOR);
	}

	private UnitManager unitManager;
	public BreadthFirstSearch bsf = new BreadthFirstSearch();
	private SearchResult searchResult = null;

	public LayEggAction (Unit unit, HighlighterManager highlighterManager)
		: base(false, true, 8, unit, UnitActionType.LAY_EGG, highlighterManager)
	{
		unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();;
	}

	public override void doAction (Vector2i position)
	{
		if(searchResult == null)
			return;
		
		int cost = apCostForTile(position);
		
		if(unit.Ap < cost) {
			Debug.LogWarning("Unit " + unit + " can't lay egg at " + position + ": Not enought ap!");
			return;
		}

		List<Vector2i> path;
		
		if (searchResult.PathMap.TryGetValue (position, out path)) {
			Unit newUnit = UnitType.XENO_EGG.createUnit (position.x, position.y);

			if (!unitManager.registerUnit(newUnit)) {
				Debug.LogError("Failed to register Egg at unitManager!");
			}

			unit.Ap -= cost;
		}
		else {
			Debug.LogWarning("Unit " + unit + " can't lay egg at " + position + ": No such path exists!");
		}
	}

	public override void doAction (Unit unit)
	{
		Debug.LogWarning("Can't do LayEggAction @ Unit");
	}

	public override int apCostForTile (Vector2i position)
	{
		return apCost;
	}

	public override void actionSelected ()
	{
		searchResult = bsf.searchReagion(unit.Position, 1, false, breedingTiles);
		
		highlighterManager.clearSelection();
		highlighterManager.setState(unit.Position.x, unit.Position.y, HighlighterState.SELECTED);
		
		foreach(Vector2i walkablePos in searchResult.PathMap.Keys) {
			if(unit.Position.Equals(walkablePos)) continue;
			
			highlighterManager.setState(walkablePos.x, walkablePos.y, HighlighterState.WALKABLE);
		}
	}
}

