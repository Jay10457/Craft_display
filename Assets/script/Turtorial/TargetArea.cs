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
        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                isInTargetArea = true;
            }
        }
    }
}
