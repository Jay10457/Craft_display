using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{


    public class Cooker : MonoBehaviour
    {
        [SerializeField] private GameObject cookUI;
        
        
        public static bool isPlayerIn = false;
        private List<Collider> playerColliders = new List<Collider>();


        private void OnTriggerEnter(Collider col)
        {


            if (!playerColliders.Contains(col) && col.gameObject.tag == "Player")
            {
                cookUI.gameObject.SetActive(true);
                playerColliders.Add(col);
                isPlayerIn = true;
                this.transform.GetComponent<SphereCollider>().enabled = false;
                

            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                playerColliders.Remove(col);
                cookUI.gameObject.SetActive(false);
                isPlayerIn = false;
                //Debug.LogError(playerColliders.Count);
            }

        }
    }
}

