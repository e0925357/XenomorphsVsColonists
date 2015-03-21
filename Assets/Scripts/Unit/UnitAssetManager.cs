using UnityEngine;
using System.Collections;

public class UnitAssetManager : MonoBehaviour {

	public GameObject xenoPrefab;
	public GameObject colonistPrefab;
	public GameObject soldierPrefab;
	public GameObject eggPrefab;

	public PlayerManager playerManager;
	public UnitManager unitManager;

	// Use this for initialization
	void Start () {
		UnitType.init(playerManager, unitManager);

		UnitType.XENO.Prefab = xenoPrefab;
		UnitType.COLONIST.Prefab = colonistPrefab;
		UnitType.SOLDIER.Prefab = soldierPrefab;
		UnitType.XENO_EGG.Prefab = eggPrefab;
	}
}
