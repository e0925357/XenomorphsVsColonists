using UnityEngine;
using System.Collections;

public class RotationAnimation : MonoBehaviour {
	public AnimationCurve rotationX;
	public AnimationCurve rotationY;
	public AnimationCurve rotationZ;

	public bool loop = false;
	public bool relativeValues = true;
	
	private Vector3 relativeOffset;

	private float time;

	// Use this for initialization
	void Start () {
		time = 0;
		
		if(relativeValues) {
			relativeOffset = transform.localRotation.eulerAngles;
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		Quaternion newRotation;

		if(relativeValues) {
			newRotation = Quaternion.Euler(
				rotationX.Evaluate(time) + relativeOffset.x,
				rotationY.Evaluate(time) + relativeOffset.y,
				rotationZ.Evaluate(time) + relativeOffset.z);
		} else {
			newRotation = Quaternion.Euler(rotationX.Evaluate(time), rotationY.Evaluate(time), rotationZ.Evaluate(time));
		}
		transform.localRotation = newRotation;

		if(loop) {
			float lastKeyX = rotationX.length <= 0 ? 0 : rotationX.keys[rotationX.length-1].time;
			float lastKeyY = rotationY.length <= 0 ? 0 : rotationY.keys[rotationY.length-1].time;
			float lastKeyZ = rotationZ.length <= 0 ? 0 : rotationZ.keys[rotationZ.length-1].time;

			float maxTime = Mathf.Max(lastKeyX, Mathf.Max(lastKeyY, lastKeyZ));

			while(time > maxTime) {
				time -= maxTime;
			}
		}
	}
}
