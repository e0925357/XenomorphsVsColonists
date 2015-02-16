﻿using UnityEngine;
using System.Collections;

public class UnitAssetManager : MonoBehaviour {

	public GameObject xenoPrefab;
	public GameObject colonistPrefab;
	public GameObject soldierPrefab;

	public PlayerManager playerManager;
	public UnitManager unitManager;

	// Use this for initialization
	void Start () {
		UnitType.init(playerManager, unitManager);

		UnitType.XENO.Prefab = xenoPrefab;
		UnitType.COLONIST.Prefab = colonistPrefab;
		UnitType.SOLDIER.Prefab = soldierPrefab;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
