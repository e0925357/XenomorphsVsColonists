using UnityEngine;
using UnityEngine.UI;

public class VisibilitySwitcher : MonoBehaviour {

	public Canvas[] canvases;

	public void switchVisibility() {
		if(canvases != null) {
			foreach(Canvas canvas in canvases) {
				canvas.enabled = !canvas.enabled;
			}
		}
	}
}
