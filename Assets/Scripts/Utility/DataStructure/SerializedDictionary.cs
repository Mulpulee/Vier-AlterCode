using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Utility.DataStructure {
	[System.Serializable]
	public class SerializedDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver {
		public SerializedDictionary() {}
#if UNITY_EDITOR
		public SerializedDictionary(Dictionary<K, V> data) {
			foreach (var item in data) {
				Add(item.Key, item.Value);
			}
		}
#endif
		protected SerializedDictionary(SerializationInfo info, StreamingContext context) : base(info, context) {}
		[SerializeField] private List<CustomKeyValuePair<K, V>> datas = new();
		public void OnBeforeSerialize() {
			datas.Clear();

			foreach (KeyValuePair<K, V> pair in this) {
				datas.Add(new CustomKeyValuePair<K, V>(pair));
			}
		}

		public void OnAfterDeserialize() {
			this.Clear();
			for (int i = 0, icount = datas.Count; i < icount; ++i) {
				this.Add(datas[i].Key, datas[i].Value);
			}
		}
	}
}