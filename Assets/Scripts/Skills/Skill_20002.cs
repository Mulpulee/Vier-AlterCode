using Entity.Components;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_20002 : FSMState<Skill> {
		private float m_regenDelay = 30.0f;
		private float m_regenHealth = 1.0f;

		private float m_beforeHealth = 0.0f;
		private float m_curTime = 0.0f;
		private bool m_isHited = false;

		public Skill_20002() {
			m_regenDelay =  SkillDataSystem.GetValue(20002, "RegenDelay");
			m_regenHealth =  SkillDataSystem.GetValue(20002, "RegenHealth");
		}

		public override void Enter(Skill target) {
			m_beforeHealth = target.Caster.Status.Health;
			m_curTime = m_regenDelay;
			m_isHited = false;
		}

		public override void Update(Skill target) {
			if (!m_isHited) {
				if (target.Caster.Status.Health > m_beforeHealth) {
					m_beforeHealth = target.Caster.Status.Health;
				}

				if (m_beforeHealth > target.Caster.Status.Health) {
					m_isHited = true;
				}
			} else {
				if (m_curTime <= 0.0f) {
					if (target.Caster.IsAlive) {
						target.Caster.Status.Health += m_regenHealth;

						target.ChangeState(SkillState.Cooldown);
					}
				}

				m_curTime -= TimeManager.DeltaTime;
			}
		}
		public override void FixedUpdate(Skill target) { 
		
		}
	}
}