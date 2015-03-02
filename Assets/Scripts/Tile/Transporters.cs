
public abstract class Transporters : Tile {
	private static readonly Vector2i[,] roomOffsets = {
		{new Vector2i(-1, 1), new Vector2i(0, 1), new Vector2i(1, 1), new Vector2i(-1, -1), new Vector2i(0, -1), new Vector2i(1, -1)},
		{new Vector2i(-1, 1), new Vector2i(-1, 0), new Vector2i(-1, -1), new Vector2i(1, 1), new Vector2i(1, 0), new Vector2i(1, -1)}
	};

	private static readonly Vector2i[,] transporterOffsets = {
		{new Vector2i(-1, 0), new Vector2i(1, 0)},
		{new Vector2i(0, 1), new Vector2i(0, -1)}
	};

	public Transporters(TileType type, GameBoard gb, int x, int y) : base(type, 0, gb, x,y) {

	}

	public override abstract void receiveEvent(TileEvent eventId, int originX, int originY);

	public override bool isSpawnPositionValid (int x1, int y1) {
		if(!base.isSpawnPositionValid(x1, y1))
			return false;

		int roomTiles = 0;
		int differentTransporter = 0;
		int bestMode1 = 0;
		int bestMode2 = 0;

		for(int i = 0; i < roomOffsets.GetLength(0); i++) {
			int roomTilesCounter = 0;
			int differentTransporterCounter = 0;

			for(int j = 0; j < roomOffsets.GetLength(1); j++) {
				Vector2i pos = roomOffsets[i,j];
				pos.x += x1;
				pos.y += y1;

				if(gameboard.isInside(pos.x, pos.y)) {
					TileType neighbourType = gameboard.tiles[pos.x, pos.y].Type;

					if(neighbourType.IsRoom) {
						roomTilesCounter++;
					} else if(neighbourType.IsTransporter && neighbourType != type) {
						differentTransporterCounter++;
					}
				}
			}

			if(roomTilesCounter > roomTiles) {
				roomTiles = roomTilesCounter;
				bestMode1 = i;
			}

			if(differentTransporterCounter > differentTransporter) {
				differentTransporter = differentTransporterCounter;
				bestMode2 = i;
			}
		}

		int transporterTiles = 0;
		int equalTransporters = 0;
		for(int i = 0; i < transporterOffsets.GetLength(0); i++) {
			int transporterTilesCounter = 0;
			int equalTransportersCounter = 0;
			
			for(int j = 0; j < transporterOffsets.GetLength(1); j++) {
				Vector2i pos = transporterOffsets[i,j];
				pos.x += x1;
				pos.y += y1;
				
				if(gameboard.isInside(pos.x, pos.y)) {
					TileType neighbourType = gameboard.tiles[pos.x, pos.y].Type;
					
					if(neighbourType.IsTransporter) {
						transporterTilesCounter++;

						if(neighbourType == type) {
							equalTransportersCounter++;
						}
					}
				}
			}

			if(bestMode1 == i) {
				transporterTiles = transporterTilesCounter;
			}

			if(bestMode2 == i) {
				equalTransporters = equalTransportersCounter;
			}
		}

		if(transporterTiles > 0 && roomTiles > 1)
			return false;

		if(differentTransporter > 2 && equalTransporters > 0)
			return false;


		return true;
	}
}

