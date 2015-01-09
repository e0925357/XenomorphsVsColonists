using UnityEngine;

public class TileType {
	private static readonly TileType[] instances;

	public static readonly TileType WALL = new TileType(0, "Wall");
	public static readonly TileType FLOOR = new TileType(1, "Floor");
	public static readonly TileType CABLE = new TileType(2, "Cable Shaft");
	public static readonly TileType VENT = new TileType(3, "Ventilation Shaft");
	public static readonly TileType LAB = new TileType(4, "Laboratory");
	public static readonly TileType REACTOR = new TileType(5, "Reactor");
	public static readonly TileType AIR = new TileType(6, "Air Purification");
	public static readonly TileType MINE = new TileType(7, "Mine");

	private static GameBoard gameBoard = null;

	static TileType() {
		instances = new TileType[]{WALL, FLOOR, CABLE, VENT, LAB, REACTOR, AIR, MINE};
	}

	public static TileType getById(int id) {
		if(id < 0 || id >= instances.Length) {
			return null;
		}

		return instances[id];
	}

	public static void init(GameBoard gameBoard) {
		TileType.gameBoard = gameBoard;
	}

	private readonly int id;
	private readonly string name;
	private GameObject prefab;
	private GameObject previewPrefab;

	private TileType(int id, string name) {
		this.id = id;
		this.name = name;
	}

	public Tile createTile(int x, int y) {
		switch(id) {
		case 0:
			return new Wall(x, y, gameBoard);
		case 1:
			return new Floor(x, y, gameBoard);
		case 2:
			return new CableShaft(x, y, gameBoard);
		case 3:
			return new VentShaft(x, y, gameBoard);
		case 4:
			return new Laboratory(x, y, gameBoard);
		case 5:
			return new Reactor(x, y, gameBoard);
		case 6:
			return null;
		case 7:
			return new Mine(x, y, gameBoard);
		default:
			return null;
		}
	}

	public int Id {
		get {
			return this.id;
		}
	}

	public GameObject Prefab {
		get {
			return this.prefab;
		}
		set {
			prefab = value;
		}
	}

	public GameObject PreviewPrefab {
		get {
			return this.previewPrefab;
		}
		set {
			previewPrefab = value;
		}
	}

	public override string ToString() {
		return string.Format("[TileType: {1}|{0}]", id, name);
	}

	public override bool Equals(object obj) {
		if(obj == null)
			return false;
		if(ReferenceEquals(this, obj))
			return true;
		if(obj.GetType() != typeof(TileType))
			return false;
		TileType other = (TileType)obj;
		return Id == other.Id;
	}
	

	public override int GetHashCode() {
		unchecked {
			return Id.GetHashCode();
		}
	}
}

