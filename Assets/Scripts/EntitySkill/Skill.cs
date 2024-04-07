using Entity;
using Entity.Components;
using EntitySkill.State;
using UnityEngine;
using Utility.DesignPattern.FSM;

namespace EntitySkill {
	public enum SkillState {
		Available,
		UnAvailable,
		InUse,
		Cooldown,
		CustomCooldown
	}

	public class Skill : FSMBase<SkillState, Skill> {
		private readonly int m_id;
		private SkillType m_type;
		private EntityBehaviour m_caster;
		private SkillCooldown m_cooldown;
		private SkillCooldown m_customCooldown;
		private SkillSlot m_slot;

		public int ID => m_id;
		public EntityBehaviour Caster => m_caster;
		public SkillCooldown Cooldown => m_cooldown;
		public SkillCooldown CustomCooldown {
			get => m_customCooldown;
			set => m_customCooldown = value;
		}
		public SkillSlot Slot => m_slot;
		public SkillType Type => m_type;

		public Skill(int id) {
			m_id = id;
		}

		protected override void StateMachineInitialize() {
			m_type = SkillDataSystem.GetSkillType(m_id);

			m_cooldown = new SkillCooldown(m_id, m_type == SkillType.Passive);
			m_customCooldown = new SkillCooldown(0.1f);

			m_stateByEnum.Add(SkillState.Available, new SkillAvailable());
			m_stateByEnum.Add(SkillState.UnAvailable, new SkillUnAvailable());
			m_stateByEnum.Add(SkillState.InUse, SkillBinder.GetState(m_id));
			m_stateByEnum.Add(SkillState.Cooldown, m_cooldown);
			m_stateByEnum.Add(SkillState.CustomCooldown, m_customCooldown);

			SkillState startState = m_type switch {
				SkillType.Active => SkillState.Available,
				SkillType.Passive => SkillState.InUse,
				_ => SkillState.UnAvailable
			};
			m_stateMachine.Initialize(this, m_stateByEnum[startState]);
		}
		public void InformationInitialize(EntityBehaviour caster, SkillSlot slot) {
			m_caster = caster;
			m_slot = slot;
		}
		public bool TryCast(EntityBehaviour caster, SkillSlot slot) {
			if (NowState != SkillState.Available) {
				return false;
			}

			m_slot = slot;
			m_caster = caster;

			ChangeState(SkillState.InUse);
			return true;
		}
	}
}