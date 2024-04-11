using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.SceneManagement;

public class SceneMover : MonoBehaviour {
	public static void GameLoad() {
		SceneLoader.LoadScene("TutorialScene");
	}

	public static void GameExit() {
		Application.Quit();
	}
}
