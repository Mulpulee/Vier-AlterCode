using System;
using UnityEngine;

namespace Utility.ProjectSetting {
	public static class ProjectSettingManager {
		private static readonly Exception ProjectSettingsLoadingFailed = new Exception("\"ProjectSettings\" Loading Failed..");

		private static ProjectSettingObject m_settings;
		
		public static ProjectSettings Settings {
			get {
				if (m_settings == null) {
					ProjectSettingObject[] settingObjects = Resources.LoadAll<ProjectSettingObject>("");
					
					if (settingObjects.Length > 0) { 
						m_settings = settingObjects[0];
					} else {
						throw ProjectSettingManager.ProjectSettingsLoadingFailed;
					}
				}

				return m_settings.Settings;
			}
		}
	}
}