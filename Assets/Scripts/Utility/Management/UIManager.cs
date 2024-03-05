using System;
using UnityEngine;
using Utility.DataStructure;
using Utility.DesignPattern.MVP;
using Utility.SceneManagement;

namespace Utility.Management {
	public class UIManager : IndestructibleSingleton<UIManager> {
		[SerializeField] private ListStack<Presenter> m_presenters;
		public event Action OnEmptyStack;

		protected override void OnInstantiated() {
			m_presenters = new ListStack<Presenter>();
			SceneLoader.OnSceneLoaded += Clear;
		}

		public void Clear() {
			m_presenters.Clear();
			OnEmptyStack = null;
		}

		public void AddPresenter(Presenter presenter) {
			if (presenter.HasViewFlag(ViewOptions.CloseOther | ViewOptions.StackView)) {
				if (m_presenters.Count > 0) {
					PeekShow();
				}
			}

			m_presenters.Add(presenter);
		}
		public void RemovePresenter(Presenter presenter) {
			if (presenter == null) {
				return;
			}

			bool isTopFlag = false;
			if (presenter.HasViewFlag(ViewOptions.CloseOther | ViewOptions.StackView)) {
				if (m_presenters.Peek() == presenter) {
					isTopFlag = true;
				}
			}

			m_presenters.Remove(presenter);

			if (isTopFlag) {
				if (m_presenters.Count > 0) {
					PeekHide();
				}
			}
		}

		private void PopPresenter() {
			if (m_presenters.Count <= 0) {
				return;
			}

			Presenter presenter = m_presenters.Peek();

			if (!presenter.HasViewFlag(ViewOptions.EscapeClose)) {
				return;
			}

			var target = presenter;
			target.Release();
		}

		private void PeekShow() => m_presenters.Peek().Show();
		private void PeekHide() => m_presenters.Peek().Hide();

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				if (m_presenters.Count <= 0) {
					OnEmptyStack?.Invoke();
				} else {
					PopPresenter();
				}
			}
		}
	}
}