using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	private Unit selectedUnit;

	public int currentPlayer = 1;
	public int playerCount = 2;
	public Text unitNameText;
	public Text apText;
	public Text healthText;

	public HighlighterManager highlighterManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endTurn() {
		SelectedUnit = null;

		currentPlayer++;

		if(currentPlayer > playerCount) {
			currentPlayer = 1;
		}
	}

	public Unit SelectedUnit {
		get {
			return selectedUnit;
		} set {
			if(value != null && value.Team != currentPlayer) {
				return;
			}

			highlighterManager.clearSelection();

			if(value != null) {
				unitNameText.text = value.Type.Name;
				apText.text = string.Format("{0}/{1}", value.Ap, value.MaxAP);
				healthText.text = string.Format("{0}/{1}", value.Health, value.MaxHealth);
				highlighterManager.setState(value.Position.x, value.Position.y, HighlighterState.SELECTED);
			} else {
				unitNameText.text = "No Unit Selected";
				apText.text = "-";
				healthText.text = "-";
			}

			selectedUnit = value;
		}
	}
}
