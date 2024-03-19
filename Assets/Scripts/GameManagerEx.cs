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

    public void GoToFirstScene()
    {
        SceneMover.GameLoad();
    }
}
