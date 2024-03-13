using Entity.Player;
using EntitySkill;
using GameSystemManager;

namespace Entity.Components {
	public class PlayerSkillChangeComponent : EntityComponent {
		private bool m_isChanged = false;
		private int m_skillNumber = 0;
		private PlayerSkillComponent m_skill;
		private PlayerAnimationController m_anim;

		private void Start() {
			m_skill = Entity.GetComponent<PlayerSkillComponent>();
			m_anim = Entity.GetComponent<PlayerAnimationController>();
		}

		public void Update() {
			if (GameInputManager.GetKeyDown(InputType.FormSlot1)) {
				m_skillNumber = 0;
				m_isChanged = true;
				m_anim.ChangeForm(m_skillNumber);
			}
			if (GameInputManager.GetKeyDown(InputType.FormSlot2)) {
				m_skillNumber = 1;
				m_isChanged = true;
				m_anim.ChangeForm(m_skillNumber);
			}
			if (GameInputManager.GetKeyDown(InputType.FormSlot3)) {
				m_skillNumber = 2;
				m_isChanged = true;
                m_anim.ChangeForm(m_skillNumber);
            }
			if (GameInputManager.GetKeyDown(InputType.FormSlot4)) {
				m_skillNumber = 3;
				m_isChanged = true;
                m_anim.ChangeForm(m_skillNumber);
            }
			if (GameInputManager.GetKeyDown(InputType.FormSlot5)) {
				m_skillNumber = 4;
				m_isChanged = true;
                m_anim.ChangeForm(m_skillNumber);
            }

			if (m_isChanged) {
				if (m_skillNumber != 0) {
					int skillId = 20000 + (m_skillNumber * 1000);
					m_skill.SetSkill(SkillSlot.Slot1, 20001);
					m_skill.SetSkill(SkillSlot.Slot2, skillId + 11);
					m_skill.SetSkill(SkillSlot.Slot3, 20002);
					m_skill.SetSkill(SkillSlot.Slot4, skillId + 1);
				} else {
					int skillId = 10000;
					m_skill.SetSkill(SkillSlot.Slot1, skillId + 1);
					m_skill.SetSkill(SkillSlot.Slot2, skillId + 2);
					m_skill.SetSkill(SkillSlot.Slot3, 0);
					m_skill.SetSkill(SkillSlot.Slot4, 0);
				}


				m_isChanged = false;
			}
		}
	}
}