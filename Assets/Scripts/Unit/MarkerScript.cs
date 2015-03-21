using UnityEngine;
using System.Collections;

public class MarkerScript : MonoBehaviour {

	public Unit markedUnit = null;
	public PlayerManager playerManager;
	public bool markerActive = false;

	void OnEnable() {
		FogManager.unitVisibleEvent += unitVisible;
		FogManager.unitInvisibleEvent += unitInvisible;
		PlayerManager.endTurnEvent += onEndTurn;
	}
	
	void OnDisable() {
		FogManager.unitVisibleEvent -= unitVisible;
		FogManager.unitInvisibleEvent -= unitInvisible;
		PlayerManager.endTurnEvent -= onEndTurn;
	}

	public void unitVisible(Unit unit) {
		if (!unit.Equals (markedUnit))
						return;

		markerActive = false;
		updateVisibility ();
	}

	public void unitInvisible(Unit unit) {
		if (!unit.Equals (markedUnit))
			return;

		transform.position = new Vector3(markedUnit.Position.x*2, 0, markedUnit.Position.y*2);
		markerActive = true;
		updateVisibility ();
	}

	public void onEndTurn(int lastPlayer, int nextPlayer) {
		setVisible(markedUnit != null && nextPlayer != markedUnit.Team && markerActive);
	}

	public void updateVisibility() {
		setVisible(markedUnit != null && playerManager.currentPlayer != markedUnit.Team && markerActive);
	}

	private void setVisible(bool visible) {
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
