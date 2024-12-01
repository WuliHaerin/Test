using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

/// <summary>
/// ·����ز���
/// </summary>
public class PathUtile 
{

    /// <summary>
    /// ��toPathת��Ϊ�����fromPath�����·��
    /// </summary>
    /// <param name="fromPath">����·��</param>
    /// <param name="toPath">Ҫת����·��</param>
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
