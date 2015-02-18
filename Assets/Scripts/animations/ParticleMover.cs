using UnityEngine;
using System.Collections;

public class ParticleMover : MonoBehaviour {

	public Vector3 target;
	public float speed;

	void Start() {
		transform.LookAt(target);
		Debug.Log("Look @ " + target);
	}

	// Update is called once per frame
	void Update () {
		Vector3 direction = (target - transform.position).normalized;
		float step = speed*Time.deltaTime;

		//move
		transform.position += direction*step;

		if(Vector3.Distance(target, transform.position) <= step*1.1f) {
			GameObject.Destroy(gameObject);
		}
	}
}
