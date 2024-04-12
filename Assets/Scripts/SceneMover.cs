using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.SceneManagement;

public class SceneMover : MonoBehaviour {
	public static void MoveTo(string pScene) {
		SceneLoader.LoadScene(pScene);
	}

	public static void GameExit() {
		Application.Quit();
	}
}
