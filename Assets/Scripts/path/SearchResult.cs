using System.Collections.Generic;

public class SearchResult {
	private List<Unit> units = new List<Unit>();
	private Dictionary<Vector2i, List<Vector2i>> pathMap = new Dictionary<Vector2i, List<Vector2i>>();

	public SearchResult() {
	}

	public List<Unit> Units {
		get {
			return this.units;
		}
	}

	public Dictionary<Vector2i, List<Vector2i>> PathMap {
		get {
			return this.pathMap;
		}
	}
}

