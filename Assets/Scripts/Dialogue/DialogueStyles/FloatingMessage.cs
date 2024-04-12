using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DialogueSystem;
using System.Collections.Generic;

public class FloatingMessage : DialogueUI
{
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private TMP_Text m_talkLineText;
    [SerializeField] private Image m_talkerProfile;
    [SerializeField] private GameObject m_pressZ;

    private Dictionary<string, Sprite> m_talkerSprites;

    private bool m_isPrinting = false;

    private void Awake()
    {
        m_input = -1;

        m_talkerSprites = new Dictionary<string, Sprite>();
        
        foreach (var sprite in Resources.LoadAll<Sprite>("TalkerSprites"))
        {
            m_talkerSprites.Add(sprite.name, sprite);
        }
    }

    public override void BeginPrint()
    {
        m_dialogueBox.SetActive(true);
        GameManagerEx.Instance.isDialogueOn = true;
    }

    public override void EndPrint()
    {
        m_dialogueBox.SetActive(false);
        GameManagerEx.Instance.DialogueEnded();
    }

    private void Update()
    {
        if (m_printRoutine == null) return;
        
        if (m_isPrinting && Input.GetKeyDown(KeyCode.Z))
        {
            m_isPrinting = false;
        }
    }

    protected override IEnumerator PrintRoutine(Action pNext)
    {
        m_talkerProfile.sprite = m_talkerSprites[m_talker];
        m_talkLineText.text = "";

        if (m_key != -1) m_pressZ.SetActive(false);
        else m_pressZ.SetActive(true);

        m_isPrinting = true;
        for (int i = 0; i < m_talkLine.Length; i++)
        {
            if (m_talkLine[i] == '<')
            {
                while (m_talkLine[i] != '>')
                {
                    m_talkLineText.text += m_talkLine[i++];
                }
            }

            m_talkLineText.text += m_talkLine[i];
            if (m_talkLine[i] == ' ') continue;
            yield return new WaitForSeconds(0.05f);
            if (!m_isPrinting)
            {
                m_talkLineText.text = m_talkLine;
                break;
            }
        }

        if (m_key != -1) yield return new WaitUntil(() => Input.GetKeyDown((KeyCode)m_key));
        else yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        pNext.Invoke();
    }

}
