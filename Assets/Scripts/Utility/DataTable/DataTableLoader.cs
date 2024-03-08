using System.Collections.Generic;
using UnityEngine;

namespace Utility.DataTable { 
	public static class DataTableLoader {
		private static Dictionary<string, ScriptableObject> m_tableByName;

		public static void Initialize() {
			m_tableByName = new Dictionary<string, ScriptableObject>();

			ScriptableObject[] loaded = Resources.LoadAll<ScriptableObject>("DataTableAsset");
			for (int i = 0; i < loaded.Length; i++) {
				string name = loaded[i].name.Replace("Asset", "");
				m_tableByName.Add(name, loaded[i]);
			}
		}

		public static DataTable<TRow> GetTable<TRow>() where TRow : DataTableRow {
			if (m_tableByName == null || m_tableByName.Count <= 0) {
				Initialize();
			}

			string tableName = typeof(TRow).Name.Replace("Row", "");

			if (m_tableByName.ContainsKey(tableName)) {
				DataTableAsset<TRow> asset = (DataTableAsset<TRow>)m_tableByName[tableName];

				return asset.Table;
			}

			return null;
		}
	}
}