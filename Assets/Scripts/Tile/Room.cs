
public abstract class Room : Tile {
	public Room(TileType type, GameBoard gb, int x, int y, int width, int height) :
		base(type, 0, gb, x,y, width, height) {
	}
	
	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		
		
		receiveEventHook(eventId, originX, originY);
	}
	
	public override bool isSpawnPositionValid (int x1, int y1) {
		if(!base.isSpawnPositionValid (x1, y1)) return false;
		
		for(int xOff = -1; xOff <= width; xOff++) {
			for(int yOff = -1; yOff <= height; yOff++) {
				if(xOff > -1 && xOff < width && yOff > -1 && yOff < height) {
					continue;
				}
				
				int x2 = x1 + xOff;
				int y2 = y1 + yOff;
				
				if(!gameboard.isInside(x2, y2)) continue;
				
				Tile neighbourTile = gameboard.tiles[x2, y2];
				
				if(neighbourTile.Type == TileType.WALL) continue;
				
				if(neighbourTile.Type == TileType.FLOOR || neighbourTile.Type == TileType.VENT
				   || neighbourTile.Type == TileType.CABLE) {
				
					Vector2i[] nnOffsets = null;
					
					if((xOff == -1 || xOff == width) && yOff > -1 && yOff < height) {
						nnOffsets = new Vector2i[2];
						nnOffsets[0] = new Vector2i(xOff, yOff-1);
						nnOffsets[1] = new Vector2i(xOff, yOff+1);
						
					} else if(xOff > -1 && xOff < width && (yOff == -1 || yOff == height)) {
						nnOffsets = new Vector2i[2];
						nnOffsets[0] = new Vector2i(xOff-1, yOff);
						nnOffsets[1] = new Vector2i(xOff+1, yOff);
					}
					
					if(nnOffsets != null) {
						//Check neighbours of neighbours for equal types
						for(int i = 0; i < nnOffsets.Length; i++) {
							if(!gameboard.isInside(nnOffsets[i].x, nnOffsets[i].y)) {
								continue;
							}
							
							Tile nnTile = gameboard.tiles[nnOffsets[i].x, nnOffsets[i].y];
							
							if(nnTile.Type == neighbourTile.Type) {
								return false;
							}
						}
					}
				} else {
					//No walls, no moveable tiles -> it has to be a room
					return false;
				}
			}
		}
		
		return true;
	}
	
	protected abstract void receiveEventHook(TileEvent eventId, int originX, int originY);
}

