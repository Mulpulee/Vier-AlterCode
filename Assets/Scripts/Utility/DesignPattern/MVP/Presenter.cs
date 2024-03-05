using System;
using UnityEngine;

namespace Utility.DesignPattern.MVP {
	public abstract class Presenter : MonoBehaviour {
		private void Awake() => InitializePresenter();

		public abstract void InitializePresenter();

		public abstract Boolean HasViewFlag(ViewOptions options);

		public abstract void Bind();
		public abstract void Release();

		public abstract void Show();
		public abstract void Hide();
	}
}