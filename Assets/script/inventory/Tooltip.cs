using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory
{


    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private Transform tooltipHolder;
        [SerializeField] private TMP_Text tooltipText;


        public void SetTooltip(string text, Vector3 position)
        {
            tooltipHolder.position = position;
            tooltipText.text = text;
        }
    }

}
