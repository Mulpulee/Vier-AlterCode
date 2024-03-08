using UnityEngine;

namespace Entity {
	public class EntityComponent : MonoBehaviour {
		[SerializeField] private EntityBehaviour m_behaviour;
		protected EntityBehaviour Entity => m_behaviour;

		public void Initialize(EntityBehaviour entity) {
			m_behaviour = entity;
		}
	}
}