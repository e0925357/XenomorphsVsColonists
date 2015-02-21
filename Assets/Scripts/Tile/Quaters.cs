
public class Quaters : Tile {
	public Quaters(int x, int y, GameBoard gb) : base(TileType.QUATERS, 0, gb, x, y, 6, 6) {
	}

	public override void receiveEvent (TileEvent eventId, int originX, int originY) {
		//do nothing
	}
}

