
public class Armory : Room {
	public Armory(int x, int y, GameBoard gb) : base(TileType.ARMORY, gb, x, y, 6, 6) {
	}

	protected override void receiveEventHook (TileEvent eventId, int originX, int originY) {
		//Do nothing
	}
}

