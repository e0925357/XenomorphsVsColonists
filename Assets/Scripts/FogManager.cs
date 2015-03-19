using UnityEngine;
using System.Collections.Generic;
using Scripts.path;

public class FogManager : MonoBehaviour {

	public UnitManager unitManager;
	public PlayerManager playerManager;

	public delegate void unitSeen(Unit seenUnit, Unit watcher);
	public event unitSeen unitSeenEvent;

	public Material visibleMat;
	public Material hiddenMat;

	private List<Unit>[,,] visibleField;

	private GameBoard gameboard;
	private LineDrawer lineDrawer;
	private bool visible = true;
	private Vector2i ignorePos;
	private Vector2i ignorePos2;
	private HashSet<TileType> transperentTiles;
	private Dictionary<TileType, float> costMap;
	private BreadthFirstSearch bfs;

	// Use this for initialization
	void Start () {
		gameboard = GameObject.Find("GameBoard").GetComponent<GameBoard>();
		bfs = new BreadthFirstSearch();

		visibleField = new List<Unit>[gameboard.sizeX, gameboard.sizeY, playerManager.playerCount];

		transperentTiles = new HashSet<TileType>();
		transperentTiles.Add(TileType.FLOOR);
		transperentTiles.Add(TileType.CABLE);
		transperentTiles.Add(TileType.VENT);

		costMap = new Dictionary<TileType, float>();
		costMap[TileType.FLOOR] = 1;
		costMap[TileType.CABLE] = 1.5f;
		costMap[TileType.VENT] = 1.5f;

		lineDrawer = new RoundToNearestLineDrawer(checkTileVisible);

		//initialize visible field
		for(int x = 0; x < gameboard.sizeX; x++) {
			for(int y = 0; y < gameboard.sizeY; y++) {
				for(int player = 0; player < playerManager.playerCount; player++) {
					visibleField[x,y, player] = new List<Unit>();
				}
			}
		}

		updateFog();
	}

	void OnEnable() {
		PlayerManager.endTurnEvent += onEndTurn;
		UnitManager.unitMovedEvent += onUnitMovement;
	}
	
	void OnDisable() {
		PlayerManager.endTurnEvent -= onEndTurn;
		UnitManager.unitMovedEvent -= onUnitMovement;
	}

	public void onUnitMovement(Unit unit) {
		updateFog();

		if (unitSeenEvent != null) {
			for(int player = 0; player < playerManager.playerCount; player++) {
				if(player + 1 == unit.Team) continue;

				foreach(Unit watcher in visibleField[unit.Position.x, unit.Position.y, player]) {
					unitSeenEvent(unit, watcher);
				}
			}
		}
	}

	public void onEndTurn(int lastPlayer, int nextPlayer) {
		updateFog(nextPlayer);
	}

	public void updateFog() {
		updateFog(playerManager.currentPlayer);
	}
	
	private void updateFog(int player) {
		//make everything invisible
		for(int x = 0; x < gameboard.sizeX; x++) {
			for(int y = 0; y < gameboard.sizeY; y++) {
				visibleField[x,y, player-1].Clear();
				TileData td = gameboard.tiles[x,y].TileData;

				if(td != null) {
					td.setMaterial(hiddenMat);
				}
			}
		}

		foreach(Unit u in unitManager.ActiveUnits) {
			if(u.Team != player) {
				continue;
			}

			UnitData uData = u.UnitData;

			if(uData != null) {
				uData.setVisible(true);
			}

			ignorePos = u.Position;

			SearchResult putativeVisibles = bfs.searchReagion(u.Position, u.MaxSight, true, transperentTiles, costMap);

			foreach(Vector2i putativeVisiblePos in putativeVisibles.PathMap.Keys) {
				visible = true;
				ignorePos2 = putativeVisiblePos;

				if(!putativeVisibles.Equals(u.Position)) {
					lineDrawer.drawLine(u.Position, putativeVisiblePos);
				}

				if(visible) {
					TileData td = gameboard.tiles[putativeVisiblePos.x,putativeVisiblePos.y].TileData;
					visibleField[putativeVisiblePos.x,putativeVisiblePos.y, player-1].Add(u);

					if(td != null) {
						td.setMaterial(visibleMat);
					}
				}
			}
		}

		foreach(Unit u in unitManager.ActiveUnits) {
			if(u.Team == player) {
				continue;
			}

			UnitData uData = u.UnitData;
			
			if(uData != null) {
				uData.setVisible(visibleField[u.Position.x, u.Position.y, player-1].Count > 0);
			}
		}
	}

	public void checkTileVisible(Vector2i pos) {
		if(!ignorePos.Equals(pos) && !ignorePos2.Equals(pos) &&
		   (!gameboard.isInside(pos.x, pos.y) || !transperentTiles.Contains(gameboard.tileTypes[pos.x, pos.y]) || unitManager.getUnit(pos) != null)) {

			visible = false;
		}
	}
}
