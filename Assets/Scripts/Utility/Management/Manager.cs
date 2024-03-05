using UnityEngine;

namespace Utility.Management {
	public abstract class Manager : MonoBehaviour {
		[SerializeField] private bool m_isInitialized = false;

		protected abstract void OnInstantiated();

		public void Initialize() {
			if (m_isInitialized) {
				return;
			}

			OnInstantiated();
			m_isInitialized = true;
		}
	}
}