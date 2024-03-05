using UnityEngine;

namespace Utility.Extension {
	public static class InputUtility {
		private static Camera m_camera;
		private static Camera Camera {
			get {
				if (m_camera == null) {
					m_camera = Camera.main;
				}

				return m_camera;
			}
		}

		public static Vector3 WorldMousePosition => Camera.ScreenToWorldPoint(Input.mousePosition);

		public static Vector3 GetDirectionToPosition(Transform transform, Vector3 position) {
			Vector3 direction = position - transform.position;

			direction.z = 0.0f;
			return direction.normalized;
		}
		public static Vector3 GetDirectionToMouse(Transform transform) => GetDirectionToPosition(transform, WorldMousePosition);
	}
}