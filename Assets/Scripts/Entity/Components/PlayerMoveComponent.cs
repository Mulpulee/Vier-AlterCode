using UnityEngine;
using Utility.Extension;
using Utility.Management;

namespace Entity.Components {
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerMoveComponent : EntityComponent {
		private Vector2 m_movement;
		private Rigidbody m_rigidbody;
		private bool m_isOnGround = true;
		private SpriteRenderer m_sr;

		public Rigidbody RigidBody => m_rigidbody;

		private void Awake() {
			m_rigidbody = GetComponent<Rigidbody>();
			m_sr = GetComponent<SpriteRenderer>();
		}

		public void StartGravity() {
			m_isOnGround = true;
		}

		public void StopGravity() {
			m_isOnGround = false;
		}

		public void Move(Vector2 move) {
			m_movement = move;
		}
		
		public void UpdateMoving() {
			Vector2 move = Entity.Status.Speed * TimeManager.TimeScale * m_movement.normalized;

			m_rigidbody.velocity = new Vector3(move.x, m_rigidbody.velocity.y, 0.0f);
			if (m_isOnGround) {
				m_rigidbody.AddForce(Vector3.down * 1000.0f, ForceMode.Force);
			}

			if (move.x > 0) {
				transform.SetFacing2D(Facing2D.Right);
				m_sr.flipX = false;

			} else if (move.x < 0) {
				transform.SetFacing2D(Facing2D.Left);
				m_sr.flipX = true;
			}
		}

		public void Jump() {
			m_rigidbody.velocity -= Vector3.up * m_rigidbody.velocity.y;
			m_rigidbody.AddForce(Vector3.up * 70.0f, ForceMode.Impulse);
		}
	}
}