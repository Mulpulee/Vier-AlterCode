using Entity.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttack : MonoBehaviour {
	public float damage;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			return;
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			return;
		}

		if (other.TryGetComponent(out IHittable hit)) {
			Debug.Log("Hit!");
			hit.OnHit(gameObject, damage, HitType.Normal);
		}
	}
}
