using UnityEngine;
using UnityEngine.UI;

public class WinScreenScript : MonoBehaviour {

	public Text playerText;

	// Use this for initialization
	void Start () {
		GameObject gameobject = GameObject.Find("GameBoard");

		if(gameobject != null) {
			Destroy(gameobject);
		}

		playerText.text = "Player " + PlayerManager.winningPlayer;
	}
}
