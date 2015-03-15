
public class LevelAssertionError {
	private readonly string message;
	private readonly LevelAssErrType type;
	private Tile causingTile;
	
	public LevelAssertionError(string message, LevelAssErrType type, Tile causingTile = null) {
		this.message = message;
		this.type = type;
		this.causingTile = causingTile;
	}
	
	public string Message {
		get {
			return this.message;
		}
	}

	public LevelAssErrType Type {
		get {
			return this.type;
		}
	}

	public Tile CausingTile {
		get {
			return this.causingTile;
		}
	}
}

