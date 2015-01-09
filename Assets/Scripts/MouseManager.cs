using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

	public bool overGUI = false;

	private Rect elementSize;

	// Use this for initialization
	void Start () {
		RectTransform rectTransform = GetComponent<RectTransform>();

		Vector3 transformPos = transform.position - (new Vector3(rectTransform.pivot.x*rectTransform.rect.size.x, rectTransform.pivot.y*rectTransform.rect.size.y, 0));
		elementSize = new Rect(transformPos.x, transformPos.y, rectTransform.rect.size.x, rectTransform.rect.size.y);

	}
	
	// Update is called once per frame
	void Update () {
		overGUI = elementSize.Contains(Input.mousePosition);
	}
}
