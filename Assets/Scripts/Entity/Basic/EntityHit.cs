using UnityEngine;
using Entity.Interface;

namespace Entity.Basic {
	public class EntityHit : IHit {
		public void OnHit(EntityBehaviour entity, GameObject attacker, float damage, HitType hitType) {
			if (!entity.IsAlive) {
				return;
			}

			if (!entity.Status.IsInvincibility) {
				entity.Status.Health -= damage;
			} else {
				/* Invincibility.. */
			}

			if (!entity.IsAlive) {
				Debug.Log($"{entity.Name} is Dead..");

				entity.Status.Health = 0;
				entity.OnDeath();
				OnDeath();
			}
		}

		public void Destroy() { }
		protected virtual void OnDeath() { }
	}
}