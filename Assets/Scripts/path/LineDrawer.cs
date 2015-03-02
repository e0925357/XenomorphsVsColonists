

namespace Scripts.path {

	/// <summary>
	/// This delegate will be called if a pixel should be drawn
	/// </summary>
	public delegate void drawPixel(Vector2i position);

	/// <summary>
	/// A Line drawer encapsules a line-drawing algorithm. It will not draw directly to the screen, but will execute callbacks for every pixel instead.
	/// </summary>
	public interface LineDrawer {

		/// <summary>
		/// Draws a line from the start to the end. Has no effect if no callbacks were registered.
		/// </summary>
		/// <param name="start">The position to start from</param>
		/// <param name="end">The position to end at</param>
		void drawLine(Vector2i start, Vector2i end);

		/// <summary>
		/// Adds the callback to the LineDrawer. It will be called inside of drawLine(Vector2i, Vector2i) when a pixel should be drawn.
		/// </summary>
		/// <param name="pixelDrawer">The callback to register, never null</param>
		void addCallback(drawPixel pixelDrawer);

		/// <summary>
		/// Removes the callback from the LineDrawer. The callback would be called inside of drawLine(Vector2i, Vector2i) if a pixel should be drawn.
		/// </summary>
		/// <param name="pixelDrawer">The callback to remove, never null</param>
		void removeCallback(drawPixel pixelDrawer);
	}
}

