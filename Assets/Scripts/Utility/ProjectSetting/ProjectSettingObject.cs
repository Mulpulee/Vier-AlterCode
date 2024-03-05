using UnityEditor;
using UnityEngine;
using Utility;

namespace Utility.ProjectSetting {
	[CreateAssetMenu(fileName = "ProjectSettings", menuName = "ProjectSettings", order = 0)]
	public class ProjectSettingObject : ScriptableObject {
		[SerializeField] private ProjectSettings m_settings;

		public ProjectSettings Settings => m_settings;
	}
}