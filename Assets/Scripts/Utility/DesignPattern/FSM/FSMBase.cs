using System.Collections.Generic;
using System.Xml;
using Utility.Behaviour;
using Utility.Management;

namespace Utility.DesignPattern.FSM {
	public abstract class FSMBase<TEnum, TTarget> : IMonoBehaviour where TEnum : System.Enum where TTarget : class {
		protected Dictionary<TEnum, FSMState<TTarget>> m_stateByEnum;
		protected StateMachine<TTarget> m_stateMachine;
		private TEnum m_nowState;

		public TEnum NowState => m_nowState;

		public virtual void Initialize() {
			BehaviourManager.Instance.Add(this);
			m_stateByEnum = new Dictionary<TEnum, FSMState<TTarget>>();
			m_stateMachine = new StateMachine<TTarget>();
			StateMachineInitialize();
		}
		public void Delete() {
			BehaviourManager.Instance.Remove(this);
			m_stateByEnum.Clear();
			m_stateMachine.Delete();

			m_stateByEnum = null;
			m_stateMachine = null;
		}

		public void Update() {
			m_stateMachine?.Update();
		}
		public void FixedUpdate() {
			m_stateMachine?.FixedUpdate();
		}

		public virtual void ChangeState(TEnum state) {
			m_nowState = state;

			if (m_stateMachine == null) {
				Initialize();
			}

			if (m_stateByEnum.ContainsKey(state)) {
				m_stateMachine.ChangeState(m_stateByEnum[state]);
			}
		}

		abstract protected void StateMachineInitialize();
	}
}