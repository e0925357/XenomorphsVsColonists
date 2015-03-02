
public class AirPrurifacator : Room {

	public AirPrurifacator(int x, int y, GameBoard gb) : base(TileType.AIR, gb, x, y, 6, 6) {
	}

	protected override void receiveEventHook (TileEvent eventId, int originX, int originY) {
		//Do nothing
	}
}

