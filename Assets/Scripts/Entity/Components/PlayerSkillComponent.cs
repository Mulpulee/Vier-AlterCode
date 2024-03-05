using EntitySkill;
using System;
using System.Collections.Generic;

namespace Entity.Components {
	public enum SkillSlot {
		Slot1,
		Slot2,
		Slot3,
		Slot4,
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
				skill.Delete();
			}

			var newSkill = new Skill(id);
			newSkill.InformationInitialize(Entity, slot);
			newSkill.Initialize();

			m_skillBySlot[slot] = newSkill;

			OnSkillUpdated?.Invoke(slot, newSkill);
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
				return skill.TryCast(Entity, slot);
			}

			return false;
		}
	}
}