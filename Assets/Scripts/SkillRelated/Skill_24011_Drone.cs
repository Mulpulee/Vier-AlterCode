using Entity;
using Entity.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utility.Management;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpriteRenderer))]
public class Skill_24011_Drone : MonoBehaviour {
	[Header("Debug")]
	[SerializeField] private Rigidbody m_rigidbody;
	[SerializeField] private LayerMask m_targetLayer;
	[SerializeField] private float m_damage;
	[SerializeField] private float m_senseRange;
	[SerializeField] private float m_power;
	[SerializeField] private float m_removeMinSpeed;
	[SerializeField] private EntityBehaviour m_caster;

	private void Awake() {
		m_targetLayer = 1 << LayerMask.NameToLayer("Enemy");

		if (TryGetComponent(out BoxCollider collider)) {
			collider.isTrigger = true;
		}
		m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.freezeRotation = true;
		m_rigidbody.useGravity = false;

		Collider[] colliders = new Collider[10];
		int count = Physics.OverlapSphereNonAlloc(transform.position, m_senseRange, colliders, m_targetLayer);

		Collider nearest = GetNearestCollider(colliders, count);

		if (nearest == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 force = (nearest.transform.position - transform.position).normalized * m_power;
		m_rigidbody.AddForce(force, ForceMode.Impulse);
	}

	private void Update() {
		if (m_rigidbody.velocity.magnitude <= m_removeMinSpeed) {
			Destroy(gameObject);
		}
	}

	private Collider GetNearestCollider(Collider[] coliders, int count) {
		if (count == 0) {
			return null;
		}

		float minDistance = Vector3.Distance(transform.position, coliders[0].transform.position);
		int index = 0;

		for (int i = 0; i < count; i++) {
			float distance = Vector3.Distance(transform.position, coliders[i].transform.position);
			if (minDistance > distance) {
				index = i;
				minDistance = distance;
			}
		}

		return coliders[index];
	}

	public void SetDamage(float damage) => m_damage = damage;
	public void SetSenseRange(float range) => m_senseRange = range;
	public void SetMovePower(float power) => m_power = power;
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

		if (other.TryGetComponent(out IHittable hit)) {
			hit.OnHit(gameObject, m_damage, HitType.Skill);
		}
		
	}
}