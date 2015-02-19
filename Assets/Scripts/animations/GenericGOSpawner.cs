using UnityEngine;

public enum SpawnMode {
	ENDLESS_RANDOM, ENDLESS_ORDERED, SINGLE_RANDOM, SINGLE_ORDERED
}

public class GenericGOSpawner : MonoBehaviour {
	public GameObject[] prefabsToSpawn;
	public float spawnTimeout = 1;
	public SpawnMode spawnMode = SpawnMode.SINGLE_RANDOM;
	public int numberOfRandomSpawns = 0;

	public Vector3 randomTranslation;
	public Vector3 randomRotation;
	public Vector3 randomScale;

	private float timer;
	private int nextIndex;

	void Start() {
		timer = 0;
		nextIndex = 0;
	}

	void Update() {
		timer -= Time.deltaTime;

		if(timer <= 0) {
			timer = spawnTimeout;

			GameObject selectedPrefab;

			if(spawnMode == SpawnMode.ENDLESS_RANDOM || spawnMode == SpawnMode.SINGLE_RANDOM) {
				selectedPrefab = prefabsToSpawn[(int)(Random.Range(0.0625f, prefabsToSpawn.Length) - 0.0625f)];
				nextIndex++;
			} else {
				selectedPrefab = prefabsToSpawn[nextIndex++];
			}

			GameObject go = (GameObject)Instantiate(selectedPrefab);
			go.transform.position = transform.position + randomTranslation*(Random.value*2 - 1);
			go.transform.rotation = transform.rotation;
			go.transform.localScale = transform.localScale + randomScale*(Random.value*2 - 1);

			if(!Vector3.zero.Equals(randomRotation)) {
				go.transform.Rotate(randomRotation*(Random.value*2 - 1));
			}

			if(spawnMode == SpawnMode.SINGLE_RANDOM && nextIndex >= numberOfRandomSpawns || spawnMode == SpawnMode.SINGLE_ORDERED && nextIndex >= prefabsToSpawn.Length) {
				Destroy(gameObject);

			} else if(spawnMode == SpawnMode.ENDLESS_ORDERED && nextIndex >= prefabsToSpawn.Length) {
				nextIndex = 0;
			}
		}
	}
}

