using Automation.DataTable;
using EntitySkill.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.DesignPattern.FSM;

namespace EntitySkill {
	public static class SkillBinder {
		private static Dictionary<int, Func<FSMState<Skill>>> m_skillByID;

		public static FSMState<Skill> GetState(int ID) {
			if (m_skillByID == null) {
				Bind();
			}

			if (m_skillByID.TryGetValue(ID, out Func<FSMState<Skill>> state)) {
				return state.Invoke();
			} else {
				Debug.LogError($"{ID} is not bind.");
				return null;
			}
		}

		public static void Bind() {
			m_skillByID = new Dictionary<int, Func<FSMState<Skill>>>();
			Debug.Log("Skill Binding..");

			foreach (SkillTableRow skill in SkillDataSystem.Values) {
				Type skillType = Type.GetType($"EntitySkill.Skills.Skill_{skill.ID}");

				if (skillType != null) {
					object instance = Activator.CreateInstance(skillType);

					if (instance != null) {
						m_skillByID.Add(skill.ID, () => (FSMState<Skill>)instance);
					}
				}
			}

			Debug.Log("Skill Binding Compelete!");
		}
	}
}