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
        public static bool isDishDestroy;
        public static bool isDishInArea;

        private void OnEnable()
        {
            isDishInArea = false;
            isDishDestroy = false;
        }
        private void Update()
        {
            DestroyDish();
        }

        private void DestroyDish()
        {
            if (TutorialSequence.isDishPutAble && TargetArea.isInTargetArea)
            {
                InventoryManager.RemoveItemFromInventory(item, 1);
                isDishDestroy = true;
                //Debug.LogError("destroy");
            }
        }
    }
}
