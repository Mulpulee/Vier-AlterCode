using Entity;
using Entity.Components;
using Entity.Interface;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_21001 : FSMState<Skill> {
		private float m_reflectCoefficient = 0.2f;

		private EntityBehaviour m_normalHitter;
		private EntityBehaviour m_skillHitter;
		private int m_normalHitCount = 0;
		private int m_skillHitCount = 0;

		public Skill_21001() {
			m_reflectCoefficient = SkillDataSystem.GetValue(21001, "ReflectCoefficient");
		}

		public override void Enter(Skill target) {
			m_normalHitCount = target.Caster.NormalHitData.lastHit.count;
			m_skillHitCount = target.Caster.SkillHitData.lastHit.count;
		}
		private void Check(Skill target, HitType hitType, ref EntityBehaviour entity, ref int count) {
			EntityHitData hitData = target.Caster.GetHitData(hitType);
			EntityHitData.HitInfo hitInfo = hitData.lastHit;

			if (hitInfo.entity != entity || hitInfo.count != count) {
				hitInfo.entity.OnHit(target.Caster.gameObject, hitInfo.damage * m_reflectCoefficient, HitType.Reflect);

				count = hitInfo.count;
				entity = hitInfo.entity;
			}
		}

		public override void Update(Skill target) {
			Check(target, HitType.Normal, ref m_normalHitter, ref m_normalHitCount);
			Check(target, HitType.Skill, ref m_skillHitter, ref m_skillHitCount);
		}
		public override void FixedUpdate(Skill target) { 
		
		}
	}
}