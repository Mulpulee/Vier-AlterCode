using UnityEngine;
using Utility.DataStructure;

namespace Entity {
	[System.Serializable]
	public class EntityStatus {
		public static readonly float LimitedAttack				= 100.0f;
		public static readonly float LimitedAttackSpeed			= 10.0f;
		public static readonly float LimitedAttackCritical		= 100.0f;
		public static readonly float LimitedAttackRange			= 50.0f;
		public static readonly float LimitedDefense				= 100.0f;
		public static readonly float LimitedDamageReductionRate = 100.0f;
		public static readonly float LimitedSpeed				= 10.0f;
		public static readonly float LimitedVision				= 50.0f;

		[SerializeField] private string m_name;
		[SerializeField] private LimitedValue m_health;
		[SerializeField] private LimitedValue m_healthRegen;
		[SerializeField] private LimitedValue m_stamina;
		[SerializeField] private LimitedValue m_staminaRegen;

		[SerializeField] private LimitedValue m_attack;
		[SerializeField] private LimitedValue m_attackSpeed;
		[SerializeField] private LimitedValue m_attackCritical;
		[SerializeField] private LimitedValue m_attackRange;

		[SerializeField] private LimitedValue m_defense;
		[SerializeField] private LimitedValue m_damageReductionRate;
		[SerializeField] private LimitedValue m_speed;
		[SerializeField] private LimitedValue m_vision;
		[SerializeField] private bool m_isInvincibility;
		[SerializeField] private bool m_isBind;

		public string Name {
			get => m_name;
			set => m_name = value;
		}
		public float Health {
			get => m_health.Value;
			set => m_health.Value = value;
		}
		public float HealthRegen {
			get => m_healthRegen.Value;
			set => m_healthRegen.Value = value;
		}
		public float Stamina {
			get => m_stamina.Value;
			set => m_stamina.Value = value;
		}
		public float StaminaRegen {
			get => m_staminaRegen.Value;
			set => m_staminaRegen.Value = value;
		}
		public float Attack {
			get => m_attack.Value;
			set => m_attack.Value = value;
		}
		public float AttackSpeed {
			get => m_attackSpeed.Value;
			set => m_attackSpeed.Value = value;
		}
		public float AttackCritical {
			get => m_attackCritical.Value;
			set => m_attackCritical.Value = value;
		}
		public float AttackRange {
			get => m_attackRange.Value;
			set => m_attackRange.Value = value;
		}
		public float Defense {
			get => m_defense.Value;
			set => m_defense.Value = value;
		}
		public float DamageReductionRate {
			get => m_damageReductionRate.Value;
			set => m_damageReductionRate.Value = value;
		}
		public float Speed {
			get => m_speed.Value;
			set => m_speed.Value = value;
		}
		public float Vision {
			get => m_vision.Value;
			set => m_vision.Value = value;
		}
		public bool IsInvincibility {
			get => m_isInvincibility;
			set => m_isInvincibility = value;
		}
		public bool IsBind {
			get => m_isBind;
			set => m_isBind = value;
		}

		public LimitedValue OriginHealth => m_health;
		public LimitedValue OriginHealthRegen => m_healthRegen;
		public LimitedValue OriginStamina => m_stamina;
		public LimitedValue OriginStaminaRegen => m_staminaRegen;
		public LimitedValue OriginAttack => m_attack;
		public LimitedValue OriginAttackSpeed => m_attackSpeed;
		public LimitedValue OriginAttackCritical => m_attackCritical;
		public LimitedValue OriginAttackRange => m_attackRange;
		public LimitedValue OriginDefense => m_defense;
		public LimitedValue OriginSpeed => m_speed;
		public LimitedValue OriginVision => m_vision;
		public LimitedValue OriginDamageReductionRate => m_damageReductionRate;

		public EntityStatus() {
			m_name = "";
			m_health				= new LimitedValue(0.0f,  20.0f,  20.0f);
			m_stamina				= new LimitedValue(0.0f, 100.0f, 100.0f);
			m_healthRegen			= new LimitedValue(0.0f, 100.0f, 0.0f);
			m_staminaRegen			= new LimitedValue(0.0f, 100.0f, 1.0f);

			m_attack				= new LimitedValue(0.0f, EntityStatus.LimitedAttack);
			m_attackSpeed			= new LimitedValue(0.0f, EntityStatus.LimitedAttackSpeed);
			m_attackCritical		= new LimitedValue(0.0f, EntityStatus.LimitedAttackCritical);
			m_attackRange			= new LimitedValue(0.0f, EntityStatus.LimitedAttackRange);
			m_defense				= new LimitedValue(0.0f, EntityStatus.LimitedDefense);
			m_speed					= new LimitedValue(0.0f, EntityStatus.LimitedSpeed);
			m_vision				= new LimitedValue(0.0f, EntityStatus.LimitedVision);
			m_damageReductionRate	= new LimitedValue(0.0f, EntityStatus.LimitedDamageReductionRate);
			m_isInvincibility = false;
			m_isBind = false;
		}
		public EntityStatus(string name) : this() {
			m_name = name;
		}
		public EntityStatus(
			string name,
			float health,
			float stamina,
			float attack,
			float attackSpeed,
			float attackCritical,
			float defense,
			float speed
			) : this(name) {
			m_health.Value = health;
			m_speed.Value = stamina;
			m_attack.Value = attack;
			m_attackSpeed.Value = attackSpeed;
			m_attackCritical.Value = attackCritical;
			m_defense.Value = defense;
			m_speed.Value = speed;
		}

		public EntityStatus(
			string name,
			LimitedValue health,
			LimitedValue stamina,
			LimitedValue attack,
			LimitedValue attackSpeed,
			LimitedValue attackCritical,
			LimitedValue defense,
			LimitedValue speed
			) {
			m_name = name;
			m_health = health;
			m_stamina = stamina;
			m_attack = attack;
			m_attackSpeed = attackSpeed;
			m_attackCritical = attackCritical;
			m_defense = defense;
			m_speed = speed;
		}
	}
}