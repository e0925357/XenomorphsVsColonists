using UnityEngine;
using System.Collections.Generic;

public class BreadthFirstSearch {

	private GameBoard gameBoard;
	private UnitManager unitManager;
	private Queue<SearchStep> openList = new Queue<SearchStep>();
	private HashSet<SearchStep> visitedSet = new HashSet<SearchStep>();

	private static readonly Vector2i[] neighbourOffsets = {new Vector2i(1, 0), new Vector2i(0, 1), new Vector2i(-1, 0), new Vector2i(0, -1)};

	public BreadthFirstSearch() {
		gameBoard = GameObject.Find("GameBoard").GetComponent<GameBoard>();
		unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
	}

	public SearchResult searchReagion(Vector2i startField, int maxSteps, bool ignoreUnits, HashSet<TileType> walkableTiles) {
		openList.Clear();
		visitedSet.Clear();

		openList.Enqueue(new SearchStep(startField, null));
		visitedSet.Add(new SearchStep(startField, null));

		SearchResult result = new SearchResult();

		while(openList.Count > 0) {
			SearchStep currentStep = openList.Dequeue();

			if(unitManager.getUnit(currentStep.Position) != null) {
				result.Units.Add(unitManager.getUnit(currentStep.Position));
			}

			foreach(Vector2i offsetVec in neighbourOffsets) {
				SearchStep neighbourStep = new SearchStep(currentStep.Position + offsetVec, currentStep);

				if(neighbourStep.Step > maxSteps || visitedSet.Contains(neighbourStep) || !gameBoard.isInside(neighbourStep.Position.x, neighbourStep.Position.y) ||
				   !walkableTiles.Contains(gameBoard.tileTypes[neighbourStep.Position.x, neighbourStep.Position.y])) continue;

				visitedSet.Add(neighbourStep);
				openList.Enqueue(neighbourStep);
			}
		}

		foreach(SearchStep sStep in visitedSet) {
			List<Vector2i> path = new List<Vector2i>();
			SearchStep currentStep = sStep;

			while(currentStep != null) {
				path.Add(currentStep.Position);

				currentStep = currentStep.LastStep;
			}
			path.Reverse();
			result.PathMap[sStep.Position] = path;
		}

		return result;
	}
}

class SearchStep {
	private Vector2i position;
	private SearchStep lastStep;
	private int step;

	public SearchStep(Vector2i position, SearchStep lastStep) {
		this.position = position;
		this.lastStep = lastStep;

		if(lastStep == null) {
			step = 0;
		} else {
			step = lastStep.Step + 1;
		}
	}

	public Vector2i Position {
		get {
			return this.position;
		}
	}

	public SearchStep LastStep {
		get {
			return this.lastStep;
		}
	}

	public int Step {
		get {
			return this.step;
		}
	}

	public override bool Equals(object obj) {
		if(obj == null)
			return false;
		if(ReferenceEquals(this, obj))
			return true;
		if(obj.GetType() != typeof(SearchStep))
			return false;
		SearchStep other = (SearchStep)obj;
		return position.Equals(other.position);
	}
	

	public override int GetHashCode() {
		unchecked {
			return position.GetHashCode();
		}
	}
	
}

