using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveDir;
    // Start is called before the first frame update
    void Start()
    {
        GameObject firePosObj= GameObject.Find("FirePos");
        transform.position = firePosObj.transform.position;
        moveDir = firePosObj.transform.lossyScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDir * 20 * Time.deltaTime, 0, 0);
    }
}
