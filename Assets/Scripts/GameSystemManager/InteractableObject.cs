using GameSystemManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string Name;

    [SerializeField] private GameObject m_graphic;

    private Action m_action;

    public void Init(Action pAction)
    {
        m_action = pAction;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameInputManager.GetKeyDown(InputType.Interact))
        {
            m_action.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManagerEx.Instance.isDialogueOn) return;

        if (!other.CompareTag("Player")) return;

        m_graphic.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (GameManagerEx.Instance.isDialogueOn) return;

        if (!other.CompareTag("Player")) return;

        m_graphic.SetActive(false);
    }
}
