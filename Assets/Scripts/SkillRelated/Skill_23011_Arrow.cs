using Entity;
using Entity.Interface;
using UnityEngine;
using Utility.Extension;
using Utility.Management;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class Skill_23011_Arrow : MonoBehaviour {
	[Header("Debug")]
	[SerializeField] private float m_damage;
	[SerializeField] private float m_knockbackPower;
	[SerializeField] private float m_moveSpeed;
	[SerializeField] private float m_maxDistance;
	[SerializeField] private Facing2D m_direction;
	[SerializeField] private EntityBehaviour m_caster;

	private Vector3 Direction => Vector3.right * (int)m_direction;
	private float m_distance = 0.0f;

	private void Awake() {
		if (TryGetComponent(out BoxCollider collider)) {
			collider.isTrigger = true;
		}
	}

	private void Update() {
		float distance = m_moveSpeed * TimeManager.DeltaTime;
		transform.position += Direction * distance;
		m_distance += distance;

		if (m_distance >= m_maxDistance) {
			Destroy(gameObject);
		}
	}

	public void SetDamage(float damage) => m_damage = damage;
	public void SetKnockbackPower(float knockbackPower) => m_knockbackPower = knockbackPower;
	public void SetMoveSpeed(float moveSpeed) => m_moveSpeed = moveSpeed;
	public void SetDirection(Facing2D direction) => m_direction = direction;
	public void SetMaxDistance(float maxDistance) => m_maxDistance = maxDistance;
	public void SetCaster(EntityBehaviour caster) => m_caster = caster;


	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			Destroy(gameObject);
		}

		if (other.TryGetComponent(out EntityBehaviour entity)) {
			if (entity == m_caster) {
				return;
			}
		}

		if (other.TryGetComponent(out IHittable hit)) {
			hit.OnHit(gameObject, m_damage, HitType.Skill);
		}
		if (other.TryGetComponent(out Rigidbody rigidbody)) {
			rigidbody.AddForce(Direction * m_knockbackPower, ForceMode.Force);
		}

	}
}