namespace Utility.DesignPattern.FSM {
	[System.Serializable]
	public abstract class FSMState<T> {
		abstract public void Enter(T target);
		virtual public void Exit(T target) { }

		abstract public void Update(T target);
		virtual public void FixedUpdate(T target) { }
	}
}