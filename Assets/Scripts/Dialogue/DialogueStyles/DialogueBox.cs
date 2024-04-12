using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DialogueSystem;
using System.Collections.Generic;
using System.Reflection;

public class DialogueBox : DialogueUI
{
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private TMP_Text m_talkLineText;
    [SerializeField] private TMP_Text m_talkLineStretcher;
    [SerializeField] private GameObject m_dialoguePoses;

    [SerializeField] private GameObject m_selectionBox;

    [SerializeField] private Animation m_screenFader;

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
        GameManagerEx.Instance.isDialogueOn = true;
    }

    public override void EndPrint()
    {
        m_dialogueBox.SetActive(false);
        GameManagerEx.Instance.DialogueEnded();
    }
    
    public override void DoPrint(String pAction, Action pNext)
    {
        if (m_printRoutine != null)
            StopCoroutine(m_printRoutine);

        if (pAction == "FadeIn") m_printRoutine = ScreenFader.instance.FadeIn(pNext);
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
        if (m_selections == null)
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
            m_printRoutine = StartCoroutine(SelectRoutine(pNext));
        }
    }

    private IEnumerator SelectRoutine(Action pNext)
    {
        m_selectionBox.SetActive(true);
        m_selectionBox.transform.position = new Vector3(m_poses[m_talker].x, m_poses[m_talker].y, m_dialogueBox.transform.position.z);

        TMP_Text t = m_selectionBox.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text pointer = m_selectionBox.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        t.text = "";

        foreach (var s in m_selections)
        {
            t.text += $"\t{s}";

            if (!s.Equals(m_selections[m_selections.Length - 1])) t.text += "\n";
        }

        int pointing = 0;
        pointer.text = OnPointerMoved(pointing);

        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pointing--;
                if (pointing < 0) pointing = m_selections.Length - 1;

                pointer.text = OnPointerMoved(pointing);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                pointing++;
                if (pointing > m_selections.Length - 1) pointing = 0;

                pointer.text = OnPointerMoved(pointing);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnInput(pointing);
                m_selectionBox.SetActive(false);
                m_selections = null;
                pNext.Invoke();
                break;
            }
        }
    }

    private string OnPointerMoved(int pIndex)
    {
        string pointer = "";
        for (int i = 0; i < pIndex; i++) pointer += "\n ";
        pointer += ">";
        for (int i = pIndex; i < m_selections.Length - 1; i++) pointer += "\n ";

        return pointer;
    }
}
