using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isPlayerIn;
    protected GameObject tip;

    public virtual void Awake()
    {
        tip = transform.Find("Tip").gameObject;
    }

    public void Interact()
    {
        if(isPlayerIn)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                InteractLogic();
            }
        }
    }

    public virtual void InteractLogic()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            isPlayerIn = true;
            tip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerIn = false;
            tip.SetActive(false);
        }
    }


}
