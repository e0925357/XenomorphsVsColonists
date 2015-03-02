using UnityEngine;
using System.Collections;

public class TileData : MonoBehaviour {
	public Tile tile = null;

	public void setMaterial(Material mat) {
		setMaterial(mat, transform);
	}
	
	private void setMaterial(Material mat, Transform trans) {
		MeshRenderer renderer = trans.GetComponent<MeshRenderer>();
		
		if(renderer != null) {
			renderer.material = mat;
		}

		for(int i = 0; i < trans.childCount; i++) {
			setMaterial(mat, trans.GetChild(i));
		}
	}
}
