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
        public float maxHealth = 1;
        public float health = 1;
        public float minDamage;

        static GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            DropItems();
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            health = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (health < 0)
            {
                DropItems();
                Destroy(gameObject);
            }
        }

        void DropItems()
        {
            for (int i = 0; i < dropAmount; i++)
            {
                //Spawn force and position. Random so they all pop out in different directions
                Vector3 force = new Vector3(Random.Range(-2f, 2f), 2, Random.Range(-2f, 2f));
                ItemPickUp drop = (Instantiate(itemPrefab, transform.position + (force / 4f), Quaternion.identity) as GameObject).GetComponent<ItemPickUp>();
                drop.SetUpPickupable(itemDrop, 5);
                drop.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            }
        }

        public void Damage(float damage)
        {
            health -= damage;
        }
    }
}
