using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyMenuTool
{
    [MenuItem("MyTool/���ñ���", false, 20)]
    static void ShowConfigWindow()
    {
        // Debug.Log("MenuOne is click!");
        ConfigToolWnd window = EditorWindow.GetWindow<ConfigToolWnd>("���ñ��ߴ���");
        window.Show();
    }
}
