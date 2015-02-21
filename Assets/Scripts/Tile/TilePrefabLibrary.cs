using UnityEngine;
using System.Collections;

public class TilePrefabLibrary : MonoBehaviour {

	public GameObject WallPrefab;
	public GameObject WallP_Prefab;

	public GameObject FloorPrefab;
	public GameObject FloorP_Prefab;

	public GameObject CablePrefab;
	public GameObject CableP_Prefab;

	public GameObject VentPrefab;
	public GameObject VentP_Prefab;

	public GameObject MinePrefab;
	public GameObject MineP_Prefab;

	public GameObject LabPrefab;
	public GameObject LabP_Prefab;

	public GameObject ReactorPrefab;
	public GameObject ReactorP_Prefab;

	public GameObject AirPurePrefab;
	public GameObject AirPureP_Prefab;

	public GameObject ArmoryPrefab;
	public GameObject ArmoryP_Prefab;

	public GameObject QuatersPrefab;
	public GameObject QuatersP_Prefab;

	public void initTileType(GameBoard gameBoard) {
		TileType.init(gameBoard);

		TileType.WALL.Prefab = WallPrefab;
		TileType.WALL.PreviewPrefab = WallP_Prefab;
		
		TileType.FLOOR.Prefab = FloorPrefab;
		TileType.FLOOR.PreviewPrefab = FloorP_Prefab;
		
		TileType.CABLE.Prefab = CablePrefab;
		TileType.CABLE.PreviewPrefab = CableP_Prefab;
		
		TileType.VENT.Prefab = VentPrefab;
		TileType.VENT.PreviewPrefab = VentP_Prefab;
		
		TileType.MINE.Prefab = MinePrefab;
		TileType.MINE.PreviewPrefab = MineP_Prefab;
		
		TileType.LAB.Prefab = LabPrefab;
		TileType.LAB.PreviewPrefab = LabP_Prefab;
		
		TileType.REACTOR.Prefab = ReactorPrefab;
		TileType.REACTOR.PreviewPrefab = ReactorP_Prefab;
		
		TileType.AIR.Prefab = AirPurePrefab;
		TileType.AIR.PreviewPrefab = AirPureP_Prefab;

		TileType.ARMORY.Prefab = ArmoryPrefab;
		TileType.ARMORY.PreviewPrefab = ArmoryP_Prefab;

		TileType.QUATERS.Prefab = QuatersPrefab;
		TileType.QUATERS.PreviewPrefab = QuatersP_Prefab;
	}
}
