using UnityEngine;
using System.Collections.Generic;

public class ShootAction : UnitAction {
	private BreadthFirstSearch bfs = new BreadthFirstSearch();
	private SearchResult lastResult = null;
	private int range;
	private float damage;
	private HashSet<TileType> shootableTiles = new HashSet<TileType>();
	private GameBoard gameboard;
	private GameObject rifleShotsPrefab;

	public ShootAction(Unit unit, HighlighterManager highlighterManager, GameObject rifleShotsPrefab) : base(true, false, 3, unit, UnitActionType.SHOOT, highlighterManager) {
		this.range = 6;
		this.rifleShotsPrefab = rifleShotsPrefab;
		damage = 4;

		gameboard = GameObject.Find("GameBoard").GetComponent<GameBoard>();

		shootableTiles.Add(TileType.AIR);
		shootableTiles.Add(TileType.CABLE);
		shootableTiles.Add(TileType.FLOOR);
		shootableTiles.Add(TileType.VENT);
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

	private bool isTileShootable(Vector2i tilePos) {
		
		Vector2i deltaVector = tilePos - unit.Position;
		Vector2i deltaSign = new Vector2i((int)Mathf.Sign(deltaVector.x), (int)Mathf.Sign(deltaVector.y));
		float m = Mathf.Abs((float)deltaVector.y/(float)deltaVector.x);
		
		Vector2 currentPos = new Vector2(unit.Position.x, unit.Position.y);
		bool horizontal = m < 1;
		
		int maxDelta = Mathf.Abs(horizontal ? deltaVector.x : deltaVector.y);
		
		for(int k = 0; k < maxDelta; k++) {
			if(horizontal) {
				currentPos.x += deltaSign.x;
				currentPos.y += m*deltaSign.y;
			} else {
				currentPos.x += deltaSign.x/m;
				currentPos.y += deltaSign.y;
			}
			
			int x = (int)Mathf.Round(currentPos.x);
			int y = (int)Mathf.Round(currentPos.y);
			
			if(!shootableTiles.Contains(gameboard.tileTypes[x,y])) {
				return false;
			}
		}

		return true;
	}
}

