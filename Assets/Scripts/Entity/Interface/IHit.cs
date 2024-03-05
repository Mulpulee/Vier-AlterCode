using UnityEngine;

namespace Entity.Interface {
	public enum HitType {
		Normal,
		Skill,
		Reflect
	}

	public interface IHit {
		public void OnHit(EntityBehaviour entity, GameObject attacker, float damage, HitType hitType);
		public void Destroy();
	}
}