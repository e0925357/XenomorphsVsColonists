using UnityEngine;
using System.Collections.Generic;
using Scripts.path;

public class ShootAction : UnitAction {
	private BreadthFirstSearch bfs = new BreadthFirstSearch();
	private SearchResult lastResult = null;
	private int range;
	private float damage;
	private HashSet<TileType> shootableTiles = new HashSet<TileType>();
	private GameBoard gameboard;
	private GameObject rifleShotsPrefab;
	private LineDrawer lineDrawer;

	private bool tileShootable = true;

	public ShootAction(Unit unit, HighlighterManager highlighterManager, GameObject rifleShotsPrefab) : base(true, false, 3, unit, UnitActionType.SHOOT, highlighterManager) {
		this.range = 6;
		this.rifleShotsPrefab = rifleShotsPrefab;
		damage = 4;

		gameboard = GameObject.Find("GameBoard").GetComponent<GameBoard>();

		shootableTiles.Add(TileType.AIR);
		shootableTiles.Add(TileType.CABLE);
		shootableTiles.Add(TileType.FLOOR);
		shootableTiles.Add(TileType.VENT);

		lineDrawer = new ThickLineDrawer(checkTile);
	}

	public override void doAction (Vector2i position) {
		Debug.LogWarning("Can't do ShootAction @ position");
	}

	public override void doAction (Unit target) {
		if(unit.Ap < apCost) {
			Debug.LogWarning("Not enought AP to shoot '" + target + "'!");
			return;
		}

		if(!lastResult.Units.Contains(target)) {
			Debug.LogWarning("The unit '" + target + "' is not shootable from here!");
			return;
		}

		unit.Ap -= apCost;
		target.Health -= damage;

		GameObject go = (GameObject)GameObject.Instantiate(rifleShotsPrefab);
		go.transform.position = new Vector3(unit.Position.x*2 + 1, 0.8f, unit.Position.y*2 + 1);

		ProjectileSpawner ps = go.GetComponent<ProjectileSpawner>();
		ps.target = new Vector3(target.Position.x*2 + 1, 0.8f, target.Position.y*2 + 1);
	}

	public override int apCostForTile (Vector2i position) {
		return apCost;
	}

	public override void actionSelected () {
		highlighterManager.clearSelection();
		highlighterManager.setState(unit.Position.x, unit.Position.y, HighlighterState.SELECTED);

		lastResult = bfs.searchReagion(unit.Position, range, true, shootableTiles);

		foreach(Vector2i pos in lastResult.PathMap.Keys) {
			if(isTileShootable(pos)) {
				//Mark tile
				highlighterManager.setState(pos.x, pos.y, HighlighterState.ATTACKABLE);
			}
		}

		List<Unit> possibleTargetList = lastResult.Units;

		for(int i = possibleTargetList.Count - 1; i >= 0; i--) {
			Unit target = possibleTargetList[i];

			if(target == unit || target.Team == unit.Team) {
				possibleTargetList.RemoveAt(i);
				continue;
			}

			if(isTileShootable(target.Position)) {
				highlighterManager.setState(target.Position.x, target.Position.y, HighlighterState.ENEMY);
			} else {
				possibleTargetList.RemoveAt(i);
			}
		}
	}

	public void checkTile(Vector2i tilePos) {
		if(!shootableTiles.Contains(gameboard.tileTypes[tilePos.x,tilePos.y])) {
			tileShootable = false;
		}
	}

	private bool isTileShootable(Vector2i tilePos) {

		tileShootable = true;
		lineDrawer.drawLine(unit.Position, tilePos);

		return tileShootable;
	}
}

