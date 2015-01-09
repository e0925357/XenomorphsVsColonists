using UnityEngine;

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
	protected TileType[] walkableTiles;

	protected UnitAction[] actions;
	protected UnitAction defaultFloorAction;
	protected UnitAction defaultEnemyAction;
	protected UnitAction defaultAllyAction;

	protected GameObject gameObject = null;
	protected PlayerManager playerManager;

	public Unit(int maxAP, float maxHealth, UnitType type, int team, Vector2i position, TileType[] walkableTiles, PlayerManager playerManager) {
		this.maxAP = maxAP;
		ap = maxAP;
		this.maxHealth = maxHealth;
		health = maxHealth;
		this.type = type;
		this.team = team;
		this.position = position;
		this.walkableTiles = walkableTiles;
		this.playerManager = playerManager;
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
		UnitData unitData = gameObject.transform.GetChild(0).GetComponent<UnitData>();
		unitData.unit = this;
		unitData.playerManager = playerManager;
	}

	public void destroyGameObject() {
		if(gameObject == null)
			return;

		GameObject.Destroy(gameObject);
		gameObject = null;
	}
	

	public virtual void nextTurn() {
		ap = maxAP;
	}

	public void tileReached() {
		nextPathIndex++;
	}

	public bool canWalkOn(TileType tileType) {
		if(tileType == null) {
			Debug.LogWarning("Unit.canWalkOn(TileType) has been called with a parameter of value null!");
			return false;
		}

		foreach(TileType tt in walkableTiles) {
			if(tt == tileType) return true;
		}

		return false;
	}

	public Vector2i? NextTile {
		get {
			if(nextPathIndex < 0 || nextPathIndex > pathToWalk.Length) return null;
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
			position = value;
		}
	}

	public Vector2i[] PathToWalk {
		get {
			return this.pathToWalk;
		}
		set {
			pathToWalk = value;
		}
	}

	public TileType[] WalkableTiles {
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
