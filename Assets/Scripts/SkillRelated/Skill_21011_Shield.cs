using Entity;
using Entity.Interface;
using UnityEngine;
using Utility.Extension;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
public class Skill_21011_Shield : MonoBehaviour {
	[Header("Debug")]
	[SerializeField] private Rigidbody m_rigidbody;
	[SerializeField] private float m_damage;
	[SerializeField] private float m_knockbackPower;
	[SerializeField] private float m_power;
	[SerializeField] private float m_removeMinSpeed;
	[SerializeField] private Facing2D m_direction;
	[SerializeField] private EntityBehaviour m_caster;

	private Vector3 Direction => Vector3.right * (int)m_direction;

	private void Awake() {
		if (TryGetComponent(out BoxCollider collider)) {
			collider.isTrigger = true;
		}
		m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.freezeRotation = true;
		m_rigidbody.useGravity = false;
	}

	public void ShieldStart() {
		m_rigidbody.AddForce(Direction * m_power, ForceMode.Impulse);
	}

	private void Update() {
		if (m_rigidbody.velocity.magnitude <= m_removeMinSpeed) {
			Destroy(gameObject);
		}
	}

	public void SetDamage(float damage) => m_damage = damage;
	public void SetKnockbackPower(float knockbackPower) => m_knockbackPower = knockbackPower;
	public void SetMovePower(float power) => m_power = power;
	public void SetDirection(Facing2D direction) => m_direction = direction;
	public void SetRemoveMinSpeed(float removeMinSpeed) => m_removeMinSpeed = removeMinSpeed;
	public void SetCaster(EntityBehaviour caster) => m_caster = caster;


	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			m_rigidbody.velocity = Vector3.zero;
		}

		if (other.TryGetComponent(out EntityBehaviour entity)) {
			if (entity == m_caster) {
				return;
			}
		}

		if (other.TryGetComponent(out Skill_21011_Shield _)) {
			return;
		}

		if (other.TryGetComponent(out IHittable hit)) {
			hit.OnHit(gameObject, m_damage, HitType.Skill);
		}
		if (other.TryGetComponent(out Rigidbody rigidbody)) {
			rigidbody.AddForce(Direction * m_knockbackPower, ForceMode.Force);
		}

	}
}