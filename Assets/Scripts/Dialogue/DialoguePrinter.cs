using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePrinter : MonoBehaviour
{
    [SerializeField] private string m_script;
    [SerializeField] private DialogueStyle m_style;

    private void Start()
    {
        DialogueManager.Instance.RunDialogue(m_script, m_style);
    }
}
