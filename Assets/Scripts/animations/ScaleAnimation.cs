using UnityEngine;
using System.Collections;

public class ScaleAnimation : MonoBehaviour {
	public AnimationCurve scaleX;
	public AnimationCurve scaleY;
	public AnimationCurve scaleZ;
	
	public bool loop = false;
	public bool relativeValues = true;
	
	private Vector3 relativeScale;
	
	private float time;
	
	// Use this for initialization
	void Start () {
		time = 0;
		
		if(relativeValues) {
			relativeScale = transform.localScale;
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		
		if(relativeValues) {
			transform.localScale = new Vector3(
				scaleX.Evaluate(time) + relativeScale.x,
				scaleY.Evaluate(time) + relativeScale.y,
				scaleZ.Evaluate(time) + relativeScale.z);
		} else {
			transform.localScale = new Vector3(scaleX.Evaluate(time), scaleY.Evaluate(time), scaleZ.Evaluate(time));
		}
		
		if(loop) {
			float lastKeyX = scaleX.length <= 0 ? 0 : scaleX.keys[scaleX.length-1].time;
			float lastKeyY = scaleY.length <= 0 ? 0 : scaleY.keys[scaleY.length-1].time;
			float lastKeyZ = scaleZ.length <= 0 ? 0 : scaleZ.keys[scaleZ.length-1].time;
			
			float maxTime = Mathf.Max(lastKeyX, Mathf.Max(lastKeyY, lastKeyZ));
			
			while(time > maxTime) {
				time -= maxTime;
			}
		}
	}
}
