using System;
using UnityEngine;
using Utility.DesignPattern.ObjectPooling;

namespace Utility.DesignPattern.ObjectPooling {
	public class UnityObjectPool<TKey, TObject> : ObjectPool<TKey, TObject> where TObject : Component, IPoolable {
		private readonly Transform m_poolParent;

		public UnityObjectPool(Transform parent) {
			m_poolParent = parent;
		}

		public override void AssignObject(TKey key, Func<TObject> objectSpawn, Action<TObject> resetObject, int initSize = 32) {
			var objectBag = new UnityObjectBag<TObject>(objectSpawn, resetObject, initSize, m_poolParent);
			m_poolByKey.Add(key, objectBag);
		}

		public override bool TrySpawnObject(TKey key, out TObject @object) {
			bool value = m_poolByKey.TryGetValue(key, out ObjectBag<TObject> item);
			@object = item.GetObject();
			return value;
		}

		public override TObject SpawnObject(TKey key) {
			bool value = m_poolByKey.TryGetValue(key, out ObjectBag<TObject> item);

			if (!value) {
				throw UnknownKeyException(key);
			}

			TObject instance = item.GetObject();
			return instance;
		}

		public override void ReleaseObject(TKey key, TObject @object) {
			bool value = m_poolByKey.TryGetValue(key, out ObjectBag<TObject> item);

			if (!value) {
				throw UnknownKeyException(key);
			}

			item.Release(@object);
		}
	}
}