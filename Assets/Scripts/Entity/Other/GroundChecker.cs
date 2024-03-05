using System;
using UnityEngine;

namespace Entity.Other {
	public class GroundChecker : MonoBehaviour {
		[SerializeField] private Vector2 m_offset;
		[SerializeField] private float m_radius;
		[SerializeField] private LayerMask m_groundLayer;

		public Vector3 CenterPosition => transform.position + (Vector3)m_offset;

		private Collider[] m_cacheColliders;
		private Collider m_collider;

		private void Awake() {
			m_collider = GetComponent<Collider>();
			m_cacheColliders = new Collider[10];
		}

		public bool IsGround() {
			Array.Clear(m_cacheColliders, 0, m_cacheColliders.Length);
			int colliderCount = Physics.OverlapSphereNonAlloc(CenterPosition, m_radius, m_cacheColliders, m_groundLayer);

			bool isTriggerOnly = true;
			if (colliderCount > 0) {
				for (int i = 0; i < colliderCount; i++) {
					if (m_cacheColliders[i].isTrigger) {
						continue;
					} else {
						if (!Physics.GetIgnoreCollision(m_collider, m_cacheColliders[i])) {
							isTriggerOnly = false;
						}
					}
				}
			} else {
				return false;
			}

			return !isTriggerOnly;
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;

			Gizmos.DrawWireSphere(CenterPosition, m_radius);

			Gizmos.color = Color.white;
		}
	}
}