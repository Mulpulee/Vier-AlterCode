using Entity;
using Entity.Components;
using GameSystemManager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_10002 : FSMState<Skill> {
		private float m_intersection = 2.0f;
		private float m_damage = 2.0f;

		public Skill_10002() {
			m_damage = SkillDataSystem.GetValue(10002, "Damage");
			m_intersection = SkillDataSystem.GetValue(10002, "Intersection");
		}

		public override void Enter(Skill target) {
			int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
			List<Collider> colliders = Physics.OverlapSphere(target.Caster.Position, m_intersection, enemyLayer).ToList();
			if (target.Caster.TryGetComponent(out Collider targetCollider)) {
				colliders.Remove(targetCollider);
			}

			Collider closeCollider = null;
			float minDistance = 0.0f;
			if (colliders.Count > 0) {
				closeCollider = colliders[0];
				minDistance = Vector3.Distance(target.Caster.Position, colliders[0].transform.position);
			}

			foreach(Collider collider in colliders) {
				float distance = Vector3.Distance(target.Caster.Position, collider.transform.position);
				if (distance < minDistance) {
					Vector3 direction = collider.transform.position - target.Caster.Position;

					Physics.Raycast(target.Caster.Position, direction.normalized, out RaycastHit info, distance);

					if (info.collider != collider) {
						continue;
					}

					closeCollider = collider;
					minDistance = distance;
				}
			}

			if (closeCollider.TryGetComponent(out EntityBehaviour behaviour)) {
				behaviour.OnHit(target.Caster.gameObject, m_damage, Entity.Interface.HitType.Normal);
			}

			target.ChangeState(SkillState.Cooldown);
		}

		public override void Update(Skill target) {
		}
		public override void FixedUpdate(Skill target) {

		}
	}
}