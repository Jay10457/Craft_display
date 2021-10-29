using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Inventory
{


    public class ItemPickUp : MonoBehaviour
    {
        Item _item;
        int itemAmount;
      

       

      
        

        public void SetUpPickupable(Item item, int amount)
        {
            _item = item;
            itemAmount = amount;
            //GetComponent<SpriteRenderer>().sprite = item.itemSprite;
            GetComponentInChildren<SpriteRenderer>().sprite = item.itemSprite;
            item.itemPrefab = GetComponent<GameObject>().gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                int remaining = InventoryManager.AddItemToInventory(_item, itemAmount);

                if (remaining > 0)
                {
                    itemAmount = remaining;
                }
                else
                {
                    Destroy(gameObject);
                    Debug.LogError("destroy!");
                }
            }
        }
    }
}

