using System;
using System.Collections.Generic;

namespace Utility.DesignPattern.ObjectPooling {
	public class ObjectPool<TKey, TObject> where TObject : IPoolable {
		protected Dictionary<TKey, ObjectBag<TObject>> m_poolByKey;

		public ObjectPool() {
			m_poolByKey = new Dictionary<TKey, ObjectBag<TObject>>();
		}

		public virtual void AssignObject(TKey key, Func<TObject> objectSpawn, Action<TObject> resetObject, int initSize = 32) {
			var objectBag = new ObjectBag<TObject>(objectSpawn, resetObject, initSize);
			m_poolByKey.Add(key, objectBag);
		}

		public virtual bool TrySpawnObject(TKey key, out TObject @object) {
			bool value = m_poolByKey.TryGetValue(key, out ObjectBag<TObject> item);
			@object = item.GetObject();

			return value;
		}

		public virtual TObject SpawnObject(TKey key) {
			bool value = m_poolByKey.TryGetValue(key, out ObjectBag<TObject> item);

			if (!value) {
				throw UnknownKeyException(key);
			}

			TObject instance = item.GetObject();

			return instance;
		}

		public virtual void ReleaseObject(TKey key, TObject @object) {
			bool value = m_poolByKey.TryGetValue(key, out ObjectBag<TObject> item);

			if (!value) {
				throw UnknownKeyException(key);
			}

			item.Release(@object);
		}

		protected Exception UnknownKeyException(TKey key) {
			var exception = new Exception($"{key} is nuknown key");

			return exception;
		}
	}
}