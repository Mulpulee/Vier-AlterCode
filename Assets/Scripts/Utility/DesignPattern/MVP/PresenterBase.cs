using Utility.Management;

namespace Utility.DesignPattern.MVP {
	public abstract class PresenterBase<TView> : Presenter where TView : ViewBase {
		private TView m_view;
		protected TView View {
			get {
				if (m_view == null) {
					m_view = GetComponent<TView>();
				}

				return m_view;
			}
		}

		public override void InitializePresenter() {
			if (m_view == null) {
				m_view = GetComponent<TView>();
			}
		}

		public override bool HasViewFlag(ViewOptions options) => m_view.viewOptions.HasFlag(options);

		protected void OpenView(float duration = float.MinValue) {
			if (View.HasStackView) {
				UIManager.Instance.AddPresenter(this);
			}

			if (duration == float.MinValue) {
				m_view.OpenView();
			} else {
				m_view.OpenView(duration);
			}
		}
		protected void CloseView(float duration = float.MinValue) {
			if (m_view.HasStackView) {
				UIManager.Instance.RemovePresenter(this);
			}

			if (duration == float.MinValue) {
				m_view.CloseView();
			} else {
				m_view.CloseView(duration);
			}
		}

		public override void Show() => m_view.OpenView(0.0f);
		public override void Hide() => m_view.CloseView(0.0f);
	}
}