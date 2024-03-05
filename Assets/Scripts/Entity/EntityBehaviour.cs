using System;
using UnityEngine;
using Entity.Basic;
using Entity.Interface;
using Utility.Management;

namespace Entity {
	public class EntityBehaviour : MonoBehaviour, IHittable, IDeathable {
		[SerializeField] private int m_id;
		[SerializeField] private EntityStatus m_status;
		private IHit m_hit;
		private IDeath m_death;

		private EntityHitData m_normalHitData = new();
		private EntityHitData m_skillHitData = new();

		public EntityHitData NormalHitData => m_normalHitData;
		public EntityHitData SkillHitData => m_skillHitData;

		public EntityStatus Status => m_status;

		public int ID => m_id;
		public string Name => m_status.Name;
		public bool IsAlive => m_status.Health > 0;
		public Vector3 Position {
			get => transform.position;
			set => transform.position = value;
		}

		public IHit HitHandler => m_hit;
		public IDeath DeathHandler => m_death;

		public EntityHitData GetHitData(HitType hitType) => hitType switch {
			HitType.Normal => m_normalHitData,
			HitType.Skill => m_skillHitData,
			_ => null
		};
		private void Update() {
			float deltaTime = TimeManager.DeltaTime;
			m_status.Health += m_status.HealthRegen * deltaTime;
			m_status.Stamina += m_status.StaminaRegen * deltaTime;
		}

		public void ReInitHandler(IHit hit, IDeath death) {
			m_hit = hit;
			m_death = death;
		}
		public void ReInitStatus(EntityStatus status) {
			m_status = status;
		}
		public void ReInitID(int id) {
			m_id = id;
		}

		public void OnHit(GameObject attacker, float damage, HitType hitType) {
			if (attacker != null) {
				if (attacker.TryGetComponent(out EntityBehaviour entity)) {
					GetHitData(hitType)?.LastHit(entity, damage);
					entity.GetHitData(hitType)?.LastAttack(this, damage);
				} else {
					GetHitData(hitType)?.ResetLastHit();
				}
			}

			m_hit?.OnHit(this, attacker, damage, hitType);
		}
		public void OnDeath() => m_death?.OnDeath(this);

		public T AddEntityComponent<T>() where T : EntityComponent {
			T instance = gameObject.AddComponent<T>();
			instance.Initialize(this);

			return instance;
		}
		public T AddEntityComponent<T>(Action<T> callback) where T : EntityComponent {
			T instance = AddEntityComponent<T>();
			callback?.Invoke(instance);

			return instance;
		}

		public void OnDestroy() {
			m_hit?.Destroy();
			m_death?.Destroy();

			m_hit = null;
			m_death = null;
		}

		public static EntityBuilder GetBuilder(EntityBehaviour prefab, Vector3 position) => EntityBuilder.GetBuilder(prefab, position);

		public class EntityBuilder {
			private static EntityBuilder m_entityBuilder;
			public static EntityBuilder GetBuilder(EntityBehaviour prefab, Vector3 position) {
				m_entityBuilder ??= new EntityBuilder();

				m_entityBuilder.Reset();
				m_entityBuilder.m_instance = Instantiate(prefab, position, prefab.transform.rotation);
				m_entityBuilder.m_status = m_entityBuilder.m_instance.Status;

				return m_entityBuilder;
			}

			private EntityBehaviour m_instance;
			private EntityStatus m_status;
			private IHit m_hit;
			private IDeath m_death;

			private EntityBuilder() { }

			public EntityBehaviour CurrentEntityInstance => m_instance;
			public EntityBuilder AddEntityComponent<T>() where T : EntityComponent {
				m_instance.AddEntityComponent<T>();

				return this;
			}
			public EntityBuilder AddEntityComponent<T>(Action<T> callback) where T : EntityComponent {
				m_instance.AddEntityComponent<T>(callback);

				return this;
			}
			public EntityBuilder SetName(string name) {
				m_status.Name = name;

				return this;
			}

			public EntityBuilder SetAttack(float attack) {
				m_status.Attack = attack;

				return this;
			}
			public EntityBuilder SetAttackSpeed(float attackSpeed) {
				m_status.AttackSpeed = attackSpeed;

				return this;
			}
			public EntityBuilder SetAttackCirtical(float attackCritical) {
				m_status.AttackCritical = attackCritical;

				return this;
			}
			public EntityBuilder SetDefense(float defense) {
				m_status.Defense = defense;

				return this;
			}
			public EntityBuilder SetSpeed(float speed) {
				m_status.Speed = speed;

				return this;
			}

			public EntityBuilder SetHealth(float health) {
				m_status.Health = health;

				return this;
			}
			public EntityBuilder SetStamina(float stamina) {
				m_status.Stamina = stamina;

				return this;
			}

			public EntityBuilder SetMaxHealth(float maxHealth) {
				m_status.OriginHealth.SetMaxValue(maxHealth);

				return this;
			}
			public EntityBuilder SetMaxHealth(Func<float> maxHealth) {
				m_status.OriginHealth.SetMaxValue(maxHealth);

				return this;
			}
			public EntityBuilder SetMaxStamina(float maxStamina) {
				m_status.OriginStamina.SetMaxValue(maxStamina);

				return this;
			}
			public EntityBuilder SetMaxStamina(Func<float> maxStamina) {
				m_status.OriginStamina.SetMaxValue(maxStamina);

				return this;
			}

			public EntityBuilder SetHit(IHit hit) {
				m_hit = hit;

				return this;
			}
			public EntityBuilder SetDeath(IDeath death) {
				m_death = death;

				return this;
			}
			public EntityBuilder SetStatus(EntityStatus status) {
				m_status = status;

				return this;
			}

			public EntityBehaviour Build() {
				m_status ??= new EntityStatus("Default Name", 20, 10, 1, 1, 0, 0, 1);
				m_hit ??= new EntityHit();
				m_death ??= new EntityDeath();
				m_instance.ReInitStatus(m_status);
				m_instance.ReInitHandler(m_hit, m_death);

				return m_instance;
			}

			public void Reset() {
				m_instance = null;
				m_status = null;
				m_hit = null;
				m_death = null;
			}
		}
	}
}
