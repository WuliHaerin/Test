using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleTool 
{
    [MenuItem("MyTool/BuildAssetBundle")]
    static void BuildAssetBundle()
    {
        Debug.Log("³É¹¦");
        string outputPath = Application.streamingAssetsPath;
        if(!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
