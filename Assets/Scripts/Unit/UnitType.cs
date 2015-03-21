using UnityEngine;

public class UnitType {
	private static readonly UnitType[] instances;
	
	public static readonly UnitType COLONIST = new UnitType(0, "Colonist");
	public static readonly UnitType SOLDIER = new UnitType(1, "Soldier");
	public static readonly UnitType XENO = new UnitType(2, "Xenomorph");
	public static readonly UnitType XENO_EGG = new UnitType(3, "Xenomorph Egg");

	private static PlayerManager playerManager;
	private static UnitManager unitManager;
	
	static UnitType() {
		instances = new UnitType[]{COLONIST, SOLDIER, XENO, XENO_EGG};
	}
	
	public static UnitType getById(int id) {
		if(id < 0 || id >= instances.Length) {
			return null;
		}
		
		return instances[id];
	}
	
	private readonly int id;
	private readonly string name;
	private GameObject prefab;
	private GameObject markerPrefab;

	public static void init(PlayerManager playerManager, UnitManager unitManager) {
		UnitType.playerManager = playerManager;
		UnitType.unitManager = unitManager;
	}
	
	private UnitType(int id, string name) {
		this.id = id;
		this.name = name;
	}
	
	public Unit createUnit(int x, int y) {
		switch(id) {
		case 0:
			return new Colonist(new Vector2i(x, y), playerManager, unitManager);
		case 1:
			return new Soldier(new Vector2i(x, y), playerManager, unitManager);
		case 2:
			return new Xenomorph(new Vector2i(x, y), playerManager, unitManager);
		case 3:
			return new XenomorphEgg(new Vector2i(x, y), playerManager, unitManager);
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
	
	public GameObject MarkerPrefab {
		get {
			return this.markerPrefab;
		}
		set {
			markerPrefab = value;
		}
	}

	public string Name {
		get {
			return name;
		}
	}
	
	public override string ToString() {
		return string.Format("[UnitType: {1}|{0}]", id, name);
	}
	
	public override bool Equals(object obj) {
		if(obj == null)
			return false;
		if(ReferenceEquals(this, obj))
			return true;
		if(obj.GetType() != typeof(UnitType))
			return false;
		UnitType other = (UnitType)obj;
		return Id == other.Id;
	}
	
	
	public override int GetHashCode() {
		unchecked {
			return Id.GetHashCode();
		}
	}
}

