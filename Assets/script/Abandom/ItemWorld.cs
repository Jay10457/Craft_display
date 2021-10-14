using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TestSystem
{


    public class ItemWorld : MonoBehaviour
    {

        
        public static ItemWorld SpawnItemWorld(Vector3 position , Item item)
        {
            Transform transform = Instantiate(item.GetGameObject().transform, position, Quaternion.identity);
            

            ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
            
            itemWorld.SetItem(item);
            
           
            return itemWorld;
        }



        private Item item;
        
        

        public void SetItem(Item item)
        {
            this.item = item;
            
        }
        public Item GetItem()
        {
            
            return item;
        }
        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        

       

       


    }
}
