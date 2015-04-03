using UnityEngine;
using System.Collections.Generic;

public enum HighlighterState {
	HIDDEN, SELECTED, WALKABLE, ENEMY, ATTACKABLE, SUPPORTABLE, TEAMMATE
}

public class HighlighterManager : MonoBehaviour {

	public Material selectedMaterial;
	public Material walkableMaterial;
	public Material enemyMaterial;
	public Material attackableMaterial;
	public Material teamMateMaterial;
	public Material supportableMaterial;

	public GameObject highligherPrefab;

	public UnitManager unitManager;
	public ActionManager actionManager;

	private GameBoard gameboard;
	private HighlighterData[,] highlighters;
	private HighlighterState[,] highlighterStates;
	private List<HighlighterData> activeHighlighterList;

	// Use this for initialization
	void Start () {
		gameboard = GameObject.Find("GameBoard").GetComponent<GameBoard>();
		gameboard.highlighterManager = this;

		highlighters = new HighlighterData[gameboard.sizeX, gameboard.sizeY];
		highlighterStates = new HighlighterState[gameboard.sizeX, gameboard.sizeY];

		for(int x = 0; x < gameboard.sizeX; x++) {
			for(int y = 0; y < gameboard.sizeY; y++) {
				highlighters[x, y] = ((GameObject)GameObject.Instantiate(highligherPrefab)).GetComponent<HighlighterData>();
				highlighters[x, y].transform.parent = transform;
				highlighters[x, y].transform.position = new Vector3(x*2, 0, y*2);
				highlighters[x, y].x = x;
				highlighters[x, y].y = y;
				highlighters[x, y].unitManager = unitManager;
				highlighters[x, y].actionManager = actionManager;
				highlighterStates[x, y] = HighlighterState.HIDDEN;
				MeshRenderer[] meshRenderers = highlighters[x, y].GetComponentsInChildren<MeshRenderer>();

				foreach(MeshRenderer meshRenderer in meshRenderers) {
					meshRenderer.enabled = false;
				}
			}
		}

		activeHighlighterList = new List<HighlighterData>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clearSelection() {
		while(activeHighlighterList.Count > 0){
			setState(activeHighlighterList[0], HighlighterState.HIDDEN);
		}
	}

	public void setState(int x, int y, HighlighterState newState) {
		setState(highlighters[x, y], newState);
	}

	private void setState(HighlighterData gameobject, HighlighterState state) {
		int x = gameobject.x;
		int y = gameobject.y;

		if(highlighterStates[x, y] == state)
			return;

		//Set graphical state
		MeshRenderer[] meshRenderers = gameobject.GetComponentsInChildren<MeshRenderer>();
		
		foreach(MeshRenderer meshRenderer in meshRenderers) {
			meshRenderer.enabled = state != HighlighterState.HIDDEN;

			switch(state) {
			case HighlighterState.ENEMY:
				meshRenderer.material = enemyMaterial;
				break;
			case HighlighterState.SELECTED:
				meshRenderer.material = selectedMaterial;
				break;
			case HighlighterState.WALKABLE:
				meshRenderer.material = walkableMaterial;
				break;
			case HighlighterState.ATTACKABLE:
				meshRenderer.material = attackableMaterial;
				break;
			case HighlighterState.SUPPORTABLE:
				meshRenderer.material = supportableMaterial;
				break;
			case HighlighterState.TEAMMATE:
				meshRenderer.material = teamMateMaterial;
				break;
			default:
				break;
			}
		}

		//set state
		if(state == HighlighterState.HIDDEN) {
			activeHighlighterList.Remove(highlighters[x, y]);
		} else if(highlighterStates[x, y] == HighlighterState.HIDDEN) {
			activeHighlighterList.Add(highlighters[x, y]);
		}
		
		highlighterStates[x, y] = state;
	}
}
