using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ABLoadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "texture.unity3d"));

        string uiPath = Path.Combine(Application.streamingAssetsPath, "ui.unity3d");
        AssetBundle uiAB = AssetBundle.LoadFromFile(uiPath);
        if (uiAB != null)
        {
            Debug.Log("���سɹ���");
            GameObject uiPrefab = uiAB.LoadAsset<GameObject>("UIMenu");
            GameObject uiGo = Instantiate(uiPrefab, transform);
        }
        else
        {
            Debug.Log("����ʧ�ܣ�");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
