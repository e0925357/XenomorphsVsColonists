using UnityEngine;
using System.Collections;

public class ReadyButtonScript : MonoBehaviour {
	
	public GameBoard gameboard;
	public LevelAssertion levelAssertion;
	public LevelAssertionDisplay levelAssertionDisplay;
	
	public void tryChangeLevel() {
		levelAssertion.assertLevel();
		
		if(!gameboard.checkLevel || levelAssertion.IsLevelValid) {
			Debug.Log("Starting with current level...");
			
			gameboard.bakeLevel();
			Application.LoadLevel("GameTurns");
		} else {
			Debug.LogWarning("Can't change Level!");
			foreach(LevelAssertionError error in levelAssertion.AssertionErrorList) {
				Debug.LogWarning(error.Type + ", message=" + error.Message);
			}

			if(levelAssertionDisplay != null) {
				levelAssertionDisplay.UpdateDisplay(levelAssertion.AssertionErrorList, gameboard);
			}
			else {
				Debug.LogWarning("LevelAssertionDisplay is not set in ReadyButtonScript!");
			}
		}
	}
}
