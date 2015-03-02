using UnityEngine;
using System.Collections.Generic;
using Scripts.path;

public class FogManager : MonoBehaviour {

	public UnitManager unitManager;
	public PlayerManager playerManager;

	public Material visibleMat;
	public Material hiddenMat;

	private bool[,] visibleField;

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

		visibleField = new bool[gameboard.sizeX, gameboard.sizeY];

		transperentTiles = new HashSet<TileType>();
		transperentTiles.Add(TileType.FLOOR);
		transperentTiles.Add(TileType.CABLE);
		transperentTiles.Add(TileType.VENT);

		costMap = new Dictionary<TileType, float>();
		costMap[TileType.FLOOR] = 1;
		costMap[TileType.CABLE] = 1.5f;
		costMap[TileType.VENT] = 1.5f;

		lineDrawer = new RoundToNearestLineDrawer(checkTileVisible);

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
				visibleField[x,y] = false;
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
					visibleField[putativeVisiblePos.x,putativeVisiblePos.y] = true;

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
				uData.setVisible(visibleField[u.Position.x, u.Position.y]);
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
