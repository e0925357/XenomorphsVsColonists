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

	public PlayerManager playerManager;

	private Vector3[] lastPosition;
	private Quaternion[] lastRotation;

	// Use this for initialization
	void Start () {
		if(playerManager != null) {
			lastPosition = new Vector3[playerManager.playerCount];
			lastRotation = new Quaternion[playerManager.playerCount];

			//Initialize cameras with current position
			for(int i = 0; i < playerManager.playerCount; i++) {
				lastPosition[i] = transform.parent.position;
				lastRotation[i] = transform.parent.rotation;
			}
		}
	}

	void OnEnable() {
		PlayerManager.endTurnEvent += endTurn;
	}
	
	void OnDisable() {
		PlayerManager.endTurnEvent -= endTurn;
	}

	void endTurn(int lastPlayer, int nextPlayer) {
		if(playerManager == null)
			return;

		//save current camera
		lastPosition[lastPlayer-1] = transform.parent.position;
		lastRotation[lastPlayer-1] = transform.parent.rotation;

		//load camera for next player
		transform.parent.position = lastPosition[nextPlayer-1];
		transform.parent.rotation = lastRotation[nextPlayer-1];
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
