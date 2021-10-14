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
        static Transform camTransform;

        private void Start()
        {
            if (!camTransform)
                camTransform = Camera.main.transform;
        }

        private void Update()
        {
            transform.forward = -camTransform.forward;
        }

        public void SetUpPickupable(Item item, int amount)
        {
            _item = item;
            itemAmount = amount;
            GetComponent<SpriteRenderer>().sprite = item.itemSprite;
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

