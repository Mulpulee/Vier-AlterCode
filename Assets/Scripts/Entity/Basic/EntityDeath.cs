using UnityEngine;
using Entity.Interface;

namespace Entity.Basic {
	public class EntityDeath : IDeath {
		public void OnDeath(EntityBehaviour entity) {
			GameObject.Destroy(entity.gameObject);
		}

		public void Destroy() { }
	}
}