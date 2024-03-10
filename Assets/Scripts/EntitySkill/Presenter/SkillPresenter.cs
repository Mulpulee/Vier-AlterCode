using Entity.Components;
using Entity.Player;
using GameSystemManager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.DesignPattern.MVP;

namespace EntitySkill.Presenter {
	public class SkillPresenter : PresenterBase<SkillView> {
		private Dictionary<int, Skill> m_skillByIndex = new();

		public override void InitializePresenter() {
			PlayerEvent.Instance.OnPlayerSpawned += OnPlayerSpawnd;
		}

		private void Start() {
			Bind();
		}

		public override void Bind() {
			OpenView(0);
			OnKeyChanged();
		}

		private void OnPlayerSpawnd() {
			PlayerSkillComponent psc = GameObject.FindObjectOfType<PlayerSkillComponent>();

			GameInputManager.Instance.OnKeyChanged += OnKeyChanged;
			psc.OnSkillUpdated += OnSkillUpdate;
		}

		private void OnSkillUpdate(SkillSlot slot, Skill skill) {
			View.SkillElement[(int)slot].LoadResource(skill);
			m_skillByIndex[(int)slot] = skill;
		}
		private void OnKeyChanged() {
			for (int i = 0; i < (int)SkillSlot.Count; i++) {
				View.SkillElement[i].BindKey((SkillSlot)i);
				LayoutRebuilder.ForceRebuildLayoutImmediate(View.SkillElement[i].KeyText.rectTransform);
			}
		}

		private void Update() {
			for (int i = 0; i < (int)SkillSlot.Count; i++) { 
				if (m_skillByIndex.TryGetValue(i, out Skill skill)) {
					if (skill.Cooldown.CoolTime == 0.0f && (skill.NowState != SkillState.InUse || skill.Type == SkillType.Passive)) {
						View.SkillElement[i].SolidImage.fillAmount = 0.0f;
						continue;
					}

					View.SkillElement[i].SolidImage.fillAmount = skill.NowState switch {
						SkillState.UnAvailable or SkillState.InUse => 1.0f,
						_ => skill.Cooldown.RemainingTime / skill.Cooldown.CoolTime,
					};
				}
			}
		}

		public override void Release() {

		}
	}
}