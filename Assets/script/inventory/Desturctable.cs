using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Inventory
{
    public class Desturctable : MonoBehaviour
    {
        public Item itemDrop;
        public GameObject itemPrefab;
        public int dropAmount;
        public int dropPerItem;
       

        static GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            DropItems();
           
        }

        // Update is called once per frame
       

        void DropItems()
        {
            for (int i = 0; i < dropAmount; i++)
            {
                //Spawn force and position. Random so they all pop out in different directions
                Vector3 force = new Vector3(Random.Range(-2f, 2f), 2, Random.Range(-2f, 2f));
                ItemPickUp drop = (Instantiate(itemPrefab, transform.position + (force / 4f), Quaternion.identity) as GameObject).GetComponent<ItemPickUp>();
                drop.SetUpPickupable(itemDrop, dropPerItem);
                drop.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            }
        }

     
    }
}
