namespace Utility.DesignPattern.FSM {
	public class StateMachine<T> where T : class {
		private T m_target;
		private FSMState<T> m_currentState;

		public void Delete() {
			m_target = null;
			m_currentState = null;
		}

		public void ChangeState(FSMState<T> newState) {
			if (m_currentState == newState) {
				return;
			}

			m_currentState?.Exit(m_target);

			m_currentState = newState;

			m_currentState?.Enter(m_target);
		}

		public void Initialize(T initTarget, FSMState<T> initState) {
			m_target = initTarget;
			ChangeState(initState);
		}

		public void Update() {
			m_currentState?.Update(m_target);
		}
		public void FixedUpdate() {
			m_currentState?.FixedUpdate(m_target);
		}
	}
}