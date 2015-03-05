using UnityEngine;
using System.Collections.Generic;

public class SlashAction : UnitAction {
	private GameBoard gameboard;
	private UnitManager unitManager;
	private List<Unit> targetList = new List<Unit>(9);
	private GameObject slashPrefab;
	private float damage = 1.5f;

	private static readonly Vector2i[] possibleTargetOffsets = {
		new Vector2i(-1,  1), new Vector2i( 0,  1), new Vector2i( 1,  1),
		new Vector2i(-1,  0), new Vector2i( 0,  0), new Vector2i( 1,  0),
		new Vector2i(-1, -1), new Vector2i( 0, -1), new Vector2i( 1, -1)};

	public SlashAction(Unit unit, HighlighterManager highlighterManager, GameObject slashPrefab) : base(true, false, 3, unit, UnitActionType.SLASH, highlighterManager) {
		gameboard = GameObject.Find("GameBoard").GetComponent<GameBoard>();
		unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();

		this.slashPrefab = slashPrefab;
	}

	public override void doAction (Vector2i position) {
		Debug.LogWarning("Can't do SlashAction @ position");
	}

	public override void doAction (Unit target) {
		if(unit.Ap < apCost) {
			Debug.LogWarning("Not enought AP to slash '" + target + "'!");
			return;
		}
		
		if(!targetList.Contains(target)) {
			Debug.LogWarning("The unit '" + target + "' is not slashable from here!");
			return;
		}
		
		unit.Ap -= apCost;
		target.Health -= damage;
		
		GameObject go = (GameObject)GameObject.Instantiate(slashPrefab);
		go.transform.position = new Vector3(target.Position.x*2 + 1, 1.5f, target.Position.y*2 + 1);
	}

	public override int apCostForTile (Vector2i position) {
		return apCost;
	}

	public override void actionSelected () {
		highlighterManager.clearSelection();
		highlighterManager.setState(unit.Position.x, unit.Position.y, HighlighterState.SELECTED);
		targetList.Clear();

		foreach(Vector2i offset in possibleTargetOffsets) {
			Vector2i neighbour = unit.Position + offset;

			if(!gameboard.isInside(neighbour.x, neighbour.y)) {
				continue;
			}

			Unit neighbpurUnit = unitManager.getUnit(neighbour);

			if(neighbpurUnit != null) {
				if(neighbpurUnit.Team != unit.Team) {
					highlighterManager.setState(neighbour.x, neighbour.y, HighlighterState.ENEMY);
					targetList.Add(neighbpurUnit);
				}

			} else if(gameboard.tileTypes[neighbour.x, neighbour.y] != TileType.WALL) {
				highlighterManager.setState(neighbour.x, neighbour.y, HighlighterState.ATTACKABLE);
			}
		}
	}
}

