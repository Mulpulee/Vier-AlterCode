using Automation.DataTable;
using System;
using UnityEngine;
using Utility.DataTable;
using Utility.DesignPattern.FSM;
using Utility.Management;

namespace EntitySkill.State {
	public class SkillCooldown : FSMState<Skill> {
		private float m_remainingTime;
		private float m_maxCoolTime;
		private readonly float m_coolTime;
		private bool m_isRepeat = false;

		public float RemainingTime => m_remainingTime;
		public float CoolTime => m_maxCoolTime;

		public SkillCooldown(int id, bool isRepeat) {
			m_coolTime = SkillDataSystem.GetCoolTime(id);
			m_isRepeat = isRepeat;
		}
		public override void Enter(Skill target) {
			m_maxCoolTime = m_coolTime;
			m_remainingTime = m_maxCoolTime;
		}

		public override void Update(Skill target) {
			m_remainingTime -= TimeManager.DeltaTime;

			if (m_remainingTime <= 0.0f) {
				if (m_isRepeat) {
					target.ChangeState(SkillState.InUse);
				} else {
					target.ChangeState(SkillState.Available);
				}
			}
		}
	}
}