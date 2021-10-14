using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace TestSystem
{


    public class Inventory
    {

        private List<Item> itemList;
        public Item[] weaponList;


        public event EventHandler OnItemListChanged;

        public Inventory()
        {
            itemList = new List<Item>();
            weaponList = new Item[2];
            //Debug.LogErrorFormat("lenth {0}", weaponList.Length);





            //AddItem(new Item { itemType = Item.ItemType.material, amount = 5 });
            //AddItem(new Item { itemType = Item.ItemType.weapons, amount = 5 });
            //AddItem(new Item { itemType = Item.ItemType.dish, amount = 5 });


            //Debug.LogError("Inventory");
        }


        public void AddItem(Item item)
        {


            itemList.Add(item);
            Debug.LogError(itemList.Count);
           




            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
      

        public List<Item> GetItemList()
        {
            return itemList;
        }

    }
}
