using DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaRunner : MonoBehaviour
{
    [SerializeField] private TextAsset m_luaScript;
    private LuaEnv m_luaEnv;

    private void Awake()
    {
        m_luaEnv = new LuaEnv();
    }
    private void RunLua()
    {
        //lua ½ÇÇà
        m_luaEnv.DoString(m_luaScript.text);
    }
}


