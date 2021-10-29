using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
namespace Inventory
{


    [CreateAssetMenu(fileName = "Material List", menuName = "Craft/Material")]
    public class Material : ScriptableObject
    {
        [SerializeField] private Sprite friutSprite;
        private int materialIndex;

        

        
        public enum MaterialType
        {
            fruit,
            nut,
            vegetable,
            meat,
            egg,
            frymeat,
            potato,
            flour,
            cheese,
            cream

        }
    }
}
