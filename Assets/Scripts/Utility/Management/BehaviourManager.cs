using System.Collections.Generic;
using UnityEngine;
using Utility.Behaviour;
using Utility.SceneManagement;

namespace Utility.Management {
	public class BehaviourManager : IndestructibleSingleton<BehaviourManager> {
		public List<IMonoBehaviour> Behaviours => m_behaviours;
		private readonly List<IMonoBehaviour> m_behaviours = new();

		public void Add(IMonoBehaviour behaviour) => m_behaviours.Add(behaviour);
		public void Remove(IMonoBehaviour behaviour) => m_behaviours.Remove(behaviour);

		public void Update() {
			for (int i = 0; i < m_behaviours.Count; i++) {
				m_behaviours[i].Update();
			}
		}
		public void FixedUpdate() {
			for (int i = 0; i < m_behaviours.Count; i++) {
				m_behaviours[i].FixedUpdate();
			}
		}

		protected override void OnInstantiated() {
			SceneLoader.OnSceneLoaded += m_behaviours.Clear;
		}
	}
}