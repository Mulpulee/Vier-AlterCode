using Entity.Components;
using GameSystemManager;
using Mono.Cecil.Cil;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Utility.Management;

namespace EntitySkill.Presenter {
	public class SkillViewElement : MonoBehaviour {
		public Image SkillImage => m_skillImage;
		public Image SolidImage => m_solidImage;
		public Text KeyText => m_keyText;
		public Image KeyImage => m_keyImage;

		public bool IsCoolDown => m_isCoolDown;
		private bool m_isCoolDown = false;

		[SerializeField] private Image m_skillImage;
		[SerializeField] private Image m_solidImage;
		[SerializeField] private Text m_keyText;
		[SerializeField] private Image m_keyImage;

		public void LoadResource(Skill skill) {
			m_skillImage.sprite = ResourceLoader.SkillImageLoad(skill.ID);
			
			switch (skill.Type) {
				case SkillType.Active:
					m_solidImage.fillAmount = 0.0f;
					m_keyText.gameObject.SetActive(true);
					m_keyImage.gameObject.SetActive(true);
					break;
				case SkillType.Passive:
					m_solidImage.fillAmount = 1.0f;
					m_keyText.gameObject.SetActive(false);
					m_keyImage.gameObject.SetActive(false);
					break;
				case SkillType.None:
					m_solidImage.fillAmount = 0.5f;
					break;
			}
		}
		public void BindKey(SkillSlot slot) {
			InputType type = GameInputManager.SkillSlotToInputType(slot);
			KeyCode code = GameInputManager.Instance.GetKeyCode(type);

			m_keyText.text = $"{code}";
		}

		public void CoolDown(float time) {
			StartCoroutine(StartCoolDown(time));
		}

		private IEnumerator StartCoolDown(float coolTime) {
			m_isCoolDown = true;

			float progress = 0.0f;
			float time = 0.0f;
			m_solidImage.fillAmount = 1.0f;
			while (progress < 1.0f) {
				progress = time / coolTime;
				m_solidImage.fillAmount = 1.0f - progress;

				time += TimeManager.DeltaTime;

				yield return null;
			}
			m_solidImage.fillAmount = 0.0f;

			m_isCoolDown = false;
		}
	}
}