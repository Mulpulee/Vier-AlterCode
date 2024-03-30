using Entity;
using Entity.Basic;
using Entity.Components;
using Entity.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_22001 : FSMState<Skill> {
		private float m_additionalDamage = 10.0f;
		private float m_additionalDamageRate = 10.0f;
		private EntityBehaviour m_lastAttackEntity;

		public Skill_22001() {
			m_additionalDamage = SkillDataSystem.GetValue(22001, "AdditionalDamage");
            m_additionalDamageRate = SkillDataSystem.GetValue(22001, "AdditionalDamageRate");
		}

		public override void Enter(Skill target) {
			m_lastAttackEntity = target.Caster.NormalHitData.lastAttack.entity;
		}

		public override void Update(Skill target) {
			EntityBehaviour entity = target.Caster.NormalHitData.lastAttack.entity;
			if (entity != m_lastAttackEntity) {
				float damage = (target.Caster.Status.Attack * m_additionalDamageRate) + m_additionalDamage;
				entity.OnHit(target.Caster.gameObject, m_additionalDamage, HitType.Skill);

				m_lastAttackEntity = entity;
			}
		}
		public override void FixedUpdate(Skill target) { 
			
		}
	}
}