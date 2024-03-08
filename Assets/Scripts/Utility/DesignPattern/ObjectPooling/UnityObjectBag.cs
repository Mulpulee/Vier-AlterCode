using System;
using UnityEngine;
using System.Collections.Concurrent;
using Utility.DesignPattern.ObjectPooling;

namespace Utility.DesignPattern.ObjectPooling {
	public class UnityObjectBag<TObject> : ObjectBag<TObject> where TObject : Component, IPoolable {
		private Transform m_objectParent;

		public UnityObjectBag(Func<TObject> objectSpawn, Action<TObject> objectReset, int initSize, Transform parent) {
			m_objectSpawn = objectSpawn;
			m_objectReset = objectReset;
			m_objectBag = new ConcurrentBag<TObject>();
			m_objectParent = parent;

			for (int i = 0; i < initSize; i++) {
				TObject instance = m_objectSpawn.Invoke();
				instance.gameObject.SetActive(false);
				instance.transform.SetParent(parent);
				m_objectBag.Add(instance);
			}
		}

		public override TObject GetObject() {
			TObject spawned = base.GetObject();
			spawned.gameObject.SetActive(true);
			spawned.transform.parent = null;
			return spawned;
		}

		public override void Release(TObject @object) {
			base.Release(@object);
			@object.gameObject.SetActive(false);
			@object.transform.SetParent(m_objectParent);
		}
	}
}