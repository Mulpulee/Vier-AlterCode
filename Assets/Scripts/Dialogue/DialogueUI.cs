using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DialogueSystem;
using System.Collections.Generic;

public abstract class DialogueUI : MonoBehaviour, IDialogueInput, IDialogueOutput
{
    protected String m_talker;
    protected String m_talkLine;
    protected String[] m_selections;
    protected Int32 m_key = -1;

    protected Int32 m_input;
    protected Coroutine m_printRoutine;

    public virtual int ReadInput()
    {
        return m_input;
    }

    public virtual void WriteTalker(string pTalker)
    {
        m_talker = pTalker;
    }

    public virtual void WriteLine(string pLine)
    {
        m_talkLine = pLine;
    }

    public virtual void WriteSelections(string[] pSelections)
    {
        m_selections = pSelections;
    }

    public virtual void WriteKey(int pKey)
    {
        m_key = pKey;
    }

    public abstract void BeginPrint();
    public abstract void EndPrint();

    public virtual void DoPrint(Action pNext)
    {
        if (m_printRoutine != null)
            StopCoroutine(m_printRoutine);

        m_printRoutine = StartCoroutine(PrintRoutine(pNext));
    }

    protected virtual void OnInput(Int32 pIndex)
    {
        m_input = pIndex;
    }

    protected virtual IEnumerator PrintRoutine(Action pNext)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        pNext.Invoke();
    }

}
