using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Start_b_script : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        GameManagerEx.Instance.StartIntro();
    }
}