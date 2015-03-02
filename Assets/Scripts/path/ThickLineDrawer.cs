using UnityEngine;

namespace Scripts.path {
	public class ThickLineDrawer : LineDrawer {
		
		private drawPixel pixelDelegate;
		
		public ThickLineDrawer() {}
		
		public ThickLineDrawer(drawPixel pixelDrawer) {
			pixelDelegate += pixelDrawer;
		}
		
		public void drawLine (Vector2i start, Vector2i end) {
			if(pixelDelegate == null) {
				Debug.LogWarning("drawLine was called, but no delegates had been registered!");
				return;
			}
			
			Vector2i deltaVector = end - start;
			Vector2i deltaSign = new Vector2i((int)Mathf.Sign(deltaVector.x), (int)Mathf.Sign(deltaVector.y));
			bool lineX = Mathf.Abs(deltaVector.x) > Mathf.Abs(deltaVector.y);

			float k;
			float d;

			if(lineX) {
				k = (float)deltaVector.y/(float)deltaVector.x;
				d = start.y - k*start.x;
			} else {
				k = (float)deltaVector.x/(float)deltaVector.y;
				d = start.x - k*start.y;
			}
			
			Vector2i currentPos = start;
			
			int maxDelta = Mathf.Abs(lineX ? deltaVector.x : deltaVector.y);
			int minDelta = Mathf.Abs(lineX ? deltaVector.y : deltaVector.x);
			int lastB;

			if(lineX) {
				lastB = currentPos.y;
			} else {
				lastB = currentPos.x;
			}
			
			for(int dA = 0; dA < maxDelta; dA++) {
				bool foundLine = false;

				if(lineX) {
					currentPos.x += deltaSign.x;
					currentPos.y = lastB;
				} else {
					currentPos.y += deltaSign.y;
					currentPos.x = lastB;
				}

				if(lineInsidePixel(k, d, lineX, currentPos)) {
					pixelDelegate(currentPos);
					foundLine = true;

					if(lineX) {
						lastB = currentPos.y;
					} else {
						lastB = currentPos.x;
					}

					if(currentPos.Equals(end)) {
						return;
					}
				}

				for(int dB = 0; dB < minDelta; dB++) {
					if(lineX) {
						currentPos.y += deltaSign.y;
					} else {
						currentPos.x += deltaSign.x;
					}

					if(lineInsidePixel(k, d, lineX, currentPos)) {
						pixelDelegate(currentPos);

						if(!foundLine) {
							if(lineX) {
								lastB = currentPos.y;
							} else {
								lastB = currentPos.x;
							}

							foundLine = true;

							if(currentPos.Equals(end)) {
								return;
							}
						}
					} else if(foundLine) {
						break;
					}
				}
			}
		}

		private bool lineInsidePixel(float k, float d, bool lineX, Vector2i pixel) {
			float x1 = pixel.x - 0.5f;
			float x2 = pixel.x + 0.5f;
			float y1 = pixel.y - 0.5f;
			float y2 = pixel.y + 0.5f;

			if(lineX) {
				if(pointInsidePixel(x1, k*x1 + d, pixel)   || pointInsidePixel(x2, k*x2 + d, pixel) ||
				   pointInsidePixel((y1 - d)/k, y1, pixel) || pointInsidePixel((y2 - d)/k, y2, pixel)) {

					return true;
				}
			} else {
				if(pointInsidePixel(x1, (x1 - d)/k, pixel)   || pointInsidePixel(x2, (x2 - d)/k, pixel) ||
				   pointInsidePixel(k*y1 + d, y1, pixel) || pointInsidePixel(k*y2 + d, y2, pixel)) {

					return true;
				}
			}

			return false;
		}

		private bool pointInsidePixel(float pointX, float pointY, Vector2i pixel) {
			float x1 = pixel.x - 0.5f;
			float x2 = pixel.x + 0.5f;
			float y1 = pixel.y - 0.5f;
			float y2 = pixel.y + 0.5f;

			return pointX >= x1 && pointX <= x2 && pointY >= y1 && pointY <= y2;
		}
		
		public void addCallback (drawPixel pixelDrawer) {
			pixelDelegate += pixelDrawer;
		}
		
		public void removeCallback (drawPixel pixelDrawer) {
			pixelDelegate -= pixelDrawer;
		}
	}
}

