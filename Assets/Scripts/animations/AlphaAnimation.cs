using UnityEngine;
using System.Collections;

public enum AnimationEndBehaviour {
	STOP, LOOP, DESTROY
}

public class AlphaAnimation : MonoBehaviour {

	public AnimationCurve alphaAnimation;
	public AnimationEndBehaviour animationEnd = AnimationEndBehaviour.DESTROY;

	private float timer;
	private bool isRunning;

	// Use this for initialization
	void Start () {
		timer = 0;
		isRunning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isRunning)
			return;

		timer += Time.deltaTime;

		Color c = renderer.material.color;
		c.a = alphaAnimation.Evaluate(timer);
		renderer.material.color = c;

		float lastKey = alphaAnimation.length <= 0 ? 0 : alphaAnimation.keys[alphaAnimation.length-1].time;

		if(timer > lastKey) {
			switch(animationEnd) {
			case AnimationEndBehaviour.LOOP:
				timer -= lastKey;
				break;
			case AnimationEndBehaviour.DESTROY:
				isRunning = false;
				Destroy(gameObject);
				break;
			default:
				isRunning = false;
				break;
			}
		}

	}
}
