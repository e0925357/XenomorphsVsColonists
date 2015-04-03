using UnityEngine;
using System.Collections;

public class UnitAssetManager : MonoBehaviour {

	public GameObject xenoPrefab;
	public GameObject xenoMinionPrefab;
	public GameObject colonistPrefab;
	public GameObject soldierPrefab;
	public GameObject eggPrefab;
	public GameObject markerPrefab;

	public PlayerManager playerManager;
	public UnitManager unitManager;

	// Use this for initialization
	void Start () {
		UnitType.init(playerManager, unitManager);

		UnitType.XENO.Prefab = xenoPrefab;
		UnitType.XENO.MarkerPrefab = markerPrefab;
		UnitType.XENO_MINION.Prefab = xenoMinionPrefab;
		UnitType.XENO_MINION .MarkerPrefab = markerPrefab;
		UnitType.COLONIST.Prefab = colonistPrefab;
		UnitType.COLONIST.MarkerPrefab = markerPrefab;
		UnitType.SOLDIER.Prefab = soldierPrefab;
		UnitType.SOLDIER.MarkerPrefab = markerPrefab;
		UnitType.XENO_EGG.Prefab = eggPrefab;
		UnitType.XENO_EGG.MarkerPrefab = markerPrefab;
	}
}
