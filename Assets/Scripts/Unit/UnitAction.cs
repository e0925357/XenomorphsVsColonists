
public abstract class UnitAction {
	protected bool targetUnit;
	protected bool targetFloor;
	protected int apCost;
	protected Unit unit;
	protected UnitActionType type;

	protected HighlighterManager highlighterManager;

	public UnitAction(bool targetUnit, bool targetFloor, int apCost, Unit unit, UnitActionType type, HighlighterManager highlighterManager) {
		this.targetUnit = targetUnit;
		this.targetFloor = targetFloor;
		this.apCost = apCost;
		this.unit = unit;
		this.type = type;
		this.highlighterManager = highlighterManager;
	}

	public abstract void doAction(Vector2i position);
	public abstract void doAction(Unit unit);
	public abstract int apCostForTile(Vector2i position);
	public abstract void actionSelected();

	public bool TargetUnit {
		get {
			return this.targetUnit;
		}
	}

	public bool TargetFloor {
		get {
			return this.targetFloor;
		}
	}

	public int ApCost {
		get {
			return this.apCost;
		}
	}

	public UnitActionType Type {
		get {
			return this.type;
		}
	}
}

