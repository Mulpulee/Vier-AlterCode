using UnityEngine;

namespace Utility.Management {
	public class TimeManager : IndestructibleSingleton<TimeManager> {
		[SerializeField] private bool m_isTimeStopped = false;
		[SerializeField] private float m_timeScale = 1.0f;

		public static bool IsTimeStopped => Instance.m_isTimeStopped;
		public static float DeltaTime => Time.deltaTime * Instance.m_timeScale;
		public static float TimeScale => Instance.m_timeScale;

		public void SetTimeScale(float timeScale) {
			m_timeScale = timeScale;
		}

		public void SetTime(bool isTimeStop) {
			m_isTimeStopped = isTimeStop;
		}
		public void PlayTime() {
			m_isTimeStopped = false;
		}
		public void StopTime() {
			m_isTimeStopped = true;
		}
		public void ToggleTime() {
			m_isTimeStopped = !m_isTimeStopped;
		}

		protected override void OnInstantiated() { }
	}
}