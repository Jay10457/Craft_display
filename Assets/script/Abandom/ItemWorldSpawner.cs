using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestSystem
{


    public class ItemWorldSpawner : MonoBehaviour
    {
        public Item item;

        private void Awake()
        {
            
            //ItemWorld.SpawnItemWorld(transform.position, item);
            //Destroy(gameObject);
        }
        private void Start()
        {
            ItemWorld.SpawnItemWorld(new Vector3(5, 2, 0), new Item { itemType = Item.ItemType.SprayGun, amount = 5 });
            ItemWorld.SpawnItemWorld(new Vector3(0, 2, 0), new Item { itemType = Item.ItemType.SprayGun, amount = 5 });
            ItemWorld.SpawnItemWorld(new Vector3(3, 2, 0), new Item { itemType = Item.ItemType.GunBomb, amount = 5 });
            ItemWorld.SpawnItemWorld(new Vector3(-5, 2, 0), new Item { itemType = Item.ItemType.dish, amount = 1 });
            ItemWorld.SpawnItemWorld(new Vector3(10, 2, 0), new Item { itemType = Item.ItemType.dish, amount = 1 });
        }
    }
    
}