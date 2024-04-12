using GameSystemManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Action m_action;

    public void Init(Action pAction)
    {
        m_action = pAction;
    }

    private void Update()
    {
        if (GameInputManager.GetKeyDown(InputType.Interact))
        {
            m_action.Invoke();
        }
    }
}
