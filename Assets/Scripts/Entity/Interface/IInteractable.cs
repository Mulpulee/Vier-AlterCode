namespace Entity.Interface {
	public interface IInteractable {
		public string InteractText => GetInteractText();

		public void OnInteract();
		public void OnInteractEnter();
		public void OnInteractExit();
		public string GetInteractText();
	}
}