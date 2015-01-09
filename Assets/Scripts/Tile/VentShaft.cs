using UnityEngine;
using System.Collections.Generic;

public class VentShaft : Tile {
	public VentShaft(int x, int y, GameBoard gb) : base(TileType.VENT, 0, gb, x, y) {
	}
	
	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		if(TileEvent.REPLACED.Equals(eventId)) {
			
			//1st disable all
			gObject.transform.GetChild(0).rotation = Quaternion.identity;
			
			for(int i = 0; i < gObject.transform.GetChild(0).childCount; i++) {
				MeshRenderer renderer = gObject.transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>();
				renderer.enabled = false;
			}
			
			//2nd find the correct visual
			if(isDockable(0, 1) && isDockable(0, -1) && isDockable(1, 0) && isDockable(-1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/X").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
			} else if(isDockable(0, 1) && isDockable(0, -1) && isDockable(-1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/T").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
			} else if(isDockable(0, 1) && isDockable(1, 0) && isDockable(-1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/T").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
				gObject.transform.GetChild(0).Rotate(0, 90, 0);
				
			} else if(isDockable(0, 1) && isDockable(0, -1) && isDockable(1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/T").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
				gObject.transform.GetChild(0).Rotate(0, 180, 0);
				
			} else if(isDockable(0, -1) && isDockable(1, 0) && isDockable(-1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/T").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
				gObject.transform.GetChild(0).Rotate(0, -90, 0);
				
			} else if(isDockable(0, 1) && isDockable(-1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/L").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
			} else if(isDockable(0, 1) && isDockable(1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/L").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
				gObject.transform.GetChild(0).Rotate(0, 90, 0);
				
			}  else if(isDockable(0, -1) && isDockable(1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/L").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
				gObject.transform.GetChild(0).Rotate(0, 180, 0);
				
			} else if(isDockable(0, -1) && isDockable(-1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/L").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
				gObject.transform.GetChild(0).Rotate(0, -90, 0);
				
			} else if(isDockable(1, 0) || isDockable(-1, 0)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/I").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
			} else if(isDockable(0, 1) || isDockable(0, -1)) {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/I").GetComponent<MeshRenderer>();
				renderer.enabled = true;
				
				gObject.transform.GetChild(0).Rotate(0, -90, 0);
				
			} else {
				MeshRenderer renderer = gObject.transform.Find("Rotation Anchor/X").GetComponent<MeshRenderer>();
				renderer.enabled = true;
			}
		}
	}
	
	private bool isDockable(int dX, int dY) {
		int x2 = x + dX;
		int y2 = y + dY;
		
		if(!gameboard.isInside(x2, y2))
			return false;
		
		Tile neighbour = gameboard.tiles[x2, y2];
		
		return neighbour.Type == TileType.VENT || neighbour.Type == TileType.FLOOR;
	}
}