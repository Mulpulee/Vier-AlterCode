using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameSystemManager;
using Entity.Other;

namespace Entity.Components {
	public class InputLock {
		public bool LockVelocity { get; }
		public string DebugString { get; }

		public InputLock(bool pLockVelocity = false, [CallerMemberName] string memberName = "",
			[CallerFilePath] string sourceFilePath = "",
			[CallerLineNumber] int sourceLineNumber = 0) {
			LockVelocity = pLockVelocity;

			DebugString = $"{sourceFilePath}/{memberName}:{sourceLineNumber}";
		}
	}

	public class PlayerInputComponent : EntityComponent {
		private PlayerMoveComponent m_moveComponent;
		private PlayerSkillComponent m_skillComponent;
		private PlayerInteractComponent m_interactComponent;
		private GroundChecker m_groundCheck;
        private PlayerAnimationController m_anim;

        private HashSet<InputLock> m_inputLocks;
		private bool m_isLocked;

		public IEnumerable<InputLock> Locks => m_inputLocks;
		public bool IsLocked => m_isLocked;

		private void Awake() {
			m_inputLocks = new HashSet<InputLock>();

			m_moveComponent = GetComponent<PlayerMoveComponent>();
			m_skillComponent = GetComponent<PlayerSkillComponent>();
			m_interactComponent = GetComponent<PlayerInteractComponent>();
			m_groundCheck = GetComponent<GroundChecker>();
			m_anim = GetComponent<PlayerAnimationController>();
		}

		private void UpdateLockState() {
			Rigidbody rigidbody = m_moveComponent.RigidBody;
			
			foreach (InputLock item in m_inputLocks) {
				if (item.LockVelocity) {
					rigidbody.constraints = RigidbodyConstraints.FreezeAll;
					rigidbody.useGravity = false;
					return;
				}
			}

			rigidbody.constraints = RigidbodyConstraints.FreezeAll ^ RigidbodyConstraints.FreezePositionX ^ RigidbodyConstraints.FreezePositionY;
		}
		public void LockInput(InputLock item, bool isClearVelocity = false) {
			if (isClearVelocity) {
				m_moveComponent.RigidBody.velocity = Vector3.zero;
			}

			m_inputLocks.Add(item);
			m_isLocked = true;

			UpdateLockState();
		}
		public void UnLockInput(InputLock item) {
			m_inputLocks.Remove(item);
			m_isLocked = m_inputLocks.Any();

			UpdateLockState();
		}

		private void Update() {
			if (m_isLocked) {
				return;
			}

			if (Entity.Status.IsBind) {
				m_moveComponent.Move(Vector2.zero);
				return;
			}

			// Move
			Vector2 move = Vector2.zero;

			if (GameInputManager.GetKey(InputType.MoveLeft)) {
				move.x -= 1;
			}
			if (GameInputManager.GetKey(InputType.MoveRight)) {
				move.x += 1;
            }
			move.Normalize();
			m_moveComponent.Move(move);

			// Jump
			bool isGround = m_groundCheck.IsGround();

			if (!isGround) {
				m_moveComponent.StopGravity();
            }

            if (!GameInputManager.GetKey(InputType.Jump) && isGround) {
				m_moveComponent.StartGravity();
			}

			if (GameInputManager.GetKeyDown(InputType.Jump) && isGround) {
				m_moveComponent.StopGravity();
				m_moveComponent.Jump();
				m_anim.IsJumping = true;
				m_anim.Jump = true;
			}

			// Skill
			int start = (int)SkillSlot.Slot1;
			int end = start + (int)SkillSlot.Count;
			for (int i = start; i < end; i++) {
				SkillSlot slot = (SkillSlot)i;

				if (GameInputManager.GetKeyDown(slot)) {
					if (i == start) m_skillComponent.TryCast(slot);
                    else m_anim.Attack = m_skillComponent.TryCast(slot);
                }
			}

			// Interaction
			if (GameInputManager.GetKeyDown(InputType.Interact)) {
				m_interactComponent.InteractWith(0);
			}
		}

		private void FixedUpdate() {
			if (m_isLocked) {
				return;
			}

			m_moveComponent.UpdateMoving();
		}
	}
}