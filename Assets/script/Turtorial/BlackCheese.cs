using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
namespace Tutorial
{


    public class BlackCheese : MonoBehaviour
    {
        private Animator anim;
        public static bool isStun = false;

        private void OnEnable()
        {
            anim = this.GetComponent<Animator>();
        }
        private void OnTriggerEnter(Collider col)
        {

            
            if (col.gameObject.tag == "Player" && PlayerMovement.isSpriting)
            {
                anim.SetTrigger("Hit");
                isStun = true;
                this.GetComponent<Collider>().enabled = false;
                

            }
        }

    }
}
