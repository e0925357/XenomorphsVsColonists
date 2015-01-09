using UnityEngine;
using System.Collections;

public class TranslationAnimation : MonoBehaviour {
	public AnimationCurve translationX;
	public AnimationCurve translationY;
	public AnimationCurve translationZ;
	
	public bool loop = false;
	public bool relativeValues = true;

	private Vector3 relativeOffset;
	
	private float time;
	
	// Use this for initialization
	void Start () {
		time = 0;

		if(relativeValues) {
			relativeOffset = transform.localPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if(relativeValues) {
			transform.localPosition = new Vector3(
				translationX.Evaluate(time) + relativeOffset.x,
				translationY.Evaluate(time) + relativeOffset.y,
				translationZ.Evaluate(time) + relativeOffset.z);
		} else {
			transform.localPosition = new Vector3(translationX.Evaluate(time), translationY.Evaluate(time), translationZ.Evaluate(time));
		}
		
		if(loop) {
			float lastKeyX = translationX.length <= 0 ? 0 : translationX.keys[translationX.length-1].time;
			float lastKeyY = translationY.length <= 0 ? 0 : translationY.keys[translationY.length-1].time;
			float lastKeyZ = translationZ.length <= 0 ? 0 : translationZ.keys[translationZ.length-1].time;
			
			float maxTime = Mathf.Max(lastKeyX, Mathf.Max(lastKeyY, lastKeyZ));
			
			while(time > maxTime) {
				time -= maxTime;
			}
		}
	}
}
