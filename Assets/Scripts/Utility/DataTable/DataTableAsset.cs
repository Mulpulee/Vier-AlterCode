using UnityEngine;

namespace Utility.DataTable {
	public class DataTableAsset<T> : ScriptableObject where T : DataTableRow {
		public DataTable<T> Table;
	}
}