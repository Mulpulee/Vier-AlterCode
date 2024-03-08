using UnityEngine;

namespace Entity.Interface {
	public interface IHittable {
		public void OnHit(GameObject attacker, float damage, HitType hitType);
	}
}