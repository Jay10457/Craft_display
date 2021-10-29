using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Inventory;

namespace Inventory
{



    public class PutMaterial : MonoBehaviour
    {
        [SerializeField] private Button giveButtom;
        [SerializeField] private Text materialAmountText;
        private int materialAmount;

        private event Action giveButtomOnclick;

        private void Awake()
        {
            materialAmount = 5;
        }
        public void GiveMaterial()
        {
            if (materialAmount > 0)
            {
                materialAmount -= 1;
            }
        }
        private void Update()
        {
            materialAmountText.text = string.Format("{0}/5", materialAmount);
        }

    }
}

