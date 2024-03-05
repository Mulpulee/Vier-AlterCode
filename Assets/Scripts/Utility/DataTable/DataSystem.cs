using System.Collections.Generic;
using System;
using UnityEngine;

namespace Automation.DataTable {

}

namespace Utility.DataTable {
	public static class DataSystem<T> where T : DataTableRow {
		private static DataTable<T> m_table;

		public static void Initialize() {
			m_table = DataTableLoader.GetTable<T>();
		}

		public static IEnumerable<T> GetDatas() {
			if (m_table == null) {
				Initialize();
			}

			return m_table.Values;
		}

		public static T GetRow(int id) {
			if (m_table == null) {
				Initialize();
			}

			if (m_table.ContainsID(id)) {
				return m_table[id];
			}

			Debug.LogError($"Invaild ID.. ({id})");
			return null;
		}
	}
}