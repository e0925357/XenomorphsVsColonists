using UnityEngine;

public abstract class Tile {
	protected TileType type;
	protected int orientation;
	protected GameBoard gameboard;
	protected int x;
	protected int y;
	protected GameObject gObject = null;
	protected readonly int width;
	protected readonly int height;

	public Tile (TileType type, int orientation, GameBoard gb, int x, int y, int width = 1, int height = 1) {
		this.orientation = orientation;
		this.type = type;
		gameboard = gb;
		this.x = x;
		this.y = y;
		this.height = height;
		this.width = width;
	}

	public abstract void receiveEvent (TileEvent eventId, int originX, int originY);

	public void createGameObject(bool hide = false) {
		if(type.Prefab != null) {
			gObject = (GameObject)GameObject.Instantiate(type.Prefab);
			gObject.transform.parent = gameboard.transform;
			gObject.transform.position = new Vector3(x*2, 0, y*2);
			TileData td = gObject.GetComponent<TileData>();
			td.tile = this;

			if(hide) {
				MeshRenderer meshRenderer = gObject.transform.GetChild(0).GetComponent<MeshRenderer>();
				meshRenderer.enabled = false;
			}

		} else {
			Debug.LogWarning(this + " entered createGameObject(), but the typ's prefab is null!");
		}
	}

	public void deactivateCollider() {
		gObject.collider.enabled = false;
	}

	public void deleteGameObject() {
		if(gObject != null) {
			GameObject.Destroy(gObject);
		} else {
			//Debug.LogWarning(this + " entered deleteGameObject(), but gObject is null!");
		}
	}

	public virtual bool isSpawnPositionValid(int x1, int y1) {
		return gameboard.isInside(x1, y1) && gameboard.isInside(x1 + width - 1, y1 + height - 1);
	}

	public TileType Type {
		get {
			return this.type;
		}
	}

	public int Orientation {
		get {
			return this.orientation;
		}
		set {
			orientation = value;
		}
	}

	public int X {
		get {
			return this.x;
		}
		set {
			x = value;
		}
	}

	public int Y {
		get {
			return this.y;
		}
		set {
			y = value;
		}
	}

	public int Width {
		get {
			return this.width;
		}
	}

	public int Height {
		get {
			return this.height;
		}
	}

	public override string ToString() {
		return string.Format("[Tile: type={0}, orientation={1}, x={2}, y={3}]", type, orientation, x, y);
	}
}

