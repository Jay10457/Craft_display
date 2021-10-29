using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Inventory;
namespace Inventory
{


    public class InventorySlot : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
    {
        public Item currentItem { get; protected set; }
        public int currentItemAmount { get; protected set; }
        [Tooltip("Whether or not to include in the inventory")]
        public bool includeInInventory = true;
        [Tooltip("What type of items this slot can hold")]
        public Item.Type type;
        public bool interactable = true;
        
        [SerializeField] protected Image itemImage;
        [SerializeField] protected Image borderImage;
        [SerializeField] protected Image stackImage;
        [SerializeField] protected Text stackText;
        [SerializeField] protected GameObject tooltipPrefab;
        protected bool mouseOver;
        
        protected GameObject tooltipInstance;

       
        private void Update()
        {
            if (currentItemAmount <= 0)
            {
                currentItem = null;
            }
            SetUI();

            if (mouseOver)
            {
                MouseOverChecks();
            }
            if (currentItem == null && tooltipInstance != null)
            {
                Destroy(tooltipInstance);
            }       
        }

        /// <summary>
        /// Called in Update() while mouseOver == true. All mouse button funcitonality goes here.
        /// </summary>    
        protected virtual void MouseOverChecks()
        {
            Debug.LogError("mouse over check");
            if (!interactable) return;

            if (Input.GetMouseButtonDown(0))
            {
                //InventoryManager.SwapItemWithSlot(this);//TODO:
            }
            if (Input.GetMouseButtonDown(1))
            {
                //Grab item from slot if slot contains an item
            }

        }

        /// <summary>
        /// Called in Update(). Sets all UI elements for the slot.
        /// </summary>
        
        protected virtual void SetUI()// Handle Slot Item UI
        {
            if (currentItem)
            {
                //Slot UI
                itemImage.sprite = currentItem.itemSprite;
                itemImage.color = Color.white;
                stackImage.color = currentItem.itemBorderColor;
                borderImage.color = currentItem.itemBorderColor;

                stackImage.gameObject.SetActive(currentItemAmount > 1);
                stackText.text = currentItemAmount.ToString();


                //Create/ Destroy Tooltip
                if (mouseOver && !tooltipInstance)
                {
                    //Debug.LogError(currentItem.tooltips);
                    tooltipInstance = Instantiate(tooltipPrefab);
                    //Debug.LogError(tooltipPrefab);
                    
                    tooltipInstance.GetComponent<Tooltip>().SetTooltip(currentItem.tooltips, transform.position + Vector3.up * 30);
                    
                }
                else if (!mouseOver && tooltipInstance != null)
                {
                    Destroy(tooltipInstance);
                }
            }
            else
            {
                //Slot UI
                itemImage.sprite = null;
                stackImage.color = Color.white;
                itemImage.color = Color.clear;
                stackImage.gameObject.SetActive(false);
            }
           
        }

        public void OnPointerEnter(PointerEventData pointeventData)
        {
            mouseOver = true;
        }

        public void OnPointerExit(PointerEventData pointeventData)
        {
            mouseOver = false;
        }

        public int AddItemToSlot(Item item, int amoumt)
        {
            Debug.LogError("addItemToSlot");
            //Early exit if clicking on empty slots with no item.
            if (!currentItem && !item) 
                return 0;
            
            //Check item type for equipment slots
            if (type != item.type) return amoumt;
            //Add to stack if items are the same : TODO:can't pickup
            if (currentItem == item)
            {
                currentItemAmount += amoumt;
                //Debug.LogError("Add to stack if items are the same");
            }
            //Replace stack if items are different
            else
            {
                //Debug.LogError("Replace stack if items are different");
                currentItem = item;
                currentItemAmount = amoumt;
            }
            //Overflow management
            int overflow = currentItemAmount - currentItem.stackLimit;
            if (overflow > 0)
                currentItemAmount = currentItem.stackLimit;
            if (currentItemAmount == 0)
                currentItem = null;

            return overflow;

        }
        public void SetItemInSlot(Item item, int amount)
        {
            currentItem = item;
            currentItemAmount = amount;
        }
        public bool CheckItemCompatible(Item item)
        {
            return (item == null|| type == item.type);
        }
        private void OnDisable()
        {
            mouseOver = false;
            if (tooltipInstance)
                Destroy(tooltipInstance);

            
        }
    }
}
