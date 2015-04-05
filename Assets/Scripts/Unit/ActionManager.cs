using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour {

	public UnitManager unitManager;

	public Image[] actionImages;
	public Button[] actionButtons;
	public Text[] actionTexts;

	public int selectedActionIndex;

	// Use this for initialization
	void Start () {
		disableAllActions();
	}

	private void disableAllActions() {
		selectedActionIndex = -1;

		for(int i = 0; i < actionImages.Length; i++) {
			actionImages[i].enabled = false;
			actionButtons[i].enabled = false;
			actionTexts[i].enabled = false;
		}
	}

	public void unitSelected(Unit unit) {
		disableAllActions();

		if(unit == null)
			return;

		for(int i = 0; i < unit.Actions.Length; i++) {
			actionImages[i].sprite = unit.Actions[i].Type.Icon;
			actionImages[i].enabled = true;
			actionButtons[i].enabled = true;
			actionTexts[i].text = unit.Actions[i].ApCost + " AP";
			actionTexts[i].enabled = true;
		}

		if(unit.DefaultFloorAction != null) {
			unit.DefaultFloorAction.actionSelected();

		} else if(unit.DefaultEnemyAction != null) {
			unit.DefaultEnemyAction.actionSelected();

		} else if(unit.DefaultAllyAction != null) {
			unit.DefaultAllyAction.actionSelected();
		}
	}

	public void selectAction(int index) {
		if(unitManager.SelectedUnit == null) {
			Debug.LogWarning("Tried to select action #" + index + ", but there is no selected unit!");
			return;
		} if(index < 0 || index >= unitManager.SelectedUnit.Actions.Length) {
			Debug.LogWarning("Tried to select action #" + index + ", but the index is out of range!");
			return;
		}

		selectedActionIndex = index;
		unitManager.SelectedUnit.Actions[index].actionSelected();
	}

	public void doAction(int x, int y) {
		if(unitManager.SelectedUnit == null) {
			Debug.LogWarning("Tried to do action @(" + x + "," + y + "), but there is no selected unit!");
			return;
		}

		UnitAction selectedAction;

		if(selectedActionIndex >= 0) {
			selectedAction = unitManager.SelectedUnit.Actions[selectedActionIndex];
		} else {
			selectedAction = unitManager.SelectedUnit.DefaultFloorAction;

			if(selectedAction == null) {
				Unit targetUnit = unitManager.getUnit(new Vector2i(x,y));
				
				if(targetUnit == null) {
					Debug.LogWarning("Tried to do default action @(" + x + "," + y + "), but there is no target unit!");
					return;
				}

				//determine unitAction
				if(unitManager.SelectedUnit.Team == targetUnit.Team) {
					selectedAction = unitManager.SelectedUnit.DefaultAllyAction;
				} else {
					selectedAction = unitManager.SelectedUnit.DefaultEnemyAction;
				}

				if(selectedAction == null) {
					Debug.LogWarning("Tried to do default action @(" + x + "," + y + "), but there is no matching action!");
					return;
				}
			}

			selectedAction.actionSelected();
		}

		if(!selectedAction.TargetFloor) {
			Unit targetUnit = unitManager.getUnit(new Vector2i(x,y));

			if(targetUnit != null) {
				doAction(targetUnit);
			} else {
				Debug.LogWarning("Tried to do action(" + selectedAction.Type.Name + ") @(" + x + "," + y + "), but there is no target unit!");
			}
			return;
		}

		selectedAction.doAction(new Vector2i(x, y));
	}

	public void doAction(Unit target) {
		if(unitManager.SelectedUnit == null) {
			Debug.LogWarning("Tried to do action @(" + target + "), but there is no selected unit!");
			return;
		} if(target == null) {
			Debug.LogWarning("Tried to do action @unit, but the given target is null!");
			return;
		}

		UnitAction selectedAction;
		
		if(selectedActionIndex >= 0) {
			selectedAction = unitManager.SelectedUnit.Actions[selectedActionIndex];
		} else {
			if(target.Team == unitManager.SelectedUnit.Team) {
				selectedAction = unitManager.SelectedUnit.DefaultAllyAction;
			} else {
				selectedAction = unitManager.SelectedUnit.DefaultEnemyAction;
			}

			if(selectedAction == null) {
				selectedAction = unitManager.SelectedUnit.DefaultFloorAction;

				if(selectedAction == null) {
					Debug.LogWarning("Tried to do default action @(" + target + "), but there is no matching action!");
					return;
				}
			}

			selectedAction.actionSelected();
		}

		if(!selectedAction.TargetUnit) {
			doAction(target.Position.x, target.Position.y);
			return;
		}

		selectedAction.doAction(target);
	}
}
