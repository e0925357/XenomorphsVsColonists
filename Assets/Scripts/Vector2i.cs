using System;

public struct Vector2i {
	public int x;
	public int y;

	public Vector2i(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static Vector2i operator+ (Vector2i left, Vector2i right) {
		Vector2i result = new Vector2i();
		result.x = left.x + right.x;
		result.y = left.y + right.y;

		return result;
	}

	public static Vector2i operator- (Vector2i left, Vector2i right) {
		Vector2i result = new Vector2i();
		result.x = left.x - right.x;
		result.y = left.y - right.y;
		
		return result;
	}

	public static Vector2i operator* (Vector2i left, Vector2i right) {
		Vector2i result = new Vector2i();
		result.x = left.x * right.x;
		result.y = left.y * right.y;
		
		return result;
	}

	public static Vector2i operator* (Vector2i left, int right) {
		Vector2i result = new Vector2i();
		result.x = left.x * right;
		result.y = left.y * right;
		
		return result;
	}

	public static Vector2i operator* (int left, Vector2i right) {
		Vector2i result = new Vector2i();
		result.x = left * right.x;
		result.y = left * right.y;
		
		return result;
	}

	public static Vector2i operator/ (Vector2i left, Vector2i right) {
		Vector2i result = new Vector2i();
		result.x = left.x / right.x;
		result.y = left.y / right.y;
		
		return result;
	}
	
	public static Vector2i operator/ (Vector2i left, int right) {
		Vector2i result = new Vector2i();
		result.x = left.x / right;
		result.y = left.y / right;
		
		return result;
	}
	
	public static Vector2i operator/ (int left, Vector2i right) {
		Vector2i result = new Vector2i();
		result.x = left / right.x;
		result.y = left / right.y;
		
		return result;
	}

	public override string ToString() {
		return string.Format("[Vector2i: x={0}, y={1}]", x, y);
	}

	public override bool Equals(object obj) {
		if(obj == null)
			return false;
		if(ReferenceEquals(this, obj))
			return true;
		if(obj.GetType() != typeof(Vector2i))
			return false;
		Vector2i other = (Vector2i)obj;
		return x == other.x && y == other.y;
	}
	

	public override int GetHashCode() {
		int hash = 3;
		hash = 97 * hash + this.x;
		hash = 97 * hash + this.y;
		return hash;
	}
	
}

