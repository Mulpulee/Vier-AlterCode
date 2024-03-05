using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Utility.Management;

namespace Utility.Extension {
	public static class TransitionExtension {
		public static int Kill<T>(this T target, bool isCompelete = false) where T : Component => Kill<T>(target, TransitionManager.FindTransitions(target.gameObject), isCompelete);
		public static int Kill<T>(this T target, List<Transition> transitions, bool isCompelete = false) where T : Component {
			List<Transition> existTransitions = TransitionManager.FindTransitions(transitions);

			int count = existTransitions.Count;

			foreach (Transition transition in existTransitions) {
				transition.Kill(isCompelete);
				TransitionManager.RemoveTransition(transition);
			}

			return count;
		}
		public static int Kill<T>(this T target, Transition[] transitions, bool isCompelete = false) where T : Component => Kill<T>(target, TransitionManager.FindTransitions(transitions.ToList()), isCompelete);
		public static int Kill<T>(this T target, Transition transition, bool isCompelete = false) where T : Component => Kill<T>(target, TransitionManager.FindTransitions(new List<Transition> { transition }), isCompelete);

		#region Graphics
		public static Transition Fade(this Graphic graphic, float duration, float end) {
			var transition = new Transition(graphic.gameObject);

			TransitionManager.StartFade(graphic, duration, end, transition);

			return transition;
		}
		#endregion

		#region CanvasGroup
		public static Transition Fade(this CanvasGroup group, float duration, float end) {
			var transition = new Transition(group.gameObject);

			TransitionManager.StartFade(group, duration, end, transition);

			return transition;
		}
		#endregion

		#region Transform
		public static Transition Fade(this Transform transform, float duration, Vector3 end) {
			var transition = new Transition(transform.gameObject);

			TransitionManager.StartScale(transform, duration, end, transition);

			return transition;
		}
		public static Transition Move(this Transform transform, float duration, Vector3 end) {
			var transition = new Transition(transform.gameObject);

			TransitionManager.StartMove(transform, duration, end, transition);

			return transition;
		}
		#endregion
	}
}