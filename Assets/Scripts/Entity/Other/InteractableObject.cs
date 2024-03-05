using Entity.Components;
using Entity.Interface;
using UnityEngine;

namespace Entity.Other {
	public class InteractableObject : MonoBehaviour {
		private IInteractable m_interactable;
		private bool m_isAdded = false;

		private void Awake() {
			m_interactable = GetComponent<IInteractable>();
		}

		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out PlayerInteractComponent component)) {
				component.AddInteractable(m_interactable);
				m_isAdded = true;
			}
		}

		private void OnTriggerExit(Collider other) {
			if (other.TryGetComponent(out PlayerInteractComponent component)) {
				component.RemoveInteractable(m_interactable);
				m_isAdded = false;
			}
		}

		private void OnDestroy() {
			if (m_isAdded) {
				PlayerInteractComponent interact = FindObjectOfType<PlayerInteractComponent>(true);
				if (interact == null) {
					return;
				}

				if (m_interactable != null) {
					interact.RemoveInteractable(m_interactable);
				} else {
					interact.RemoveNullInteractables();
				}
			}
		}
	}
}