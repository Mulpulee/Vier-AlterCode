using System.Collections.Generic;

namespace Utility.ProjectSetting {
	[System.Serializable]
	public class ProjectSettings {
		public string GoogleClientID = "377803181406-dkb5m6k5khhm3moie0n73dhlerkcrar4.apps.googleusercontent.com";
		public string GoogleClientAPI = "AIzaSyDjoUJquckBktvrqPn5bj3aM31pBDxBu1c";
		public string GoogleClientSecret = "pHWEAOULsvGkvgOUqz6I3tbQ9Dg3";

		public string GoogleSpreadSheetID = "";
		public List<string> SheetNames = new();
	}
}