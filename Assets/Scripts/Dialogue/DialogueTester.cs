using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    private void Start()
    {
        DialogueManager.Instance.RunDialog("Dialogue_0002");
    }
}
