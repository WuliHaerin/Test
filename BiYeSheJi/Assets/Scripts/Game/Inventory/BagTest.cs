using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagTest : MonoBehaviour 
{
    //Start is called before the first frame update
    void Start()
    {
        CheckCross();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Item item = new Item();
            int num = Random.Range(1, 5);
            switch (num)
            {
                case 1:
                    item = TableManager.gunTable.GetDataByID(2001);
                    break;
                case 2:
                    item = TableManager.gunTable.GetDataByID(2002);
                    break;
                case 3:
                    item = TableManager.armorTable.GetDataByID(4001);
                    break;
                case 4:
                    item = TableManager.armorTable.GetDataByID(4002);
                    break;
                default:
                    break;
            }
            Inventory.instance.StoreItem(item);
            //Destroy(this.gameObject.GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckCross()
    {

    }
}
