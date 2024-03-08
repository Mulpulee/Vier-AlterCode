namespace Utility.Behaviour {
	public interface IMonoBehaviour {
		public void Update();
		public virtual void FixedUpdate() { }
	}
}