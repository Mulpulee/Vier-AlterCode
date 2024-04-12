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

    public void OnSceneChanged(string pScene)
    {
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
}
