using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

namespace Inventory
{



    public class Hotbar : MonoBehaviour
    {
        [SerializeField] private Transform equipParent;


        InventorySlot[] slots;
        Transform currentEquip;
        Item lastItem;

        int currentSlotIndex;

        // Start is called before the first frame update
        private void Start()
        {
            //equipParent = GameObject.Find(equipParentName).transform;
            slots = GetComponentsInChildren<InventorySlot>();
            //Debug.LogError(slots);
        }
        private void Update()
        {
            //Scale up currently selected slot
            for (int i = 0; i < slots.Length; i++)
            {
                if (i == currentSlotIndex && slots[i].currentItem != null)
                {
                    slots[i].transform.localScale = Vector3.one * 1.1f;
                    slots[currentSlotIndex].transform.GetChild(1).GetComponent<Image>().enabled = true;
                    
                }


                else
                {
                    slots[i].transform.localScale = Vector3.one;
                    slots[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                }
                    


                
            }
            string input = Input.inputString;
            switch (input)
            {
                case "1":
                    currentSlotIndex = 0;

                    break;
                case "2":
                    currentSlotIndex = 1;
                    break;
                case "3":
                    currentSlotIndex = 2;
                    break;
                default:
                    break;
            }



            //On item change (not called when swapping between slots containing same item)
            if (lastItem != slots[currentSlotIndex].currentItem)
            {
                lastItem = slots[currentSlotIndex].currentItem;

                //Destroy previously equipped item
                if (currentEquip)
                {
                    Destroy(currentEquip.gameObject);
                }
                //Instantiate equip item if currentItem type = Hand
                if (slots[currentSlotIndex].currentItem != null/*&& slots[currentSlotIndex].currentItem.type == Item.Type.Weapons*/)
                {
                    Debug.LogError("eqip");
                    currentEquip = Instantiate(slots[currentSlotIndex].currentItem.equipPrefab, equipParent).transform;
                    currentEquip.localPosition = Vector3.zero;
                }

            }
        }
    }
}
