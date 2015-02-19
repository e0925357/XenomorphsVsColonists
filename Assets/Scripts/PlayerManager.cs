using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public delegate void onEndTurn(int lastPlayer, int nextPlayer);
	public static event onEndTurn endTurnEvent;

	public static int winningPlayer = 0;


	public int currentPlayer = 1;
	public int playerCount = 2;

	public Canvas HUD_Canvas;
	public Canvas confidentialCanvas;
	public Text nextPlayerText;

	public void endTurn() {

		HUD_Canvas.enabled = false;
		confidentialCanvas.enabled = true;

		int nextPlayer = currentPlayer + 1;

		if(nextPlayer > playerCount) {
			nextPlayer = 1;
		}

		if(endTurnEvent != null) {
			endTurnEvent(currentPlayer, nextPlayer);
		}

		nextPlayerText.text = "Player " + nextPlayer;
		currentPlayer = nextPlayer;
	}

	public void playerLost(int player) {
		winningPlayer = (player)%2 + 1;
		Application.LoadLevel("WinScreen");
	}
}
