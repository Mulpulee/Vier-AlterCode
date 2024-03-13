using UnityEngine;
using Entity.Interface;

namespace Entity.Basic {
	public class EntityHit : IHit {
		public void OnHit(EntityBehaviour entity, GameObject attacker, float damage, HitType hitType) {
			if (!entity.IsAlive) {
				return;
			}

			if (!entity.Status.IsInvincibility) {
				float damageReductionRate = 1.0f - (entity.Status.DamageReductionRate * 0.01f);

				entity.Status.Health -= damage * damageReductionRate;
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