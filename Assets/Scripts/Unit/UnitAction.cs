
public abstract class UnitAction {
	protected bool targetUnit;
	protected bool targetFloor;
	protected int apCost;

	public UnitAction(bool targetUnit, bool targetFloor, int apCost) {
		this.targetUnit = targetUnit;
		this.targetFloor = targetFloor;
		this.apCost = apCost;
	}

	public abstract void doAction();

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
}

