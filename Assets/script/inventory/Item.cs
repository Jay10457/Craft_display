using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [Tooltip("String that appears in tooltips.")]
        public string tooltips;
        [Tooltip("stack Limit")]
        public int stackLimit = 5;
        public Sprite itemSprite;
        public Type type;
        [Tooltip("Color of the item slot border.")]
        public Color itemBorderColor = new Color(1, 1, 1, 1);
        [Tooltip("If this is an equipable item, this is what GameObject will spawn when held/equipped.")]
        public GameObject equipPrefab;


        public enum Type
        {
            All,
            material,
            dish,
            Weapons
        }
    }

   
}
