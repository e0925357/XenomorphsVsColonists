
public class LevelAssertionError {
	private readonly string message;
	private readonly LevelAssErrType type;
	
	public LevelAssertionError(string message, LevelAssErrType type) {
		this.message = message;
		this.type = type;
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
}

