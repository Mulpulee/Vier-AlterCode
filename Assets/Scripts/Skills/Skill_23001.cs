using Entity;
using Entity.Basic;
using Entity.Components;
using Entity.Interface;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_23001 : FSMState<Skill> {
		private float m_additionalDamage = 10.0f;
		private float m_additionalDamageRate = 10.0f;
		private float m_stackCount = 3;

		private int m_count = 0;

		public Skill_23001() {
			m_additionalDamage = SkillDataSystem.GetValue(23001, "AdditionalDamage");
            m_additionalDamageRate = SkillDataSystem.GetValue(23001, "AdditionalDamageRate");
			m_stackCount = SkillDataSystem.GetValue(23001, "StackCount");
		}

		public override void Enter(Skill target) {
			m_count = target.Caster.NormalHitData.lastAttack.count;
		}

		public override void Update(Skill target) {
			EntityHitData.HitInfo normalData = target.Caster.NormalHitData.lastAttack;
			m_count = normalData.count % (int)m_stackCount;

			if (m_count == (m_stackCount - 1)) {
				EntityBehaviour last = normalData.entity;

				if (last != null) {
					float damage = (target.Caster.Status.Attack * m_additionalDamageRate) + m_additionalDamage;
					last.OnHit(null, damage, HitType.Skill);
				}
			}
		}
		public override void FixedUpdate(Skill target) { 
			
		}
	}
}