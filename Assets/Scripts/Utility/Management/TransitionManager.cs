using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utility.Extension;

namespace Utility.Management {
	public class Transition {
		private GameObject m_base;
		private bool m_isPaused = false;
		private bool m_isKilled = false;

		public event Action OnStarted;
		public event Action OnCompeleted;
		public event Action OnPlayed;
		public event Action OnPaused;
		public event Action OnKilled;

		public Transition(GameObject based) {
			m_base = based;
		}

		public GameObject Base => m_base;
		public bool IsPaused => m_isPaused;
		public bool IsPlaying => !m_isPaused;
		public bool IsKilled => m_isKilled;

		public void OnStart() => OnStarted?.Invoke();
		public void OnCompelete() => OnCompeleted?.Invoke();
		public void OnPlay() => OnPlayed?.Invoke();
		public void OnPause() => OnPaused?.Invoke();
		public void OnKill() => OnKilled?.Invoke();

		public void Play() {
			OnPlayed?.Invoke();
			m_isPaused = false;
		}
		public void Pause() {
			OnPaused?.Invoke();
			m_isPaused = true;
		}
		public void Kill(bool isCompeleted = false) {
			OnKilled?.Invoke();

			if (isCompeleted) {
				OnCompeleted?.Invoke();
			}

			m_isKilled = true;
		}

		public Transition OnStart(Action callback) {
			OnStarted += callback;

			return this;
		}
		public Transition OnCompelete(Action callback) {
			OnCompeleted += callback;

			return this;
		}
		public Transition OnPlay(Action callback) {
			OnPlayed += callback;

			return this;
		}
		public Transition OnPause(Action callback) {
			OnPaused += callback;

			return this;
		}
		public Transition OnKill(Action callback) {
			OnKilled += callback;

			return this;
		}
	}

	public class TransitionManager : IndestructibleSingleton<TransitionManager> {
		private List<Transition> m_transitions;
		private readonly WaitForFixedUpdate m_wait = new();

		protected override void OnInstantiated() {
			m_transitions = new List<Transition>();
		}

		public static void AddTransition(Transition transition) => Instance.m_transitions.Add(transition);
		public static void RemoveTransition(Transition transition) => Instance.m_transitions.Remove(transition);

		public static List<Transition> FindTransitions(GameObject target) {
			List<Transition> transitions = Instance.m_transitions;

			List<Transition> result = transitions.FindAll((x) => x.Base == target);

			return result;
		}
		public static List<Transition> FindTransitions(List<Transition> targets) {
			List<Transition> transitions = Instance.m_transitions;

			List<Transition> result = transitions.FindAll((x) => {
				foreach (Transition transition in targets) {
					if (transition == x) {
						return true;
					}
				}
				return false;
			});

			return result;
		}

		public void Initialize(Transition transition) {
			transition?.OnStart();
			TransitionManager.AddTransition(transition);
		}
		public void Dispose(Transition transition) {
			transition?.OnCompelete();
			TransitionManager.RemoveTransition(transition);
		}
		public static float GetMultipleTime(float duration) => 1.0f / (duration - Time.deltaTime);

		public IEnumerator ScaleTransition(Transform transform, float duration, Vector3 end, Transition transition) {
			float multipleTime = GetMultipleTime(duration);
			Vector3 distance = end - transform.localScale;
			yield return null;

			Initialize(transition);

			float time = duration;
			while (time > 0.0f) {
				while (transition.IsPaused) {
					yield return null;
				}
				if (transition.IsKilled) {
					yield break;
				}

				transform.localScale += distance * (multipleTime * Time.fixedDeltaTime);

				time -= Time.fixedDeltaTime;

				yield return m_wait;
			}

			transform.localScale += distance * time;

			Dispose(transition);
		}
		public IEnumerator FadeTransition(Graphic graphic, float duration, float end, Transition transition) {
			float multipleTime = GetMultipleTime(duration);
			float distance = end - graphic.color.a;
			yield return null;

			Initialize(transition);

			float time = duration;
			while (time > 0.0f) {
				while (transition.IsPaused) {
					yield return null;
				}
				if (transition.IsKilled) {
					yield break;
				}

				graphic.AddAlpha(distance * (multipleTime * Time.fixedDeltaTime));

				time -= Time.fixedDeltaTime;

				yield return m_wait;
			}

			if (distance > 0.0f) {
				if (end < 1.0f) {
					graphic.AddAlpha(distance * time);
				}
			} else if (distance < 0.0f) {
				if (end > 0.0f) {
					graphic.AddAlpha(distance * time);
				}
			}

			Dispose(transition);
		}
		public IEnumerator FadeTransition(CanvasGroup group, float duration, float end, Transition transition) {
			float multipleTime = GetMultipleTime(duration);
			float distance = end - group.alpha;
			yield return null;

			Initialize(transition);

			float time = duration;
			while (time > 0.0f) {
				while (transition.IsPaused) {
					yield return null;
				}
				if (transition.IsKilled) {
					yield break;
				}

				group.alpha += distance * (multipleTime * Time.fixedDeltaTime);

				time -= Time.fixedDeltaTime;

				yield return m_wait;
			}

			if (distance > 0.0f) {
				if (end < 1.0f) {
					group.alpha += distance * time;
				}
			} else if (distance < 0.0f) {
				if (end > 0.0f) {
					group.alpha += distance * time;
				}
			}

			Dispose(transition);
		}
		public IEnumerator MoveTransition(Transform transform, float duration, Vector3 end, Transition transition) {
			float multipleTime = GetMultipleTime(duration);
			Vector3 distance = end - transform.position;
			yield return null;

			Initialize(transition);

			float time = duration;
			while (time > 0.0f) {
				while (transition.IsPaused) {
					yield return null;
				}
				if (transition.IsKilled) {
					yield break;
				}

				transform.position += distance * (multipleTime * Time.fixedDeltaTime);

				time -= Time.fixedDeltaTime;

				yield return m_wait;
			}

			transform.position += distance * time;

			Dispose(transition);
		}

		public static void StartFade(Graphic target, float duration, float end, Transition transition) {
			Instance.StartCoroutine(Instance.FadeTransition(target, duration, end, transition));
		}
		public static void StartFade(CanvasGroup target, float duration, float end, Transition transition) {
			Instance.StartCoroutine(Instance.FadeTransition(target, duration, end, transition));
		}
		public static void StartScale(Transform target, float duration, Vector3 end, Transition transition) {
			Instance.StartCoroutine(Instance.ScaleTransition(target, duration, end, transition));
		}
		public static void StartMove(Transform target, float duration, Vector3 end, Transition transition) {
			Instance.StartCoroutine(Instance.MoveTransition(target, duration, end, transition));
		}
	}
}