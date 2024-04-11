using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DialogueSystem;
using System.Collections.Generic;

public class DialogueBox : DialogueUI
{
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private TMP_Text m_talkLineText;
    [SerializeField] private TMP_Text m_talkLineStretcher;
    [SerializeField] private GameObject m_dialoguePoses;

    private Dictionary<string, Vector2> m_poses;

    private void Awake()
    {
        m_input = -1;

        m_poses = new Dictionary<string, Vector2>();
        foreach (Transform item in m_dialoguePoses.GetComponentInChildren<Transform>())
        {
            m_poses.Add(item.name, item.position);
        }
    }

    public override void BeginPrint()
    {
        m_dialogueBox.SetActive(true);
    }

    public override void EndPrint()
    {
        m_dialogueBox.SetActive(false);
        GameManagerEx.Instance.GoToFirstScene();
    }

    private void Update()
    {
        if (m_printRoutine == null) return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            StopCoroutine(m_printRoutine);
            m_printRoutine = null;
            EndPrint();
        }
    }

    protected override IEnumerator PrintRoutine(Action pNext)
    {
        m_dialogueBox.transform.position = new Vector3(m_poses[m_talker].x, m_poses[m_talker].y, m_dialogueBox.transform.position.z);
        m_talkLineText.text = "";
        m_talkLineStretcher.text = "";
        for (int i = 0; i < m_talkLine.Length; i++)
        {
            if (m_talkLine[i] == '`')
            {
                while (m_talkLine[++i] != '`') ;
                i++;
            }

            m_talkLineStretcher.text += m_talkLine[i];
        }
        m_talkLineStretcher.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();

        if (m_selections == null)
        {
            for (int i = 0; i < m_talkLine.Length; i++)
            {
                if (m_talkLine[i] == '<')
                {
                    while (m_talkLine[i] != '>')
                    {
                        m_talkLineText.text += m_talkLine[i++];
                    }
                }

                if (m_talkLine[i] == '`')
                {
                    switch (m_talkLine[i+1])
                    {
                        case 's':
                            CameraShaker.Instance.ShakeCamera(Int32.Parse(m_talkLine[i + 2].ToString()));
                            break;
                        case 'd':
                            yield return new WaitForSeconds(0.1f * Int32.Parse(m_talkLine[i + 2].ToString()));
                            break;
                        default:
                            break;
                    }
                    while (m_talkLine[++i] != '`') ;
                    i++;
                }

                m_talkLineText.text += m_talkLine[i];
                if (m_talkLine[i] == ' ') continue;
                yield return new WaitForSeconds(0.05f);
            }

            //다 출력하면 스페이스바를 눌러 다음으로
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            pNext.Invoke();
        }
        else
        {
            m_talkLineText.text = m_talkLine;
            for(int i=0;i<m_selections.Length;i++)
            {

            }
            m_selections = null;
        }
    }

}
