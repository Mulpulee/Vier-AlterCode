using System;
using System.Collections.Generic;

using UnityEngine;
using XLua;

using DialogueSystem;


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager m_instance;
    public static DialogueManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<DialogueManager>();
                if(m_instance == null)
                {
                    GameObject instance = new GameObject("DialogueManager (Singleton)");
                    m_instance = instance.AddComponent<DialogueManager>();
                    DontDestroyOnLoad(instance);
                }
            }
            return m_instance;
        }
    }//æﬂ∏≈ ΩÃ±€≈Ê

    private DialogueUI m_dialogueUI;
    private DialogueMachine m_dialogueMachine;
    private LuaEnv m_dialogueLuaEnv;


    private void Awake()
    {
        m_dialogueLuaEnv = new LuaEnv();
        m_dialogueMachine = new DialogueMachine();
        FindDialogueUI();

        TextAsset dialogueCommon = Resources.Load<TextAsset>("Dialogue/Dialogue_common");
        m_dialogueLuaEnv.DoString(dialogueCommon.text);

        TextAsset[] scripts = Resources.LoadAll<TextAsset>("Dialogue");
        foreach (var script in scripts)
        {
            m_dialogueLuaEnv.DoString(script.text);
        }
    }

    private void FindDialogueUI()
    {
        m_dialogueUI = FindObjectOfType<DialogueUI>();
        m_dialogueMachine.BindInput(m_dialogueUI);
        m_dialogueMachine.BindOutput(m_dialogueUI);
    }

    public void RunDialog(IEnumerator<IDialogueLine> pLines)
    {
        if (m_dialogueUI == null)
            FindDialogueUI();

        m_dialogueMachine.RunDialog(pLines);
    }

    public void RunDialog(String pName)
    {
        var dialogue = m_dialogueLuaEnv.Global.Get<IEnumerator<IDialogueLine>>(pName);
        if(dialogue == null)
        {
            throw new Exception($"Dialogue not found : {pName}");
        }

        RunDialog(dialogue);
    }
}


public static class DialogueLuaTypes
{
    [XLua.CSharpCallLua]
    public static List<Type> Types = new List<Type>
    {
        typeof(IEnumerator<IDialogueLine>)
    };
}
