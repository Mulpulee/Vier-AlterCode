using UnityEngine;

namespace Utility.DesignPattern {
	public class SingletionBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
		private static T m_instance;
		public static T Instance {
			get {
				if (m_instance == null) {
					m_instance = GameObject.FindObjectOfType<T>(true);

					if (m_instance == null) {
						Debug.LogError("Singlton is empty");
						return null;
					}
				}

				return m_instance;
			}
		}

		public static bool HasInstance => m_instance != null;
	}
}