using UnityEngine;
using System.Collections;

public class StickyCamera : MonoBehaviour {

	/// <summary>
	/// The moving speed of the camera.
	/// </summary>
	public float speed = 100;
	
	/// <summary>
	/// The sensitivity for x-roations of the camera
	/// </summary>
	public float sensitivityX = 20;
	
	/// <summary>
	/// The user can no longer control the camera when this member is true.
	/// </summary>
	public bool lockCamera = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dTime = Time.deltaTime;
		Transform parentTransform = transform.parent;
		
		if(!lockCamera) {
			//Move and rotate the camera
			if(Input.GetKey(KeyCode.W)) {
				parentTransform.Translate(0, 0, speed*dTime, parentTransform);
			} else if(Input.GetKey(KeyCode.S)) {
				parentTransform.Translate(0, 0, -speed*dTime, parentTransform);
			}
			
			if(Input.GetKey(KeyCode.D)) {
				parentTransform.Translate(speed*dTime, 0, 0, parentTransform);
			} else if(Input.GetKey(KeyCode.A)) {
				parentTransform.Translate(-speed*dTime, 0, 0, parentTransform);
			}
			
			if(Input.GetKey(KeyCode.Q)) {
				parentTransform.Translate(0, speed*dTime, 0, parentTransform);
			} else if(Input.GetKey(KeyCode.E)) {
				parentTransform.Translate(0, -speed*dTime, 0, parentTransform);
			}

			if(Input.GetMouseButton(2)) {
				float xRotation = Input.GetAxis("Mouse X") * sensitivityX;
				parentTransform.Rotate(0, xRotation, 0);
			}
		}
	}
}
