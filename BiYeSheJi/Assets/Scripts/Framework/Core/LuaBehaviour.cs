using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaBehaviour : MonoBehaviour
{
    [HideInInspector]
    public string LuaScriptPath = "";

    // private List<IntParam> IntArgs = new List<IntParam>();
    // private List<GameObjectParam> goArgs = new List<GameObjectParam>();

    [CSharpCallLua]
    public interface ILuaWrapper
    {
        public void onAwake();
        public void onStart();
        public void onEnable();
        public void onDisable();
        public void onDestroy();
        public void onUpdate(float deltaTime);

        public void setArg(string key, object value);
    }

    private ILuaWrapper m_LuaWrapper;

    private delegate ILuaWrapper LuaObjectNew();

    void Awake()
    {
        LuaEngine.instance.Init();

        // ��ʼ��Lua�ļ�
        InitLua();
        // ����LuaObj��onAwake����
        if (m_LuaWrapper != null)
        {
            m_LuaWrapper.onAwake();
        }
    }

    void InitLua()
    {
        // ��ʼ��Lua�ļ���
        // 1������LuaScriptPath��Ӧ��Lua��Ķ���LuaObj
        LuaEnv luaEnv = LuaEngine.instance.luaEnv;
        string luaTxt = string.Format("return require \"{0}\"", LuaScriptPath);
        object[] ret = luaEnv.DoString(luaTxt);
        LuaTable luaClass = ret[0] as LuaTable;

        // 2������ǰGameObject��һЩ����������õ�LuaObj��
        // LuaObjectNew luaNewAction = luaEnv.Global.GetInPath<LuaObjectNew>("LuaBehaviour.new");
        LuaObjectNew luaNewAction = luaClass.Get<LuaObjectNew>("new");
        m_LuaWrapper = luaNewAction();
        m_LuaWrapper.setArg("go", gameObject);
        m_LuaWrapper.setArg("transform", transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_LuaWrapper != null)
        {
            m_LuaWrapper.onStart();
        }
    }

    void OnEnable()
    {
        if (m_LuaWrapper != null)
        {
            m_LuaWrapper.onEnable();
        }
    }

    void OnDisable()
    {
        if (m_LuaWrapper != null)
        {
            m_LuaWrapper.onDisable();
        }
    }

    void OnDestroy()
    {
        if (m_LuaWrapper != null)
        {
            m_LuaWrapper.onDestroy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_LuaWrapper != null)
        {
            m_LuaWrapper.onUpdate(Time.deltaTime);
        }
    }
}
