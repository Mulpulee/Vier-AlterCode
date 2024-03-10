using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Quit_b_script : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
