using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    private static GameManagerEx instance;
    public static GameManagerEx Instance
    {
        get
        {
            if (instance == null) instance = new GameManagerEx();
            return instance;
        }
    }

    public bool isDialogueOn = false;

    private string m_scene = "Title";

    public void OnSceneChanged(string pScene)
    {
        m_scene = pScene;

        switch (pScene)
        {
            case "Intro":
                ScreenFader.instance.Init(true);
                DialogueManager.Instance.RunDialogue("Dialogue_0002", DialogueStyle.Box);
                break;
            case "Tutorial":
                DialogueManager.Instance.RunDialogue("Floating_Tutorial", DialogueStyle.Floating);
                break;
            case "FirstMapIntro":
                ScreenFader.instance.Init(true);
                DialogueManager.Instance.RunDialogue("Dialogue_0004", DialogueStyle.Box);
                break;
            default:
                break;
        }

        foreach (var item in GameObject.FindObjectsOfType<InteractableObject>())
        {
            switch (item.Name)
            {
                case "Tutorial_Meka": item.Init(() => EndTutorial()); break;
                default:
                    break;
            }
        }
    }

    public void StartIntro()
    {
        ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("IntroScene"));
    }

    public void StartTutorial()
    {
        ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("TutorialScene"));
    }

    public void EndTutorial()
    {
        ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("FirstMapIntroScene"));
    }

    public void StartFirstMap()
    {
        ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("FirstMapScene"));
    }

    public void DialogueEnded()
    {
        isDialogueOn = false;
        switch (m_scene)
        {
            case "Intro": StartTutorial(); break;
            case "FirstMapIntro": StartFirstMap(); break;
            default:
                break;
        }
    }
}
