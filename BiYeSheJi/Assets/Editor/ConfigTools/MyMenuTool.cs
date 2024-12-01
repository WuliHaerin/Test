using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyMenuTool
{
    [MenuItem("MyTool/配置表工具", false, 20)]
    static void ShowConfigWindow()
    {
        // Debug.Log("MenuOne is click!");
        ConfigToolWnd window = EditorWindow.GetWindow<ConfigToolWnd>("配置表工具窗口");
        window.Show();
    }
}
