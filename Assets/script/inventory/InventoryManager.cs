using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Inventory
{


    public class InventoryManager : MonoBehaviour
    {

        static InventoryManager instance;

        private static List<InventorySlot> slots;
        private static List<Item> items;

        public static Item currentItem { get; private set; }
        public static int currentItemAmount { get; private set; }

        private void Awake()
        {
           
        }
        private void OnEnable()
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


            if (true)
            {

            }
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
            
            if (item == null)
                return;
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;

            //Grab forward direction with slight randomness
            Vector3 random = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            Vector3 direction = player.forward + random;

            //"Throw" item forwards

            //ItemPickUp drop = (Instantiate(currentItem.itemPrefab;//FIX


            //Remove current item
            if (removeCurrentItem)
            {
                currentItem = null;
                currentItemAmount = 0;
            }
        }
        /// <summary>
        /// Swaps the currently held item stack with the item stack in [slot]. Will add to slot stack if items match.
        /// Works with empty slots, or when not holding an item.
        /// </summary>
        /// <param name="slot"></param>
        /// 
        public static void SwapItemWithSlot(InventorySlot slot)
        {
            
            //If items are different, do a complete swap
            if (slot.currentItem != currentItem)
            {
                Debug.LogError("swap");
                Item tempItem = slot.currentItem;
                int tempItemAmount = slot.currentItemAmount;

                //Check slot/item type. eg. don't put head items in torso slot.
                if (slot.CheckItemCompatible(currentItem))
                {
                    slot.SetItemInSlot(currentItem, currentItemAmount);
                    currentItem = tempItem;
                    currentItemAmount = tempItemAmount;
                }
            }
            //If items do match and they aren't both null, add current stack onto slot stack.
            else if (currentItem != null)
            {
                
                int overflow = slot.AddItemToSlot(currentItem, currentItemAmount);

                currentItemAmount = 0;
                if (overflow > 0)
                    currentItemAmount = overflow;

            }
        }
        public static bool CheckItem(Item item, int amount)
        {

            int remaining = amount;

            foreach (InventorySlot slot in slots)
            {
                if (slot.currentItem == item)
                {
                    remaining -= slot.currentItemAmount;
                }

                if (remaining <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes [amount] of [item] from inventory. 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public static void RemoveItemFromInventory(Item item, int amount)
        {
            int remaining = amount;

            foreach (InventorySlot slot in slots)
            {
                if (slot.currentItem == item)
                {
                    if (remaining >= slot.currentItemAmount)
                    {
                        remaining -= slot.currentItemAmount;
                        slot.SetItemInSlot(null, 0);
                    }
                    else
                    {
                        slot.SetItemInSlot(item, slot.currentItemAmount - remaining);
                        remaining = 0;
                    }

                    if (remaining <= 0)
                        return;
                }

            }
        }


        // <summary>
        /// Removes currently held item completely
        /// </summary>
        public static void RemoveItem()
        {
            currentItem = null;
            currentItemAmount = 0;
        }

        public void BinItem()
        {
            RemoveItem();
        }
    }

}

