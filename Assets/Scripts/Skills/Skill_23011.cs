using Entity.Components;
using UnityEngine;
using Utility;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_23011 : FSMState<Skill> {
		private static Skill_23011_Arrow m_arrowObject;

		private float m_arrowDamage = 15.0f;
		private float m_arrowDamageRate = 15.0f;
		private float m_arrowMoveSpeed = 10.0f;
		private float m_arrowKnockbackPower = 100.0f;
		private float m_arrowMaxDistance = 100.0f;

		public Skill_23011() {
			m_arrowDamage = SkillDataSystem.GetValue(23011, "ArrowDamage");
            m_arrowDamageRate = SkillDataSystem.GetValue(23011, "ArrowDamageRate");
			m_arrowMoveSpeed = SkillDataSystem.GetValue(23011, "ArrowMoveSpeed");
			m_arrowKnockbackPower = SkillDataSystem.GetValue(23011, "ArrowKnockbackPower");
			m_arrowMaxDistance = SkillDataSystem.GetValue(23011, "ArrowMaxDistance");
		}

		public override void Enter(Skill target) {
			if (m_arrowObject == null) {
				m_arrowObject = ResourceLoader.SkillObjectLoad<Skill_23011_Arrow>("Skill_23011_Arrow");
				float damage = (target.Caster.Status.Attack * m_arrowDamageRate) + m_arrowDamage;

				m_arrowObject.SetDamage(damage);
				m_arrowObject.SetMoveSpeed(m_arrowMoveSpeed);
				m_arrowObject.SetKnockbackPower(m_arrowKnockbackPower);
				m_arrowObject.SetMaxDistance(m_arrowMaxDistance);
			}

			Skill_23011_Arrow arrow = GameObject.Instantiate(m_arrowObject, target.Caster.gameObject.transform.position, Quaternion.identity);
			arrow.SetDirection(target.Caster.transform.GetFacing2D());
			arrow.SetCaster(target.Caster);

			target.ChangeState(SkillState.Cooldown);
		}

		public override void Update(Skill target) {

		}
		public override void FixedUpdate(Skill target) { 
		
		}
	}
}