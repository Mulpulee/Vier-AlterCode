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
    private EnemyBuilder m_enemy;

    public void Init(Action pAction)
    {
        m_action = pAction;
        m_enemy = FindObjectOfType<EnemyBuilder>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || !m_enemy.IsEnemyZero) return;

        if (GameInputManager.GetKeyDown(InputType.Interact))
        {
            m_action.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManagerEx.Instance.isDialogueOn || !m_enemy.IsEnemyZero) return;

        if (!other.CompareTag("Player")) return;

        m_graphic.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (GameManagerEx.Instance.isDialogueOn || !m_enemy.IsEnemyZero) return;

        if (!other.CompareTag("Player")) return;

        m_graphic.SetActive(false);
    }
}
