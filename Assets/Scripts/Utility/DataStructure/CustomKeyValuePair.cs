using System.Collections.Generic;

namespace Utility.DataStructure {
	[System.Serializable]
	public class CustomKeyValuePair<TKey, TValue> {
		public TKey Key;
		public TValue Value;
		public CustomKeyValuePair(TKey key, TValue value) {
			Key = key;
			Value = value;
		}
		public CustomKeyValuePair(KeyValuePair<TKey, TValue> pair) {
			Key = pair.Key;
			Value = pair.Value;
		}
	}
}