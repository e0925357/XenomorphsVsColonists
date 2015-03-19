using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelAssertionDisplay : MonoBehaviour {

	private Dictionary<Tile, DisplayedError> errorDict = new Dictionary<Tile, DisplayedError>();
	
	public void UpdateDisplay (List<LevelAssertionError> errorList, GameBoard gameboard) {
		errorDict.Clear ();

		// Update the values
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
				// Do nothing atm.
			} break;
			}
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
