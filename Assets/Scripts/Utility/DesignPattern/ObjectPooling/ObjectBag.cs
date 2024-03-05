using System;
using System.Collections.Concurrent;

namespace Utility.DesignPattern.ObjectPooling {
	public class ObjectBag<TObject> where TObject : IPoolable {
		protected Func<TObject> m_objectSpawn;
		protected Action<TObject> m_objectReset;
		protected ConcurrentBag<TObject> m_objectBag;

		public ObjectBag() { }
		public ObjectBag(Func<TObject> objectSpawn, Action<TObject> objectReset, int initSize = 32) {
			m_objectSpawn = objectSpawn;
			m_objectReset = objectReset;
			m_objectBag = new ConcurrentBag<TObject>();
			for (int i = 0; i < initSize; i++) {
				m_objectBag.Add(m_objectSpawn.Invoke());
			}
		}

		public virtual TObject GetObject() {
			TObject instance;
			if (m_objectBag.TryTake(out TObject item)) {
				instance = item;
			} else {
				instance = m_objectSpawn.Invoke();
			}

			instance.SetRelease(() => Release(instance));

			m_objectReset.Invoke(instance);
			return instance;
		}
		public virtual void Release(TObject @object) {
			m_objectBag.Add(@object);
		}
	}
}