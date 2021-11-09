using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
using Inventory;
namespace Tutorial
{


    public class Dish : MonoBehaviour
    {
        [SerializeField] private Item item;
        public static bool isDishDestroy = false;
        private void OnTriggerStay(Collider col)
        {
            if (TutorialSequence.isDishPutAble)
            {
                InventoryManager.RemoveItemFromInventory(item, 1);
                isDishDestroy = true;
            }
        }
    }
}

