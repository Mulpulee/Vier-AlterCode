namespace Entity {
	public class EntityHitData {
		public class HitInfo {
			public EntityBehaviour entity;
			public int count;
			public float damage;

			public void Reset() {
				entity = null;
				count = 0;
				damage = 0.0f;
			}
		}

		public HitInfo lastAttack = new();
		public HitInfo lastHit = new();

		private static void BasicCheckLast(HitInfo hitInfo, EntityBehaviour entity, float damage) {
			if (hitInfo.entity == entity) {
				hitInfo.count++;
			} else {
				hitInfo.count = 1;
			}
			hitInfo.entity = entity;
			hitInfo.damage = damage;
		}

		public void LastAttack(EntityBehaviour entity, float damage) => EntityHitData.BasicCheckLast(lastAttack, entity, damage);
		public void LastHit(EntityBehaviour entity, float damage) => EntityHitData.BasicCheckLast(lastHit, entity, damage);
		
		public void ResetLastHit() {
			lastHit.Reset();
		}
	}
}