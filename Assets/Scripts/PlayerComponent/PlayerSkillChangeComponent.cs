using Entity.Player;
using EntitySkill;
using GameSystemManager;
using System.Collections;
using UnityEngine;

namespace Entity.Components {
	public class PlayerSkillChangeComponent : EntityComponent {
		private static readonly float FormChangeDelay = 0.5f;
		
		private bool m_isChanged = false;
		private int m_skillNumber = 0;
		private PlayerSkillComponent m_skill;
		private PlayerAnimationController m_anim;
		private bool m_isFormDelay = false;

		private void Start() {
			m_skill = Entity.GetComponent<PlayerSkillComponent>();
			m_anim = Entity.GetComponent<PlayerAnimationController>();
		}

		private IEnumerator FormDelay() {
			m_isFormDelay = true;

            yield return new WaitForSeconds(PlayerSkillChangeComponent.FormChangeDelay);
			
			m_isFormDelay = false;

        }

		public void Update() {
			if (m_isFormDelay) {
				return;
			}

			if (GameInputManager.GetKeyDown(InputType.FormSlot1)) {
				if (m_skillNumber != 0) {
					m_skillNumber = 0;
					m_isChanged = true;
					m_anim.ChangeForm(m_skillNumber);
				}
			}
			if (GameInputManager.GetKeyDown(InputType.FormSlot2)) {
				if (m_skillNumber != 1) {
					m_skillNumber = 1;
					m_isChanged = true;
					m_anim.ChangeForm(m_skillNumber);
				}
			}
			if (GameInputManager.GetKeyDown(InputType.FormSlot3)) {
				if (m_skillNumber != 2) {
					m_skillNumber = 2;
					m_isChanged = true;
					m_anim.ChangeForm(m_skillNumber);
				}
			}
			if (GameInputManager.GetKeyDown(InputType.FormSlot4)) {
				if (m_skillNumber != 3) {
					m_skillNumber = 3;
					m_isChanged = true;
					m_anim.ChangeForm(m_skillNumber);
				}
			}
			if (GameInputManager.GetKeyDown(InputType.FormSlot5)) {
				if (m_skillNumber != 4) {
					m_skillNumber = 4;
					m_isChanged = true;
					m_anim.ChangeForm(m_skillNumber);
				}
			}

			if (m_isChanged) {
				if (m_skillNumber != 0) {
					int skillId = 20000 + (m_skillNumber * 1000);
					m_skill.SetSkill(SkillSlot.Slot1, 10001);
					m_skill.SetSkill(SkillSlot.Slot2, 20001);
					m_skill.SetSkill(SkillSlot.Slot3, skillId + 11);
					m_skill.SetSkill(SkillSlot.Slot4, 20002);
					m_skill.SetSkill(SkillSlot.Slot5, skillId + 1);
				} else {
					int skillId = 10000;
					m_skill.SetSkill(SkillSlot.Slot1, skillId + 1);
					m_skill.SetSkill(SkillSlot.Slot2, 20001);
					m_skill.SetSkill(SkillSlot.Slot3, skillId + 2);
					m_skill.SetSkill(SkillSlot.Slot4, 0);
					m_skill.SetSkill(SkillSlot.Slot5, 0);
				}

				StartCoroutine(FormDelay());
				m_isChanged = false;
			}
		}
	}
}