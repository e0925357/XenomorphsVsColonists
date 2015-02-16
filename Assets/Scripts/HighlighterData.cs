using UnityEngine;
using System.Collections;

public class HighlighterData : MonoBehaviour {

	public int x;
	public int y;
	public UnitManager unitManager;

	void OnMouseOver() {
		if(Input.GetMouseButtonDown(1) && unitManager.SelectedUnit != null) {
			unitManager.SelectedUnit.DefaultFloorAction.doAction(new Vector2i(x,y));
		}
	}
}
