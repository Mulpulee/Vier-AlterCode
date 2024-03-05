using System.IO;

namespace Utility.Extension.IO {
	public static class DirectoryUtility {
		public static void CreateOrNothing(string directory) {
			var directoryInfo = new DirectoryInfo(directory);
			if (!directoryInfo.Exists) {
				directoryInfo.Create();
			}
		}
	}
}