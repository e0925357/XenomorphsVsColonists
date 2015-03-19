using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkingIcon : MonoBehaviour {

	// The time the icon shall be visible and then invisible again
	public float blinkTime = 1;

	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer = (timer + Time.deltaTime) % (blinkTime * 2);

		if (timer > blinkTime) {
			GetComponent<Image>().enabled = false;
		}
		else {
			GetComponent<Image>().enabled = true;
		}
	}
}
