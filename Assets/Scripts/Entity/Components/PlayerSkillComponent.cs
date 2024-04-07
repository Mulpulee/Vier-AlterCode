using EntitySkill;
using System;
using System.Collections.Generic;
using EntitySkill.State;

namespace Entity.Components {
	public enum SkillSlot {
		Slot1,
		Slot2,
		Slot3,
		Slot4,
		Slot5,
		Count
	}

	public class PlayerSkillComponent : EntityComponent {
		private Dictionary<SkillSlot, Skill> m_skillBySlot;
		public event Action<SkillSlot, Skill> OnSkillUpdated;
		
		private void Awake() {
			m_skillBySlot = new Dictionary<SkillSlot, Skill>();	
		}

		public void SetSkill(SkillSlot slot, int id) {
			if (m_skillBySlot.TryGetValue(slot, out Skill skill)) {
				skill?.Delete();
			}

			if (!SkillDataSystem.IsContainsID(id)) {
				m_skillBySlot[slot] = null;
				OnSkillUpdated?.Invoke(slot, null);

				return;
			}
			var newSkill = new Skill(id);
			newSkill.InformationInitialize(Entity, slot);
			newSkill.Initialize();

			m_skillBySlot[slot] = newSkill;

			OnSkillUpdated?.Invoke(slot, newSkill);
			newSkill.ChangeState(SkillState.CustomCooldown);
		} 

		public bool IsSkillChangable(SkillSlot slot) {
			if (m_skillBySlot.TryGetValue(slot, out Skill skill)) {
				if (skill.NowState == SkillState.Available) {
					return true;
				}
			}

			return false;
		}
		public SkillState GetSkillState(SkillSlot slot) {
			if (m_skillBySlot.TryGetValue(slot, out Skill skill)) {
				return skill.NowState;
			}

			return SkillState.UnAvailable;
		}

		public bool TryCast(SkillSlot slot) {
			if (m_skillBySlot.TryGetValue(slot, out Skill skill)) {
				if (skill != null) {
					return skill.TryCast(Entity, slot);
				} else {
					return false;
				}
			}

			return false;
		}
	}
}