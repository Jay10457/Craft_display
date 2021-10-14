using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TestSystem
{


    public class UI_Inventory : MonoBehaviour
    {
        private Inventory inventory;
        [SerializeField] private Transform[] WeaponSlots;
        [SerializeField] private Transform materialSlot;
        [SerializeField] private Transform dishSlot;
        [SerializeField] private Transform itemTemplate;
        private float tweenTime = 0.25f;
        private Transform currentTransform;
        private RectTransform rectTransform;
        private bool isFirstItem = false;
        
       

        private void Awake()
        {
            currentTransform = materialSlot;
        }
        private void Update()
        {
            //SelectItem();
        }
        public void SetInventory(Inventory inventory)
        {
            this.inventory = inventory;
            inventory.OnItemListChanged += Inventory_OnItemListChanged;
            RefreshInventoryItem();
        }

        //private void SlotTween()
        //{
        //    LeanTween.cancel(currentTransform.gameObject);
        //    currentTransform.localScale = Vector3.one;
        //    LeanTween.scale(currentTransform.gameObject, Vector3.one * 1.25f, tweenTime).setEaseOutSine();
        //}
        //private void SelectItem()
        //{
        //    string input = Input.inputString;
        //    if (input != "")
        //    {
        //        //Debug.LogError(input);//test
        //    }
            
            
        //    switch (input)
        //    {
        //        case "1":
        //            //Debug.LogError("1");
                    
        //            SelectionColor(dishSlot, weaponSlot1, weaponSlot2);
        //            //SlotTween();
        //            break;
        //        case "2":
        //            //Debug.LogError("2");
        //            SelectionColor(weaponSlot1, dishSlot, weaponSlot2);
        //            //SlotTween();
        //            break;
        //        case "3":
        //            SelectionColor(weaponSlot2, weaponSlot1, dishSlot);
        //            //SlotTween();
        //            break;
                
        //    }

        //    if (weaponSlot1.childCount != 0 && !isFirstItem)
        //    {

        //        Image image = weaponSlot1.GetComponent<Image>();
        //        image.color = Color.green;
        //        isFirstItem = true;
        //    }
        //    else if (weaponSlot2.childCount != 0 && !isFirstItem)
        //    {
        //        Image image = weaponSlot2.GetComponent<Image>();
        //        image.color = Color.green;
        //        isFirstItem = true;
        //    }
        //    else if (dishSlot.childCount != 0 && !isFirstItem)
        //    {
        //        Image image = dishSlot.GetComponent<Image>();
        //        image.color = Color.green;
        //        isFirstItem = true;
        //    }
           
        //}
        private void EquipItem(Item item)
        {
            
                switch (item.itemType)
                {
                    case Item.ItemType.material:
                        break;
                    case Item.ItemType.dish:
                        Debug.LogError("dish");
                        break;
                    case Item.ItemType.SprayGun:
                        Debug.LogError("weapon");
                        break;
                    default:
                        break;
                }
            

        }
        private void SelectionColor(Transform transform1, Transform transform2, Transform transform3)
        {
           
            currentTransform = transform1;             
            Image image = currentTransform.GetComponent<Image>();
            if (transform1.childCount != 0)
            {
                
                image.color = Color.green;
                transform2.GetComponent<Image>().color = Color.white;
                transform3.GetComponent<Image>().color = Color.white;
                //Debug.LogError(currentTransform);
            }





        }
        private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
        {
            RefreshInventoryItem();
        }
       
        private void CheckWeaponSlot()
        {

        }
       
        private void RefreshInventoryItem()
        {
            
            
            foreach (Item item in inventory.GetItemList())
            {
                //Debug.LogError(item.itemType);
                
                switch (item.itemType)
                {
                    case Item.ItemType.material:
                        currentTransform = materialSlot;
                        Refresh(item);
                        break;
                    case Item.ItemType.dish:
                        currentTransform = dishSlot;
                        Refresh(item);
                        break;
                   
                    
                    default:
                        currentTransform = null;
                        break;
                        
                }
                              
                

            }
        }
        private void RefreshWeapon()
        {
            Debug.LogError("refresh!");
        }
        private void Refresh(Item item)
        {
            Debug.LogError("refresh!");
            if (currentTransform.childCount == 0)
            {
                //Debug.LogError(currentTransform.gameObject.name);
                rectTransform = Instantiate(itemTemplate, currentTransform).GetComponent<RectTransform>();
                Image image = rectTransform.GetComponent<Image>();
                image.sprite = item.GetSprite();

            }
            TMP_Text uiText = rectTransform.Find("Text").GetComponent<TMP_Text>();
            if (item.amount >= 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
        }
        
    }
}
