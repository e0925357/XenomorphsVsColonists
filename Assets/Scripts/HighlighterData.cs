using UnityEngine;
using System.Collections;

public class HighlighterData : MonoBehaviour {

	public int x;
	public int y;
	public UnitManager unitManager;
	public ActionManager actionManager;

	void OnMouseOver() {
		if(Input.GetMouseButtonDown(1) && unitManager.SelectedUnit != null) {
			actionManager.doAction(x, y);
//			unitManager.SelectedUnit.DefaultFloorAction.doAction(new Vector2i(x,y));
		}
	}
}
