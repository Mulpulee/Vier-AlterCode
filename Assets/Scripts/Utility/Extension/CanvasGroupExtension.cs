using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.Extension {
    public static class CanvasGroupExtension {
		public static void Show(this CanvasGroup group) {
			group.alpha = 1.0f;
			group.interactable = true;
			group.blocksRaycasts = true;
		}
		public static void Hide(this CanvasGroup group) {
			group.alpha = 0.0f;
			group.interactable = false;
			group.blocksRaycasts = false;
		}

		public static void Show(this CanvasGroup group, float duration, Action transitionCallback = null) {
			group.Kill();
			if (duration == 0.0f) {
				Show(group);
				return;
			}

			group.interactable = true;
			group.blocksRaycasts = true;

			group.Fade(duration, 1.0f).OnCompelete(transitionCallback);
		}
		public static void Hide(this CanvasGroup group, float duration, Action transitionCallback = null) {
			group.Kill();
			if (duration == 0.0f) {
				Hide(group);
				return;
			}

			group.interactable = false;
			group.blocksRaycasts = false;

			group.Fade(duration, 0.0f).OnCompelete(transitionCallback);
		}
	}
}