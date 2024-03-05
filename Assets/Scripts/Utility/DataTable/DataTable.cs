using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.DataStructure;

namespace Utility.DataTable {
	[System.Serializable]
	public class DataTable<T> : IEnumerator<T>, IEnumerable<T> where T : DataTableRow {
		[SerializeField] private SerializedDictionary<int, T> m_data;
		public int Count => m_data.Count;
		public IEnumerable<T> Values => m_data.Values;

		public bool ContainsID(int id) => m_data.ContainsKey(id);

		public T ElementAt(int index) => m_data.ElementAt(index).Value;
		public T this[int id] => m_data[id];

#if UNITY_EDITOR
		public DataTable(Dictionary<int, T> initDatas) {
			m_data = new SerializedDictionary<int, T>();

			foreach (KeyValuePair<int, T> data in initDatas) {
				m_data.Add(data.Key, data.Value);
			}
		}
		public DataTable(object initObject) {
			m_data = initObject as SerializedDictionary<int, T>;
		}
#endif

		private Int32 m_position = -1;

		public T Current => m_data.ElementAt(m_position).Value;

		System.Object IEnumerator.Current => Current;

		public IEnumerator<T> GetEnumerator() {
			foreach (var item in m_data) {
				yield return item.Value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public Boolean MoveNext() {
			if (m_position == m_data.Count - 1) {
				Reset();
				return false;
			}

			m_position++;
			return (m_position < m_data.Count);
		}

		public void Reset() => m_position = -1;

		public void Dispose() {
			Debug.LogError($"Dispose Called!");
		}
	}
}