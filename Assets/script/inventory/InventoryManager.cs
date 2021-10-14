using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Inventory
{


    public class InventoryManager : MonoBehaviour
    {
        [Tooltip("Item UI follows cursor ")]
        [SerializeField] private Image currentItemImage;
        [Tooltip("Item stack text UI that follows cursor")]
        public Text currentItemStackDisplay;

        static InventoryManager instance;

        private static List<InventorySlot> slots;
        private static List<Item> items;

        public static Item currentItem { get; private set; }
        public static int currentItemAmount { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //Debug.LogError("instance!");
            }

            slots = new List<InventorySlot>();
            items = new List<Item>();

            foreach (InventorySlot slot in GetComponentsInChildren<InventorySlot>())
            {
                if (slot.includeInInventory)
                    slots.Add(slot);
            }
            Debug.LogError(slots.Count);
            
        }

        private void Update()
        {
            //Set current item to null if stack is 0
            if (currentItemAmount <= 0)
                currentItem = null;

            if (currentItem)
            {
                //Set current held Item UI
                currentItemImage.enabled = true;
                currentItemImage.sprite = currentItem.itemSprite;
                currentItemStackDisplay.text = currentItemAmount.ToString();
                currentItemStackDisplay.gameObject.SetActive(true);

                //If clicking off of inventory UI, drop item
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    DropItem(currentItem, currentItemAmount);
                }
            }
            else
            {
                //Clear current held UI
                currentItemImage.enabled = false;
                currentItemStackDisplay.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// Add [amount] of [item] to inventory. Automatically checks slots of matching items and stack limits.
        /// Returns 0 if done successfully, otherwise if inventory is full returns leftover of item.
        /// </summary>
        /// <param name="item">What item to add</param>
        /// <param name="amount">How many of the item to add</param>
        /// <returns></returns>
        
        public static int AddItemToInventory(Item item, int amount)
        {
            int remaining = amount;

            items.Add(item);

            Debug.LogError(items.Count);

            //check slots that contain same item
            foreach (InventorySlot slot in slots)
            {
                if (slot.currentItem == item)
                {
                    Debug.LogError(slot.currentItem);
                    int overflow = slot.AddItemToSlot(item, remaining);

                    //add as many to the slot as we can without overflowing
                    if (overflow > 0) remaining = overflow;
                    else remaining = 0;
                    

                }
            }
            if (remaining <= 0)
            {
                return 0;
            }
            //check empty slots
            foreach (InventorySlot slot in slots)
            {
                if (slot.currentItem == null)
                {
                    remaining = slot.AddItemToSlot(item, remaining);
                }
                if (remaining <= 0)
                {
                    break;
                }
            }


            return remaining;
        }
        /// <summary>
        /// Physically drop specified item and place it in front of the player
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <param name="removeCurrentItem"></param>

        public static void DropItem(Item item, int amount, bool removeCurrentItem = true)
        {
            items.Remove(item);
            if (item == null)
                return;

        }
        /// <summary>
        /// Swaps the currently held item stack with the item stack in [slot]. Will add to slot stack if items match.
        /// Works with empty slots, or when not holding an item.
        /// </summary>
        /// <param name="slot"></param>
        /// 
        public static void SwapItemWithSlot(InventorySlot slot)
        {

        }




    }
}
