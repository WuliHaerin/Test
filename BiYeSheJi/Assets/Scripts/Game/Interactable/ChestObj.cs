using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObj : Interactable
{
    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public override void InteractLogic()
    {
        Debug.Log("ÄãºÃ");
    }
}
