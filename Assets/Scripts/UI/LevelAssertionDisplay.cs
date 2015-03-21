using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelAssertionDisplay : MonoBehaviour {

	public Text errorText;

	private Dictionary<Tile, DisplayedError> errorDict = new Dictionary<Tile, DisplayedError>();
	
	public void UpdateDisplay (List<LevelAssertionError> errorList, GameBoard gameboard) {
		errorDict.Clear ();

		// Update the values
		bool errorTextWritten = false;

		foreach (LevelAssertionError error in errorList) {
			Tile causingTile = error.CausingTile;
			switch(error.Type) {
			case LevelAssErrType.ROOM_CONNECTION_ENERGY:
			{
				causingTile.TileData.GetComponentInChildren<ErrorGui>().EnergyError = true;

				DisplayedError displayedError;
				if (errorDict.TryGetValue(causingTile, out displayedError)) {
					displayedError.energyError = true;
				}
				else {
					displayedError = new DisplayedError(true, false);
					errorDict[causingTile] = displayedError;
				}
			} break;

			case LevelAssErrType.ROOM_CONNECTION_VENTILATION:
			{
				causingTile.TileData.GetComponentInChildren<ErrorGui>().VentilationError = true;

				DisplayedError displayedError;
				if (errorDict.TryGetValue(causingTile, out displayedError)) {
					displayedError.ventilationError = true;
				}
				else {
					displayedError = new DisplayedError(false, true);
					errorDict[causingTile] = displayedError;
				}
			} break;

			default:
			{
				// Write the error to the errorText
				if(errorText) {
					errorText.text = error.Message;
					errorTextWritten = true;
				}
				else {
					Debug.LogWarning("errorText not set in LevelAssertionDisplay.");
				}
			} break;
			}
		}

		if (!errorTextWritten) {
			errorText.text = string.Empty;
		}

		// Unset unneeded values
		if (gameboard == null) {
			Debug.LogWarning("Gameboard is null in UpdateDisplay in LevelAssertionDisplay!");
			return;
		}
		
		foreach (Vector2i roomPos in gameboard.rooms) {
			Tile roomTile = gameboard.tiles [roomPos.x, roomPos.y];
			ErrorGui errorGui = roomTile.TileData.GetComponentInChildren<ErrorGui>();

			DisplayedError displayedError;
			errorDict.TryGetValue(roomTile, out displayedError);

			if(displayedError == null || !displayedError.energyError) {
				errorGui.EnergyError = false;
			}

			if(displayedError == null || !displayedError.ventilationError) {
				errorGui.VentilationError = false;
			}
		}
	}
}
