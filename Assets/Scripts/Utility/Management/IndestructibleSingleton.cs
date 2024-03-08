using UnityEngine;
using Utility.Management;

namespace Utility.Management {
	public abstract class IndestructibleSingleton<T> : Manager where T : Manager {
		public static T Instance => GetInstance();
		private static T m_instance;

		protected abstract override void OnInstantiated();

		private static void InstanceInitialize() {
			var instance = new GameObject();
			m_instance = instance.AddComponent<T>();

			string typename = typeof(T).Name;
			instance.name = $"{typename} ( Behaviour Singletion )";

			DontDestroyOnLoad(instance);
			m_instance.Initialize();
		}
		public static T GetInstance() {
			if (m_instance == null) {
				m_instance = FindObjectOfType<T>(true);

				if (m_instance == null) {
					InstanceInitialize();
				}
			}

			return m_instance;
		}
	}
}