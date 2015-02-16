using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour {
	public Tile[,] tiles;
	public TileType[,] tileTypes = null;
	public HashSet<Vector2i> rooms = null;
	public int sizeX = 20;
	public int sizeY = 20;
	public TilePrefabLibrary tilePrefabLibrary;
	public HighlighterManager highlighterManager;

	// Use this for initialization
	void Start() {
		tilePrefabLibrary.initTileType(this);
		tilePrefabLibrary = null;

		DontDestroyOnLoad(transform.gameObject);

		tiles = new Tile[sizeX,sizeY];

		for(int x = 0; x < sizeX; x++) {
			for(int y = 0; y < sizeY; y++) {
				tiles[x,y] = new Wall(x,y, this);
				tiles[x,y].createGameObject(true);
			}
		}
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	public void bakeLevel() {
		rooms = new HashSet<Vector2i>();
		tileTypes = new TileType[sizeX, sizeY];

		for(int x = 0; x < sizeX; x++) {
			for(int y = 0; y < sizeY; y++) {
				Vector2i pos = new Vector2i(tiles[x,y].X, tiles[x,y].Y);

				if(rooms.Contains(pos)) continue;

				if(tiles[x,y].Width > 1 || tiles[x,y].Height > 1) {
					for(int x2 = tiles[x,y].X; x2 < tiles[x,y].X + tiles[x,y].Width; x2++) {
						for(int y2 = tiles[x,y].Y; y2 < tiles[x,y].Y + tiles[x,y].Height; y2++) {
							if(x2 == tiles[x,y].X || x2 == (tiles[x,y].X + tiles[x,y].Width - 1) || y2 == tiles[x,y].Y || y2 == (tiles[x,y].Y + tiles[x,y].Height - 1)) {
								tileTypes[x2,y2] = TileType.FLOOR;
							} else {
								tileTypes[x2,y2] = TileType.WALL;
							}
						}
					}

					rooms.Add(pos);

				} else {
					tileTypes[x,y] = tiles[x,y].Type;
				}

				tiles[x,y].deactivateCollider();
			}
		}
	}

	public void loadScene(string sceneName) {
		bakeLevel();
		Application.LoadLevel(sceneName);
	}

	public bool swapTiles(Tile newTile) {
		if(!isInside(newTile.X, newTile.Y)) {
			return false;
		}

		if(tiles[newTile.X, newTile.Y].Width == 1 && tiles[newTile.X, newTile.Y].Height == 1 && newTile.Width == 1 && newTile.Height == 1) {
			tiles[newTile.X, newTile.Y].deleteGameObject();
			tiles[newTile.X, newTile.Y] = newTile;
			newTile.createGameObject();

			sendEventToNeighbours(TileEvent.REPLACED, newTile.X, newTile.Y);
		} else {
			clearTileArea(newTile);

			for (int x = newTile.X; x < newTile.X + newTile.Width; x++) {
				for (int y = newTile.Y; y < newTile.Y + newTile.Height; y++) {
					tiles[x,y].deleteGameObject();
					tiles[x,y] = newTile;
				}
			}

			//Update neighbours
			for (int x = newTile.X - 1; x < newTile.X + newTile.Width + 1; x++) {
				for (int y = newTile.Y - 1; y < newTile.Y + newTile.Height + 1; y++) {
					
					if(isInside(x, y) && (x == (newTile.X - 1) || y == (newTile.Y - 1) || x == (newTile.X + newTile.Width) || y == (newTile.Y + newTile.Height))) {
						tiles[x,y].receiveEvent(TileEvent.REPLACED, newTile.X, newTile.Y);
					}
				}
			}

			newTile.createGameObject();
		}

		return true;
	}

	public void clearTileArea(Tile tile, bool createGameObject = false) {
		//Replace tiles recursively
		for (int x = tile.X; x < tile.X + tile.Width; x++) {
			for (int y = tile.Y; y < tile.Y + tile.Height; y++) {
				
				if(tiles[x,y] == tile || (tiles[x,y].Width == 1 && tiles[x,y].Height == 1)) {
					tiles[x,y].deleteGameObject();
					tiles[x,y] = TileType.WALL.createTile(x,y);

					if(createGameObject) {
						tiles[x,y].createGameObject();
					}
				} else {
					clearTileArea(tiles[x,y], true);
				}
			}
		}

		//Update all affected tiles
		for (int x = tile.X-1; x < tile.X + tile.Width + 1; x++) {
			for (int y = tile.Y-1; y < tile.Y + tile.Height + 1; y++) {
				
				if(isInside(x, y)) {
					tiles[x,y].receiveEvent(TileEvent.REPLACED, tile.X, tile.Y);
				}
			}
		}
	}

	public void sendEventToNeighbours(TileEvent eventId, int x, int y, int radius = 1) {
		List<Tile> neighbours = getNeighbours(x, y, radius);

		foreach(Tile t in neighbours) {
			t.receiveEvent(eventId, x, y);
		}
	}

	public bool isInside(int x, int y) {
		return sizeX > x && 0 <= x && sizeY > y && 0 <= y;
	}

	public List<Tile> getNeighbours(int x, int y, int radius = 1) {
		List<Tile> result = new List<Tile>();

		for (int i = -radius; i <= radius; i++) {
			int x2 = x + i;
			for (int j = -radius; j <= radius; j++) {
				int y2 = y + j;

				if(!(x2 == 0 && y2 == 0) && isInside(x2, y2)) {
					result.Add(tiles[x2,y2]);
				}
			}
		}

		return result;
	}
}
