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
            case "Chapter1_1": case "Chapter1_2": case "Chapter1_3": case "Chapter1_4":
                ScreenFader.instance.Init(true); ScreenFader.instance.FadeIn(() => { }); break;
            case "Ending":
                ScreenFader.instance.Init(true); ScreenFader.instance.FadeIn(() => { }); break;
            default:
                break;
        }

        foreach (var item in GameObject.FindObjectsOfType<InteractableObject>())
        {
            switch (item.Name)
            {
                case "Tutorial_Meka": item.Init(() => EndTutorial()); break;
                case "Portal1_1": item.Init(() => OnPortal(1, 1)); break;
                case "Portal1_2": item.Init(() => OnPortal(1, 2)); break;
                case "Portal1_3": item.Init(() => OnPortal(1, 3)); break;
                case "Portal1_4": item.Init(() => OnPortal(1, 4)); break;
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
        ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("Chapter1_1"));
    }

    public void OnPortal(int pChap, int pStage)
    {
        switch (pChap)
        {
            case 1:
                {
                    switch (pStage)
                    {
                        case 1: ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("Chapter1_2")); break;
                        case 2: ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("Chapter1_3")); break;
                        case 3: ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("Chapter1_4")); break;
                        case 4: ScreenFader.instance.FadeOut(() => SceneMover.MoveTo("Ending")); break;
                        default: break;
                    }
                }
                break;
            default: break;
        }
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
