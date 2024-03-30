using Entity.Components;
using UnityEngine;
using Utility;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_21011 : FSMState<Skill> {
		private static Skill_21011_Shield m_shieldObject;

		private float m_shieldDamage = 0.0f;
		private float m_shieldDamageRate = 0.0f;
		private float m_shieldMovePower = 0.0f;
		private float m_shieldKnockbackPower = 0.0f;
		private float m_shieldRemoveMinSpeed = 0.0f;

		public Skill_21011() {
            m_shieldDamage = SkillDataSystem.GetValue(21011, "ShieldDamage");
			m_shieldDamageRate = SkillDataSystem.GetValue(21011, "ShieldDamageRate");
			m_shieldMovePower = SkillDataSystem.GetValue(21011, "ShieldMovePower");
			m_shieldKnockbackPower = SkillDataSystem.GetValue(21011, "ShieldKnockbackPower");
			m_shieldRemoveMinSpeed = SkillDataSystem.GetValue(21011, "ShieldRemoveMinSpeed");
		}

		public override void Enter(Skill target) {
			if (m_shieldObject == null) {
				m_shieldObject = ResourceLoader.SkillObjectLoad<Skill_21011_Shield>("Skill_21011_Shield");

				float damage = (target.Caster.Status.Attack * m_shieldDamageRate) + m_shieldDamage;

                m_shieldObject.SetDamage(damage);
				m_shieldObject.SetMovePower(m_shieldMovePower);
				m_shieldObject.SetKnockbackPower(m_shieldKnockbackPower);
				m_shieldObject.SetRemoveMinSpeed(m_shieldRemoveMinSpeed);
			}

			Skill_21011_Shield shield = GameObject.Instantiate(m_shieldObject, target.Caster.gameObject.transform.position, Quaternion.identity);
			shield.SetDirection(target.Caster.transform.GetFacing2D());
			shield.SetCaster(target.Caster);
			shield.ShieldStart();

			target.ChangeState(SkillState.Cooldown);
		}

		public override void Update(Skill target) {

		}
		public override void FixedUpdate(Skill target) { 
		
		}
	}
}