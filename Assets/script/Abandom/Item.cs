using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace TestSystem
{

    
    public class Item
    {
        public enum ItemType
        {
            material,
            dish,
            SprayGun,
            GunBomb,
            Potion
        }
        public ItemType itemType;
        public int amount;

        public object itemtype { get; internal set; }

        public Sprite GetSprite()
        {
            switch (itemType)
            {
                default:
                case ItemType.material:
                    return ItemAsset.Instance.materialSprite;
                case ItemType.SprayGun:
                    return ItemAsset.Instance.oilSprayGunSprite;
                case ItemType.GunBomb:
                    return ItemAsset.Instance.bombSprite;
                case ItemType.dish:
                    return ItemAsset.Instance.dishSprite;
                
            }
        }
        public GameObject GetGameObject()
        {
            switch (itemType)
            {
                default:
                case Item.ItemType.SprayGun:
                    return ItemAsset.Instance.oilSprayGunGameObject;
                case Item.ItemType.GunBomb:
                    return ItemAsset.Instance.gunBombGameObject;
                case Item.ItemType.dish:
                    return ItemAsset.Instance.dishGameObject;
            }
        }

        public bool IsStackable()
        {
            switch (itemType)
            {
                default:
                case ItemType.material:                    
                case ItemType.SprayGun:
                case ItemType.GunBomb:
                case ItemType.Potion:                  
                case ItemType.dish:
                    return false;
            }
        }
    }
}
