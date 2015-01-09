using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
	public Camera m_Camera;
	public bool useMainCamera = true;

	void Start() {
		if(useMainCamera) {
			m_Camera = Camera.main;
		}
	}
	
	void Update()
	{
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
		                 m_Camera.transform.rotation * Vector3.up);
	}
}