using UnityEngine;
using UnityEngine.UI;
using Utility.Management;

namespace Utility.Extension {
	public static class GraphicExtension {
		public static void Show(this Graphic graphic, float duration = 0.0f) {
			graphic.Fade(duration, 1.0f);
		}
		public static void Hide(this Graphic graphic, float duration = 0.0f) {
			graphic.Fade(duration, 0.0f);
		}

		public static void SetAlpha(this Graphic graphic, float alpha) {
			Color color = graphic.color;
			color.a = alpha;
			graphic.color = color;
		}
		public static void AddAlpha(this Graphic graphic, float alpha) {
			Color color = graphic.color;
			color.a += alpha;
			graphic.color = color;
		}
	}
}