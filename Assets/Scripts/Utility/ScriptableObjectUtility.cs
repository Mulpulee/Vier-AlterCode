using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utility {
	public static class ScriptableObjectUtility {
		public static void CreateAsset<T>(string name, string path = null) where T : ScriptableObject {
#if UNITY_EDITOR
			T asset = ScriptableObject.CreateInstance<T>();
			
			if (path == null) {
				Object selection = Selection.activeObject;
				string assetPath = AssetDatabase.GetAssetPath(selection);

				path = assetPath;

				if (path == "") {
					path = "Assets";
				} else if (Path.GetExtension(path) != "") {
					string filename = Path.GetFileName(assetPath);

					path = path.Replace(filename, "");
				}

				if (!Directory.Exists(path)) {
					Directory.CreateDirectory(path);
				}

				string filePath = Path.Combine(path, $"{name}.asset");
				string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath(filePath);

				AssetDatabase.CreateAsset(asset, uniqueAssetPath);

				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = asset;
#endif
			}
		}
	}
}