using UnityEngine;

namespace Quest {
	[System.Serializable]
	public struct QuestData {
		[SerializeField] private int m_questID;

		public int QuestID {
			readonly get => m_questID;
			set => m_questID = value;
		}
	}
}