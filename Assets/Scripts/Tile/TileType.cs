using UnityEngine;

public class TileType {
	private static readonly TileType[] instances;

	public static readonly TileType WALL = new TileType(0, "Wall", false, false);
	public static readonly TileType FLOOR = new TileType(1, "Floor", false, true);
	public static readonly TileType CABLE = new TileType(2, "Cable Shaft", false, true);
	public static readonly TileType VENT = new TileType(3, "Ventilation Shaft", false, true);
	public static readonly TileType LAB = new TileType(4, "Laboratory", true, false);
	public static readonly TileType REACTOR = new TileType(5, "Reactor", true, false);
	public static readonly TileType AIR = new TileType(6, "Air Purification", true, false);
	public static readonly TileType MINE = new TileType(7, "Mine", true, false);
	public static readonly TileType ARMORY = new TileType(8, "Armory", true, false);
	public static readonly TileType QUATERS = new TileType(9, "Quaters", true, false);

	private static GameBoard gameBoard = null;

	static TileType() {
		instances = new TileType[]{WALL, FLOOR, CABLE, VENT, LAB, REACTOR, AIR, MINE, ARMORY, QUATERS};
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
	private bool isRoom;
	private bool isTransporter;

	private TileType(int id, string name, bool isRoom, bool isTransporter) {
		this.id = id;
		this.name = name;
		this.isRoom = isRoom;
		this.isTransporter = isTransporter;
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
			return new AirPrurifacator(x, y, gameBoard);
		case 7:
			return new Mine(x, y, gameBoard);
		case 8:
			return new Armory(x, y, gameBoard);
		case 9:
			return new Quaters(x, y, gameBoard);
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

	public bool IsRoom {
		get {
			return this.isRoom;
		}
	}

	public bool IsTransporter {
		get {
			return this.isTransporter;
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

