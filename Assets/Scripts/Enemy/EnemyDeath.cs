using Entity;
using Entity.Interface;
using UnityEngine;

public class EnemyDeath : IDeath {
	private static float DESTROY_DELAY = 3.0f;

	public void OnDeath(EntityBehaviour entity) {
		if (entity.TryGetComponent(out EnemyAI enemyAI)) {
			enemyAI.enabled = false;
        }
		if (entity.TryGetComponent(out EnemyAI enemyCollider)) {
			enemyCollider.enabled = false;
		}

		if (entity.TryGetComponent(out Rigidbody enemyRigidebody)) {
			GameObject.Destroy(enemyRigidebody);
		}

		GameObject.Destroy(entity.gameObject, EnemyDeath.DESTROY_DELAY);
		if (entity.TryGetComponent(out Enemy enemy)) {
			GameObject.Destroy(enemy.hpBar.gameObject, EnemyDeath.DESTROY_DELAY);
		}
	}

	public void Destroy() {

	}

}