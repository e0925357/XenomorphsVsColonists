
public class Quaters : Room {
	public Quaters(int x, int y, GameBoard gb) : base(TileType.QUATERS, gb, x, y, 6, 6) {
	}

	protected override void receiveEventHook (TileEvent eventId, int originX, int originY) {
		//Do nothing
	}
}

