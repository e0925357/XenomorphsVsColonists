using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour {

	public delegate void unitMoved(Unit unit);
	public static event unitMoved unitMovedEvent;

	private GameBoard gameBoard;
	private Unit selectedUnit = null;
	private Unit[,] unitField;

	private List<Unit> activeUnits = new List<Unit>();

	public HighlighterManager highlighterManager;
	public PlayerManager playerManager;
	public ActionManager actionManager;
	
	public Text unitNameText;
	public Text apText;
	public Text healthText;

	public Sprite moveIcon;
	public Sprite shootIcon;
	public Sprite slashIcon;
	public Sprite eggIcon;
	public GameObject shootPrefab;
	public GameObject slashPrefab;

	// Use this for initialization
	void Start () {
		UnitActionType.init(highlighterManager, shootPrefab, slashPrefab);
		UnitActionType.WALK.Icon = moveIcon;
		UnitActionType.SHOOT.Icon = shootIcon;
		UnitActionType.SLASH.Icon = slashIcon;
		UnitActionType.LAY_EGG.Icon = eggIcon;

		GameObject go = GameObject.Find("GameBoard");
		gameBoard = go.GetComponent<GameBoard>();

		unitField = new Unit[gameBoard.sizeX, gameBoard.sizeY];

		bool alienCreated = false;

		foreach(Vector2i roomPos in gameBoard.rooms) {
			Tile roomTile = gameBoard.tiles[roomPos.x, roomPos.y];
			Unit createdUnit = null;

			if(roomTile.Type == TileType.ARMORY) {
				//Create soldier
				createdUnit = UnitType.SOLDIER.createUnit(roomPos.x, roomPos.y);

			} else if(roomTile.Type ==TileType.QUATERS) {
				//Create Xenomorph
				createdUnit = UnitType.COLONIST.createUnit(roomPos.x, roomPos.y);
				alienCreated = true;
			} else if(!alienCreated && roomTile.Type ==TileType.MINE) {
				//Create Xenomorph
				createdUnit = UnitType.XENO.createUnit(roomPos.x, roomPos.y);
				alienCreated = true;
			}

			if(createdUnit != null) {
				registerUnit(createdUnit);
			}
		}
	}

	void OnEnable() {
		PlayerManager.endTurnEvent += onEndTurn;
	}

	void OnDisable() {
		PlayerManager.endTurnEvent -= onEndTurn;
	}

	public Unit getUnit(Vector2i pos) {
		if(!gameBoard.isInside(pos.x, pos.y)) {
			return null;
		}

		return unitField[pos.x, pos.y];
	}

	public bool registerUnit(Unit newUnit) {
		if(unitField[newUnit.Position.x, newUnit.Position.y] != null) {
			return false;
		}

		unitField[newUnit.Position.x, newUnit.Position.y] = newUnit;
		newUnit.createGameObject();
		newUnit.onDeathEvent += onUnitDeath;
		activeUnits.Add(newUnit);

		return true;
	}

	public void onUnitDeath(Unit unit) {
		activeUnits.Remove(unit);

		if(SelectedUnit == unit) {
			SelectedUnit = null;
		}

		unitField[unit.Position.x, unit.Position.y] = null;
		unit.destroyGameObject();

		foreach(Unit activeUnit in activeUnits) {
			if(activeUnit.Team == unit.Team) {
				return;
			}
		}

		//player destroyed
		playerManager.playerLost(unit.Team);
	}

	public bool moveUnit(Vector2i fromPos, Vector2i toPos) {
		if(getUnit(fromPos) == null || !gameBoard.isInside(toPos.x, toPos.y) || getUnit(toPos) != null) {
			Debug.LogWarning("Unit can't go from " + fromPos + " to " + toPos + "!");
			return false;
		}

		unitField[toPos.x, toPos.y] = unitField[fromPos.x, fromPos.y];
		unitField[fromPos.x, fromPos.y] = null;

		unitField[toPos.x, toPos.y].Position = toPos;

		if(unitMovedEvent != null) {
			unitMovedEvent(unitField[toPos.x, toPos.y]);
		}

		return true;
	}

	public bool isPathClear(Vector2i[] path, int startIndex = 0) {
		for(int i = startIndex; i < path.Length; i++) {
			Vector2i pos = path[i];

			if(!gameBoard.isInside(pos.x, pos.y)) {
				return false;
			}

			if(unitField[pos.x, pos.y] != null) {
				return false;
			}

		}
		
		return true;
	}

	public void onEndTurn(int lastPlayer, int nextPlayer) {
		SelectedUnit = null;
	}

	public void updateStatsGUI(Unit unit) {
		apText.text = string.Format("{0}/{1}", unit.Ap, unit.MaxAP);
		healthText.text = string.Format("{0}/{1}", unit.Health, unit.MaxHealth);
	}

	public Unit SelectedUnit {
		get {
			return this.selectedUnit;
		}
		set {
			if(value != null && value.Team != playerManager.currentPlayer) {
				return;
			}

			highlighterManager.clearSelection();
			
			if(value != null) {
				unitNameText.text = value.Type.Name;
				updateStatsGUI(value);
				highlighterManager.setState(value.Position.x, value.Position.y, HighlighterState.SELECTED);
			} else {
				unitNameText.text = "No Unit Selected";
				apText.text = "-";
				healthText.text = "-";
			}

			if(selectedUnit != null) {
				selectedUnit.statsEvent -= updateStatsGUI;
			}
			
			selectedUnit = value;
			actionManager.unitSelected(value);

			if(selectedUnit != null) {
				selectedUnit.statsEvent += updateStatsGUI;
			}
		}
	}

	public List<Unit> ActiveUnits {
		get {
			return this.activeUnits;
		}
	}
}
