using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utility.SceneManagement {
	public class LoadingScene : MonoBehaviour {
		public static readonly string LoadingSceneName = $"LoadingScene";

		public static string NextSceneName => m_nextSceneName;
		private static string m_nextSceneName = "";

		[SerializeField] private Image m_fadeImage;

		public static void SetNextScene(string name) => m_nextSceneName = name;
		public static void LoadScene() => LoadScene(m_nextSceneName);
		public static void LoadScene(string name) {
			SetNextScene(name);

			if (ExsitScene(LoadingSceneName)) {
				SceneManager.LoadScene(LoadingSceneName);
			} else {
				SceneManager.LoadScene(m_nextSceneName);
			}

		}
		public static bool ExsitScene(string name) {
			Scene loadingScene = SceneManager.GetSceneByName(name);
			return loadingScene.buildIndex != -1;
		}

		private void Start() {
			StartCoroutine(LoadAsyncScene());
		}

		IEnumerator LoadAsyncScene() {
			AsyncOperation operation = SceneManager.LoadSceneAsync(m_nextSceneName);
			operation.allowSceneActivation = false;

			while (!operation.isDone) {
				float lerp = Mathf.Lerp(m_fadeImage.fillAmount, operation.progress, 0.5f);
				m_fadeImage.fillAmount = lerp;

				if (operation.progress >= 0.9f) {
					OnLoad(operation);
					yield break;
				}

				yield return null;
			}
		}

		private void OnLoad(AsyncOperation operation) {
			operation.allowSceneActivation = true;
		}
	}
}