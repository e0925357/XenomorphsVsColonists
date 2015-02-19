using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
	public Camera m_Camera;
	public bool useMainCamera = true;
	public bool useZRotAsOffset = false;

	private float zRot;

	void Start() {
		if(useMainCamera) {
			m_Camera = Camera.main;
		}

		zRot = transform.rotation.eulerAngles.z;
	}
	
	void Update()
	{
		Quaternion upQuat =  m_Camera.transform.rotation;

		if(useZRotAsOffset) {
			Vector3 rot = upQuat.eulerAngles;
			rot.z += zRot;
			upQuat.eulerAngles = rot;
		}

		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
		                 upQuat * Vector3.up);
	}
}