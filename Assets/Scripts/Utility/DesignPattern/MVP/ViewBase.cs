using System;
using UnityEngine;
using Utility.Extension;
using static UnityEditor.Progress;

namespace Utility.DesignPattern.MVP {
	[System.Flags]
	public enum ViewOptions {
		EscapeClose = 1,
		CloseOther = 2,
		StackView = 4,
		AutoHide = 8,
	}

	public abstract class ViewBase : MonoBehaviour {
		private CanvasGroup m_canvasGroup;

		[SerializeField] protected float m_transitionDuration = 0.0f;
		public ViewOptions viewOptions;

		public bool HasStackView => viewOptions.HasFlag(ViewOptions.StackView);
		public bool HasAutoHide => viewOptions.HasFlag(ViewOptions.AutoHide);
		public bool IsActivedView => m_canvasGroup.interactable;


		private void Awake() {
			m_canvasGroup = GetComponent<CanvasGroup>();
			if (HasAutoHide) {
				CloseView(0);
			}
			InitializeView();
		}
		public void OpenView(Action callback = null) => OpenView(m_transitionDuration, callback);
		public void OpenView(float duration, Action callback = null) {
			if (m_canvasGroup == null) {
				m_canvasGroup = GetComponent<CanvasGroup>();
			}

			m_canvasGroup.Show(duration, callback);
		}
		public void CloseView(Action callback = null) => CloseView(m_transitionDuration, callback);
		public void CloseView(float duration, Action callback = null) {
			if (m_canvasGroup == null) {
				m_canvasGroup = GetComponent<CanvasGroup>();
			}

			m_canvasGroup.Hide(duration, callback);
		}

		protected virtual void InitializeView() { }
	}
}