using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitManager : MonoBehaviour {

	private GameBoard gameBoard;
	private Unit selectedUnit = null;
	private Unit[,] unitField;

	public HighlighterManager highlighterManager;
	public PlayerManager playerManager;
	
	public Text unitNameText;
	public Text apText;
	public Text healthText;

	// Use this for initialization
	void Start () {
		UnitActionType.init(highlighterManager);

		GameObject go = GameObject.Find("GameBoard");
		gameBoard = go.GetComponent<GameBoard>();

		unitField = new Unit[gameBoard.sizeX, gameBoard.sizeY];

		bool alienCreated = false;

		foreach(Vector2i roomPos in gameBoard.rooms) {
			Tile roomTile = gameBoard.tiles[roomPos.x, roomPos.y];

			if(roomTile.Type == TileType.LAB) {
				//Create soldier
				unitField[roomPos.x, roomPos.y] = UnitType.SOLDIER.createUnit(roomPos.x, roomPos.y);
				unitField[roomPos.x, roomPos.y].createGameObject();

			} else if(!alienCreated && roomTile.Type ==TileType.MINE) {
				//Create Xenomorph
				unitField[roomPos.x, roomPos.y] = UnitType.XENO.createUnit(roomPos.x, roomPos.y);
				unitField[roomPos.x, roomPos.y].createGameObject();
				alienCreated = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
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

	public bool moveUnit(Vector2i fromPos, Vector2i toPos) {
		if(getUnit(fromPos) == null || !gameBoard.isInside(toPos.x, toPos.y) || getUnit(toPos) != null) {
			Debug.LogWarning("Unit can't go from " + fromPos + " to " + toPos + "!");
			return false;
		}

		unitField[toPos.x, toPos.y] = unitField[fromPos.x, fromPos.y];
		unitField[fromPos.x, fromPos.y] = null;

		return true;
	}

	public void onEndTurn() {
		SelectedUnit = null;
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
				apText.text = string.Format("{0}/{1}", value.Ap, value.MaxAP);
				healthText.text = string.Format("{0}/{1}", value.Health, value.MaxHealth);
				highlighterManager.setState(value.Position.x, value.Position.y, HighlighterState.SELECTED);

				if(value.DefaultFloorAction != null) {
					value.DefaultFloorAction.actionSelected();
				}
			} else {
				unitNameText.text = "No Unit Selected";
				apText.text = "-";
				healthText.text = "-";
			}
			
			selectedUnit = value;
		}
	}
}
