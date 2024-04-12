using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedSceneChecker : MonoBehaviour
{
    [SerializeField] private string m_scene;

    private void Start()
    {
        GameManagerEx.Instance.OnSceneChanged(m_scene);
    }
}
