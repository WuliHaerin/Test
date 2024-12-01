using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ConfigToolWnd : EditorWindow
{
    private ConfigTool m_ConfigTool;

    /// <summary>
    /// OnEnable
    /// </summary>
    void OnEnable()
    {
        string configToolPath = "Assets/Editor/Database/ConfigTool.asset";
        if (File.Exists(configToolPath))
        {
            m_ConfigTool = AssetDatabase.LoadAssetAtPath<ConfigTool>(configToolPath);
        }
        else
        {
            m_ConfigTool = ScriptableObject.CreateInstance<ConfigTool>();
            AssetDatabase.CreateAsset(m_ConfigTool, configToolPath);
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.Label("���ñ�·��:", GUILayout.Width(100));
        EditorGUILayout.SelectableLabel(m_ConfigTool.excelPath, "textfield",
                UnityEngine.GUILayout.Height(17));
        if (GUILayout.Button("...", GUILayout.Width(30)))
        {
            string path = EditorUtility.OpenFolderPanel("��ѡ�����ñ�·��", Application.dataPath, "");
            // Application.dataPath:assets�ļ���·��
            m_ConfigTool.excelPath = PathUtile.MakeRelativePath(Application.dataPath, path);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.Label("��������·��:", GUILayout.Width(100));
        EditorGUILayout.SelectableLabel(m_ConfigTool.codePath, "textfield",
               UnityEngine.GUILayout.Height(17));
        if (GUILayout.Button("...", GUILayout.Width(30)))
        {
            string path = EditorUtility.OpenFolderPanel("��ѡ�����ɴ���·��", Application.dataPath, "");
            m_ConfigTool.codePath = PathUtile.MakeRelativePath(Application.dataPath, path);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        // GUILayout.BeginHorizontal();
        // GUILayout.Label("Ext·��:", GUILayout.Width(100));
        // // EditorGUILayout.SelectableLabel(m_ConfigTool.extRelativePath, "textfield",
        // //         UnityEngine.GUILayout.Height(17));
        // if (GUILayout.Button("...", GUILayout.Width(30)))
        // {
        //     string path = EditorUtility.OpenFolderPanel("��ѡ����չ����·��", Application.dataPath, "");
        //     // m_ConfigTool.extRelativePath = PathUtils.MakeRelativePath(Application.dataPath, path);
        // }
        // GUILayout.EndHorizontal();
        GUILayout.Space(5);
        if (GUILayout.Button("����"))
        {
            m_ConfigTool.Generate();
        }

        GUILayout.Space(5);
        if (GUILayout.Button("��������"))
        {
            m_ConfigTool.Save();
        }

        GUILayout.EndVertical();
    }
}
