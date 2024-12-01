using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

/// <summary>
/// 路径相关操作
/// </summary>
public class PathUtile 
{

    /// <summary>
    /// 将toPath转化为相对于fromPath的相对路径
    /// </summary>
    /// <param name="fromPath">参照路径</param>
    /// <param name="toPath">要转化的路径</param>
    /// <returns></returns>

    public static string MakeRelativePath(string fromPath,string toPath)
    {
        fromPath += "/fake_depth";
        try
        {
            if (string.IsNullOrEmpty(fromPath))
                return toPath;
            if (string.IsNullOrEmpty(toPath))
                return "";

            var fromUri = new System.Uri(fromPath);
            var toUri = new System.Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme)
                return toPath;

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = System.Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath;
        }
        catch
        {
            return toPath;
        }
    }

    public static List<string> GetPathFileNames(string path)
    {
        List<string> fileList = new List<string>();
        string[] arrFile = Directory.GetFiles(path);
        for(int i=0;i<arrFile.Length;i++)
        {
            string filePath = arrFile[i];
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            fileList.Add(fileName);
        }
        return fileList;
    }

}
