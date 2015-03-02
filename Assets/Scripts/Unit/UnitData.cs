using UnityEngine;
using System.Collections;

public class UnitData : MonoBehaviour {

	public Unit unit;
	public PlayerManager playerManager;
	public UnitManager unitManager;

	private ActionManager actionManager;

	void Start() {
		actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();
	}

	void OnMouseOver() {
		if(Input.GetMouseButtonDown(0)) {
			//Left button
			if(unit.Team == playerManager.currentPlayer) {
				unitManager.SelectedUnit = unit;
			}
		} else if(Input.GetMouseButtonDown(1)) {
			actionManager.doAction(unit);
		}
	}

	void OnMouseEnter() {
		renderer.material.color = Color.white;
	}

	void OnMouseExit() {
		renderer.material.color = new Color(0.8f, 0.8f, 0.8f);
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

	public void setVisible(bool visible) {
		setVisible(visible, transform);
	}

	private void setVisible(bool visible, Transform trans) {
		MeshRenderer renderer = trans.GetComponent<MeshRenderer>();
		
		if(renderer != null) {
			renderer.enabled = visible;
		}

		Collider collider = trans.collider;
		
		if(collider != null) {
			collider.enabled = visible;
		}
		
		for(int i = 0; i < trans.childCount; i++) {
			setVisible(visible, trans.GetChild(i));
		}
	}
}
