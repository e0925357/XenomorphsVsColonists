using UnityEngine;
using System.Collections.Generic;

public class UnitMoveAction : UnitAction {

	public BreadthFirstSearch bsf = new BreadthFirstSearch();
	private SearchResult searchResult = null;

	public UnitMoveAction(Unit unit, HighlighterManager highlighterManager) : base(false, true, 1, unit, UnitActionType.WALK, highlighterManager) {
	}

	public override void doAction (Vector2i position) {
		if(searchResult == null)
			return;

		int cost = apCostForTile(position);

		if(unit.Ap < cost) {
			Debug.LogWarning("Unit " + unit + " can't move to " + position + ": Not enought ap!");
			return;
		}
		
		List<Vector2i> path;
		
		if(searchResult.PathMap.TryGetValue(position, out path)) {
			if(!unit.moveToTile(position, path.ToArray())) {
				Debug.LogWarning("Unit " + unit + " can't move to " + position + ": Action denied!");
			} else {
				unit.Ap -= cost;
				actionSelected();
			}
		} else {
			Debug.LogWarning("Unit " + unit + " can't move to " + position + ": No such path exists!");
		}

	}

	public override void doAction (Unit unit) {
		Debug.LogWarning("Can't do MoveAction @ Unit");
	}

	public override int apCostForTile (Vector2i position) {
		if(searchResult == null)
			return 0;

		List<Vector2i> path;

		if(searchResult.PathMap.TryGetValue(position, out path)) {
			return path.Count - 1;
		}

		return 0;
	}

	public override void actionSelected() {
		searchResult = bsf.searchReagion(unit.Position, unit.Ap/apCost, false, unit.WalkableTiles);

		highlighterManager.clearSelection();
		highlighterManager.setState(unit.Position.x, unit.Position.y, HighlighterState.SELECTED);

		foreach(Vector2i walkablePos in searchResult.PathMap.Keys) {
			if(unit.Position.Equals(walkablePos)) continue;

			highlighterManager.setState(walkablePos.x, walkablePos.y, HighlighterState.WALKABLE);
		}
	}
}

