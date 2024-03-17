using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using DialogueSystem;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour, IDialogueInput, IDialogueOutput
{
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private TMP_Text m_talkLineText;
    [SerializeField] private TMP_Text m_talkLineStretcher;
    [SerializeField] private GameObject m_dialoguePoses;

    private CanvasGroup m_canvasGroup;

    private String m_talkPos;
    private String m_talkLine;
    private String[] m_selections;
    private Dictionary<string, Vector2> m_poses;

    private Int32 m_lastIndex;
    private Coroutine m_printRoutine;


    private void Awake()
    {
        m_lastIndex = -1;
        m_canvasGroup = GetComponent<CanvasGroup>();

        m_poses = new Dictionary<string, Vector2>();
        foreach (Transform item in m_dialoguePoses.GetComponentInChildren<Transform>())
        {
            m_poses.Add(item.name, item.position);
        }
    }


    public int ReadSelection()
    {
        return m_lastIndex;
    }

    public void WritePos(string pPos)
    {
        m_talkPos = pPos;
    }

    public void WriteLine(string pLine)
    {
        m_talkLine = pLine;
    }

    public void WriteSelections(string[] pSelections)
    {
        m_selections = pSelections;
    }

    public void BeginPrint()
    {
        m_dialogueBox.SetActive(true);
        HideAllButtons();
    }

    public void DoPrint(Action pNext)
    {
        if (m_printRoutine != null)
            StopCoroutine(m_printRoutine);

        m_printRoutine = StartCoroutine(PrintRoutine(pNext));
    }

    public void EndPrint()
    {
        m_dialogueBox.SetActive(false);
    }


    private void HideAllButtons()
    {

    }

    private void OnSelect(Int32 pIndex)
    {
        m_lastIndex = pIndex;
        HideAllButtons();
    }

    private void Update()
    {

    }

    private IEnumerator PrintRoutine(Action pNext)
    {
        m_dialogueBox.transform.position = new Vector3(m_poses[m_talkPos].x, m_poses[m_talkPos].y, m_dialogueBox.transform.position.z);
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
                            yield return new WaitForSeconds(Int32.Parse(m_talkLine[i + 2].ToString()));
                            break;
                        default:
                            break;
                    }
                    while (m_talkLine[++i] != '`') ;
                    i++;
                }

                m_talkLineText.text += m_talkLine[i];
                if (m_talkLine[i] == ' ') continue;
                yield return new WaitForSeconds(0.025f);
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
