
public class AirPrurifacator : Tile {

	public AirPrurifacator(int x, int y, GameBoard gb) : base(TileType.AIR, 0, gb, x, y, 6, 6) {
	}

	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		//do nothing
	}
}

