using Entity.Components;
using UnityEngine;
using Utility.DesignPattern.FSM;
using Utility.Extension;
using Utility.Management;

namespace EntitySkill.Skills {
	public class Skill_20001 : FSMState<Skill> {
		private float m_movePower = 50f;
		private float m_invincibilityTime = 0.5f;

		private float m_curTime = 0.0f;

		public Skill_20001() {
			m_movePower = SkillDataSystem.GetValue(20001, "MovePower");
			m_invincibilityTime = SkillDataSystem.GetValue(20001, "InvincibilityTime");
		}

		public override void Enter(Skill target) {
			if (target.Caster.TryGetComponent(out PlayerMoveComponent entityMove)) {
				Facing2D facing = target.Caster.transform.GetFacing2D();
				entityMove.RigidBody.AddForce((int)facing * m_movePower * Vector3.right, ForceMode.VelocityChange);
			}
			target.Caster.Status.IsInvincibility = true;

			m_curTime = m_invincibilityTime;
		}

		public override void Update(Skill target) {
			if (m_curTime <= 0.0f) {
				target.Caster.Status.IsInvincibility = false;

				target.ChangeState(SkillState.Cooldown);
			}

			m_curTime -= TimeManager.DeltaTime;
		}
		public override void FixedUpdate(Skill target) { 
		
		}
	}
}