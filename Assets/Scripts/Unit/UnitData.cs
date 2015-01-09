using UnityEngine;
using System.Collections;

public class UnitData : MonoBehaviour {

	public Unit unit;
	public PlayerManager playerManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver() {
		//highlight possible target

		if(Input.GetMouseButtonDown(0)) {
			//Left button
			if(unit.Team == playerManager.currentPlayer) {
				playerManager.SelectedUnit = unit;
			}
		} else if(Input.GetMouseButtonDown(1)) {
			//Right button
			Debug.Log("Right");
		}
	}
}
