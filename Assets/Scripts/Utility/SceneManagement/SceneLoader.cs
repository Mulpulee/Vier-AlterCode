using System;
using UnityEngine.SceneManagement;

namespace Utility.SceneManagement {
	public static class SceneLoader {
		public static event Action OnSceneLoaded;

		public static void LoadScene(string sceneName) {
			OnSceneLoaded?.Invoke();

			LoadingScene.LoadScene(sceneName);
		}

		public static void ReloadScene() {
			Scene nowScene = SceneManager.GetActiveScene();
			string nowSceneName = nowScene.name;

			SceneManager.LoadScene(nowSceneName);
		}
	}
}