using UnityEngine;
using System.Collections.Generic;

public abstract class Unit {
	protected int maxAP;
	protected int ap;
	protected float maxHealth;
	protected float health;

	protected UnitType type;

	protected int team;

	protected Vector2i position;

	protected Vector2i[] pathToWalk;
	protected int nextPathIndex = -1;
	protected HashSet<TileType> walkableTiles;

	protected UnitAction[] actions;
	protected UnitAction defaultFloorAction;
	protected UnitAction defaultEnemyAction;
	protected UnitAction defaultAllyAction;

	protected GameObject gameObject = null;
	protected UnitData unitData = null;
	protected PlayerManager playerManager;
	protected UnitManager unitManager;

	public Unit(int maxAP, float maxHealth, UnitType type, int team, Vector2i position, TileType[] walkableTiles, PlayerManager playerManager, UnitManager unitManager) {
		this.maxAP = maxAP;
		ap = maxAP;
		this.maxHealth = maxHealth;
		health = maxHealth;
		this.type = type;
		this.team = team;
		this.position = position;
		this.walkableTiles = new HashSet<TileType>(walkableTiles);
		this.playerManager = playerManager;
		this.unitManager = unitManager;

		PlayerManager.endTurnEvent += onEndTurn;
	}

	~Unit() {
		PlayerManager.endTurnEvent -= onEndTurn;
	}

	public void createGameObject() {
		if(gameObject != null)
			return;

		if(type.Prefab == null) {
			Debug.LogWarning(type + "@" + position + " has no prefab: Can't create GameObject!");
			return;
		}

		gameObject = (GameObject)GameObject.Instantiate(type.Prefab);
		gameObject.transform.position = new Vector3(position.x*2, 0, position.y*2);
		unitData = gameObject.transform.GetChild(0).GetComponent<UnitData>();
		unitData.unit = this;
		unitData.playerManager = playerManager;
		unitData.unitManager = unitManager;
	}

	public void destroyGameObject() {
		if(gameObject == null)
			return;

		GameObject.Destroy(gameObject);
		gameObject = null;
	}

	public void onEndTurn() {
		nextTurn();
	}

	public bool moveToTile(Vector2i target, Vector2i[] path) {
		if(path == null || path.Length <= 0)
			throw new System.ArgumentNullException("path");

		if(unitManager.moveUnit(position, target)) {
			position = target;
			PathToWalk = path;

			return true;
		}

		return false;
	}
	

	public virtual void nextTurn() {
		ap = maxAP;
	}

	public void tileReached() {
		nextPathIndex++;
	}

	public Vector2i? NextTile {
		get {
			if(pathToWalk == null || nextPathIndex < 0 || nextPathIndex >= pathToWalk.Length) return null;
			else return pathToWalk[nextPathIndex];
		}
	}
	
	public int MaxAP {
		get {
			return this.maxAP;
		}
	}

	public int Ap {
		get {
			return this.ap;
		}
		set {
			ap = value;
		}
	}

	public float MaxHealth {
		get {
			return this.maxHealth;
		}
	}

	public float Health {
		get {
			return this.health;
		}
		set {
			health = value;
		}
	}

	public UnitType Type {
		get {
			return this.type;
		}
	}

	public int Team {
		get {
			return this.team;
		}
		set {
			team = value;
		}
	}

	public Vector2i Position {
		get {
			return this.position;
		}
		set {
			if(unitManager.moveUnit(position, value)) {
				position = value;
			}
		}
	}

	public Vector2i[] PathToWalk {
		get {
			return this.pathToWalk;
		}
		set {
			nextPathIndex = 1;
			pathToWalk = value;

			unitData.startMovement();
		}
	}

	public HashSet<TileType> WalkableTiles {
		get {
			return this.walkableTiles;
		}
	}

	public UnitAction[] Actions {
		get {
			return this.actions;
		}
	}

	public UnitAction DefaultFloorAction {
		get {
			return this.defaultFloorAction;
		}
	}

	public UnitAction DefaultEnemyAction {
		get {
			return this.defaultEnemyAction;
		}
	}

	public UnitAction DefaultAllyAction {
		get {
			return this.defaultAllyAction;
		}
	}

}
