using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using XLua;

public class LuaEngine : SingleTonMono<LuaEngine>
{
    private static string m_LuaRootFolder=null;
    public static string luaRootFolder
    {
        get
        {
            if(string.IsNullOrEmpty(m_LuaRootFolder))
            {
                m_LuaRootFolder = Path.Combine(Application.dataPath, "LuaScripts");
            }
            return m_LuaRootFolder;
        }
    }
    private LuaEnv m_LuaEnv = null;
    public LuaEnv luaEnv
    {
        get
        {
            if(m_LuaEnv==null)
            {
                m_LuaEnv = new LuaEnv();
            }
            return m_LuaEnv;
        }
    }

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        luaEnv.AddLoader(CustomLuaLoader);
    }

    public void Init()
    {
        DoString("require 'Game.Main'");
    }

    public byte[] CustomLuaLoader(ref string filePath)
    {
//#if UNITY_EDITOR
        string luaPath = filePath;
        if (luaPath.EndsWith(".lua"))
        {
            luaPath = luaPath.Replace(".lua", "");
        }
        luaPath = luaPath.Replace(".", "/");
        luaPath += ".lua";

        luaPath = Path.Combine(luaRootFolder, luaPath);
        string luaScript = File.ReadAllText(luaPath);
        filePath = luaPath;
        return System.Text.Encoding.UTF8.GetBytes(luaScript);
//#else
        //到AssetBundle加载lua文件
//#endif
    }


    public object[] DoString(string chunk)
    {
        return luaEnv.DoString(chunk);
    }
    public object[] DoString(byte[] chunk)
    {
        return luaEnv.DoString(chunk);
    }
}
