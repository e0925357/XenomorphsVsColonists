using UnityEngine;

public class UnitActionType {

	public static readonly UnitActionType WALK = new UnitActionType(0, "walk");
	public static readonly UnitActionType SHOOT = new UnitActionType(1, "shoot");
	public static readonly UnitActionType SLASH = new UnitActionType(2, "slash");
	public static readonly UnitActionType LAY_EGG = new UnitActionType(3, "lay egg");

	private static HighlighterManager highlighterManager = null;
	private static GameObject shootPrefab = null;
	private static GameObject slashPrefab = null;

	private readonly int id;
	private readonly string name;
	private Sprite icon;

	public static void init(HighlighterManager highlighterManager, GameObject shootPrefab, GameObject slashPrefab) {
		UnitActionType.highlighterManager = highlighterManager;
		UnitActionType.shootPrefab = shootPrefab;
		UnitActionType.slashPrefab = slashPrefab;
	}


	private UnitActionType(int id, string name) {
		this.id = id;
		this.name = name;
	}

	public UnitAction createAction(Unit unit) {
		switch(id) {
		case 0:
			return new UnitMoveAction(unit, highlighterManager);
		case 1:
			return new ShootAction(unit, highlighterManager, shootPrefab);
		case 2:
			return new SlashAction(unit, highlighterManager, slashPrefab);
		case 3:
			return new LayEggAction(unit, highlighterManager);
		default:
			return null;
		}
	}

	public override string ToString() {
		return string.Format("[UnitActionType: id={0}, name={1}]", id, name);
	}

	public override bool Equals(object obj) {
		if(obj == null)
			return false;
		if(ReferenceEquals(this, obj))
			return true;
		if(obj.GetType() != typeof(UnitActionType))
			return false;
		UnitActionType other = (UnitActionType)obj;
		return id == other.id;
	}
	

	public override int GetHashCode() {
		unchecked {
			return id.GetHashCode();
		}
	}

	public Sprite Icon {
		get {
			return this.icon;
		}

		set {
			icon = value;
		}
	}

	public string Name {
		get {
			return this.name;
		}
	}
}

