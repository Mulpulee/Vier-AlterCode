using Entity.Interface;
using System;
using System.Collections.Generic;
using Utility.DataStructure;
using Utility.SceneManagement;

namespace Entity.Components {
	public class PlayerInteractComponent : EntityComponent {
		private ListStack<IInteractable> m_interactables;

		public event Action<IEnumerable<IInteractable>> OnInteractableChanged;

		private void Awake() {
			m_interactables = new ListStack<IInteractable>();
			SceneLoader.OnSceneLoaded += Clear;
		}

		private void Clear() {
			m_interactables?.Clear();
			OnInteractableChanged?.Invoke(m_interactables);
		}

		public void InteractWith(int index) {
			if (index >= m_interactables.Count) {
				return;
			}

			m_interactables[index]?.OnInteract();
		}

		public void AddInteractable(IInteractable interactable) {
			if (interactable == null) {
				return;
			}

			m_interactables.Push(interactable);
			OnInteractableChanged?.Invoke(m_interactables);
			interactable.OnInteractEnter();
		}

		public void RemoveInteractable(IInteractable interactable) {
			if (interactable == null) {
				return;
			}

			m_interactables.Remove(interactable);
			OnInteractableChanged?.Invoke(m_interactables);
			interactable.OnInteractExit();
		}

		public void RemoveNullInteractables() => m_interactables.RemoveAll((o) => o == null);
	}
}