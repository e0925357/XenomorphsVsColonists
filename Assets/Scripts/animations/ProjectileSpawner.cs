using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

	public int numOfProjectiles = 3;
	public float shootingTimeout = 0.6f;
	public GameObject projectilePrefab;

	public Vector3 target;

	private float timer = 0;
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer <= 0) {
			timer = shootingTimeout;

			GameObject go = (GameObject)GameObject.Instantiate(projectilePrefab);
			go.transform.position = transform.position;
			ParticleMover mover = go.GetComponent<ParticleMover>();
			mover.target = target;

			numOfProjectiles--;

			if(numOfProjectiles <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
