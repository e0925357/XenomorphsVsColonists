﻿using UnityEngine;
using System.Collections;

public class UnitData : MonoBehaviour {

	public Unit unit;
	public PlayerManager playerManager;
	public UnitManager unitManager;

	void OnMouseOver() {
		//highlight possible target

		if(Input.GetMouseButtonDown(0)) {
			//Left button
			if(unit.Team == playerManager.currentPlayer) {
				unitManager.SelectedUnit = unit;
			}
		} else if(Input.GetMouseButtonDown(1)) {
			//Right button
			Debug.Log("Right");
		}
	}

	void Update() {
		Vector2i? targetField = unit.NextTile;

		if(targetField.HasValue) {
			Vector3 targetPos = new Vector3(targetField.Value.x*2f, 0f, targetField.Value.y*2f);
			float step = Time.deltaTime*3;
			transform.parent.position += (targetPos - transform.parent.position).normalized*step;

			if(Vector3.Distance(transform.parent.position, targetPos) <= 1.1f*step) {
				unit.tileReached();
			}
		}
	}

	public void startMovement() {
		StartCoroutine("MoveUnit");
	}

	IEnumerable MoveUnit() {
		Debug.Log("Entered MoveUnit...");
		Vector2i? targetField = unit.NextTile;
		Vector3 targetPos;

		while(targetField.HasValue) {
			targetPos = new Vector3(targetField.Value.x*2f, 0f, targetField.Value.y*2f);
			
			while(Vector3.Distance(transform.parent.position, targetPos) > 0.1f) {
				transform.parent.position = Vector3.Lerp(transform.parent.position, targetPos, Time.deltaTime);

				yield return null;
			}

			unit.tileReached();
			targetField = unit.NextTile;
		}
	}
}
