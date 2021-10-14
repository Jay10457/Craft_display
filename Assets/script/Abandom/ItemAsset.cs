using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestSystem
{

    public class ItemAsset : MonoBehaviour
    {
        public static ItemAsset Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        
        public Sprite materialSprite;
        public Sprite oilSprayGunSprite;
        public Sprite bombSprite;
        public Sprite timePotion;
        public Sprite dishSprite;
        public GameObject oilSprayGunGameObject;
        public GameObject gunBombGameObject;
        public GameObject dishGameObject;

       
    }
}