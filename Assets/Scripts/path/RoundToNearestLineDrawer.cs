using UnityEngine;

namespace Scripts.path {
	public class RoundToNearestLineDrawer : LineDrawer {

		private drawPixel pixelDelegate;

		public RoundToNearestLineDrawer() {}

		public RoundToNearestLineDrawer(drawPixel pixelDrawer) {
			pixelDelegate += pixelDrawer;
		}

		public void drawLine (Vector2i start, Vector2i end) {
			if(pixelDelegate == null) {
				Debug.LogWarning("drawLine was called, but no delegates had been registered!");
				return;
			}

			Vector2i deltaVector = end - start;
			Vector2i deltaSign = new Vector2i((int)Mathf.Sign(deltaVector.x), (int)Mathf.Sign(deltaVector.y));
			float m = Mathf.Abs((float)deltaVector.y/(float)deltaVector.x);
			
			Vector2 currentPos = new Vector2(start.x, start.y);
			bool horizontal = m < 1;
			
			int maxDelta = Mathf.Abs(horizontal ? deltaVector.x : deltaVector.y);
			
			for(int k = 0; k < maxDelta; k++) {
				if(horizontal) {
					currentPos.x += deltaSign.x;
					currentPos.y += m*deltaSign.y;
				} else {
					currentPos.x += deltaSign.x/m;
					currentPos.y += deltaSign.y;
				}
				
				int x = (int)Mathf.Round(currentPos.x);
				int y = (int)Mathf.Round(currentPos.y);
				
				pixelDelegate(new Vector2i(x, y));
			}
		}

		public void addCallback (drawPixel pixelDrawer) {
			pixelDelegate += pixelDrawer;
		}

		public void removeCallback (drawPixel pixelDrawer) {
			pixelDelegate -= pixelDrawer;
		}
	}
}

