using System;
using UnityEngine;

namespace Utility.Extension {
	public enum Facing2D {
		Left = -1,
		Right = 1
	}

	public static class TransfromExtension {
		public static RectTransform ToRect(this Transform transform) {
			return (RectTransform)transform;
		}
		public static void DestroyChildren(this Transform transform) {
			foreach (Transform item in transform) {
				GameObject.Destroy(item.gameObject);
			}
		}
		public static void DestroyChilrenExcepts(this Transform transform, Transform[] excepts) {
			foreach (Transform item in transform) {
				bool isExcept = false;

				foreach (Transform except in excepts) {
					if (item == except) {
						isExcept = true;
						break;
					}
				}

                if (!isExcept) {
					GameObject.Destroy(item.gameObject);
                }
            }
		}

		public static Facing2D GetFacing2D(this Transform transform) {
			if (transform.lossyScale.x > 0) {
				return Facing2D.Right;
			} else {
				return Facing2D.Left;
			}
		}
		public static void SetFacing2D(this Transform transform, Facing2D facing) {
			int FacingScale = (int)facing;
			float targetScale = Mathf.Abs(transform.localScale.x) * FacingScale;

			Vector3 scale = transform.localScale;
			scale.x = targetScale;
			transform.localScale = scale;
		}

		private static Quaternion GetRotate2D(Vector3 position, Vector2 target) {
			target -= (Vector2)position;

			float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
			return Quaternion.Euler(Vector3.forward * angle);
		}
		public static void LookAt2D(this Transform transform, Vector2 target) => transform.rotation = GetRotate2D(transform.position, target);
	}
}