using Automation.DataTable;
using System.Collections.Generic;
using UnityEngine;
using Utility.DataTable;

namespace EntitySkill {
	public enum SkillType {
		Active,
		Passive,
		None
	}

	public static class SkillDataSystem {
		private static DataTable<SkillTableRow> m_table;

		public static void Initialize() {
			m_table = DataTableLoader.GetTable<SkillTableRow>();
		}
		public static IEnumerable<SkillTableRow> Values => m_table.Values;

		public static bool IsContainsID(int id) { 
			if (m_table == null) {
				Initialize();
			}
			return m_table.ContainsID(id);
		}
		public static SkillType GetSkillType(int id) {
			if (m_table == null) {
				Initialize();
			}

			if (!m_table.ContainsID(id)) {
				Debug.LogError($"Invaild ID.. ({id})");
				return SkillType.None;
			}

			return m_table[id].Type switch {
				"패시브" => SkillType.Passive,
				"액티브" => SkillType.Active,
				_ => SkillType.None,
			};
		}
		public static float GetCoolTime(int id) {
			if (m_table == null) {
				Initialize();
			}

			if (!m_table.ContainsID(id)) {
				Debug.LogError($"Invaild ID.. ({id})");
				return 0.0f;
			}

			return m_table[id].CoolTime;
		}
		public static float GetValue(int id, string key) {
			if (m_table == null) {
				Initialize();
			}

			if (!m_table.ContainsID(id)) {
				Debug.LogError($"Invaild ID.. ({id})");
				return 0.0f;
			}

			SkillTableRow skillData = m_table[id];
			if (!skillData.SkillDatas.ContainsKey(key)) {
				Debug.LogError($"Invaild Key.. ({key})");
				return 0.0f;
			}

			return skillData.SkillDatas[key];
		}
	}
}