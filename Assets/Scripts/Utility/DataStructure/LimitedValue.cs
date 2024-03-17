using System;
using UnityEngine;

namespace Utility.DataStructure {
	[System.Serializable]
	public struct LimitedValue {
		[SerializeField] private float m_value;
		private Func<float> m_minValue;
		private Func<float> m_maxValue;

		public float Value {
			readonly get => m_value;
			set {
				m_value = Mathf.Clamp(value, MinValue, MaxValue);
			}
		}
		public readonly float MinValue => m_minValue.Invoke();
		public readonly float MaxValue => m_maxValue.Invoke();
		
		public void SetMinValue(float minValue) => m_minValue = () => minValue;
		public void SetMaxValue(float maxValue) => m_maxValue = () => maxValue;

		public void SetMinValue(Func<float> minValue) => m_minValue = minValue;
		public void SetMaxValue(Func<float> maxValue) => m_maxValue = maxValue;

		public LimitedValue(float minValue, float maxValue, float setValue = float.MaxValue) : this(() => minValue, () => maxValue, setValue) { }
		public LimitedValue(Func<float> minValue, Func<float> maxValue, float setValue = float.MinValue) {
			m_minValue = minValue;
			m_maxValue = maxValue;
			m_value = Mathf.Clamp(setValue, minValue.Invoke(), maxValue.Invoke());
		}
	}
}