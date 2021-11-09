using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
namespace Tutorial
{


    public class TargetArea : MonoBehaviour
    {
        public static bool isInTargetArea = false;

        private void OnEnable()
        {
            isInTargetArea = false;
        }
        private void OnTriggerStay(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                isInTargetArea = true;
                //Debug.LogError("PlayerIN");
            }
            
        }
        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                isInTargetArea = false;
            }
            
        }
    }
}
