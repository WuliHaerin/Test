using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxsColliderCheck : MonoBehaviour
{
    public BoxsColliderCheck targets;
    public float x { get { return transform.position.x; } }
    public float y { get { return transform.position.y; } }
    public Vector2 size { get { return transform.localScale; } }
    public Vector2 conter;
    protected float boxWidth { get { return size.x / 2; } }
    protected float boxHeight { get { return size.y / 2; } }

    public bool ColliderCheck(BoxsColliderCheck target)
    {
        bool isXCollider = x + boxWidth >= target.x - target.boxWidth &&
            target.x + target.boxWidth >= x - boxWidth;
        bool isYColloder = y + boxHeight >= target.y - target.boxHeight &&
            target.y + target.boxHeight >= y - boxHeight;
        if(isXCollider && isXCollider)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            return true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColliderCheck(targets);
    }
}
