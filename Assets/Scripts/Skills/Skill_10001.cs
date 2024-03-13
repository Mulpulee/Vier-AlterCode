using Entity.Components;
using GameSystemManager;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_10001 : FSMState<Skill> {
		private float m_reductionRate = 90.0f;

		public Skill_10001() {
			m_reductionRate = SkillDataSystem.GetValue(10002, "ReductionRate");
		}

		public override void Enter(Skill target) {
			target.Caster.Status.DamageReductionRate += m_reductionRate;
			target.Caster.Status.IsBind = true;
		}

		public override void Update(Skill target) {
			if (GameInputManager.GetKeyUp(target.Slot)) {
				target.Caster.Status.DamageReductionRate -= m_reductionRate;
				target.Caster.Status.IsBind = false;

				target.ChangeState(SkillState.Cooldown);
			}
		}
		public override void FixedUpdate(Skill target) {

		}
	}
}