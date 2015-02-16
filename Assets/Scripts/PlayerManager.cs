using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public delegate void onEndTurn();
	public static event onEndTurn endTurnEvent;


	public int currentPlayer = 1;
	public int playerCount = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endTurn() {

		if(endTurnEvent != null) {
			endTurnEvent();
		}

		currentPlayer++;

		if(currentPlayer > playerCount) {
			currentPlayer = 1;
		}
	}
}
