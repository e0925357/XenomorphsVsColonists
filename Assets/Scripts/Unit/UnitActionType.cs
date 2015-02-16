

public class UnitActionType {

	public static readonly UnitActionType WALK = new UnitActionType(0, "walk");

	private static HighlighterManager highlighterManager = null;

	private readonly int id;
	private readonly string name;

	public static void init(HighlighterManager highlighterManager) {
		UnitActionType.highlighterManager = highlighterManager;
	}


	private UnitActionType(int id, string name) {
		this.id = id;
		this.name = name;
	}

	public UnitAction createAction(Unit unit) {
		switch(id) {
		case 0:
			return new UnitMoveAction(unit, highlighterManager);
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
}

