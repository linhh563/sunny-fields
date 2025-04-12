using System.Collections.Generic;
using UnityEngine;
using Management;
using Crafting;
using UnityEngine.UI;

namespace GameUI
{
    public class ItemBar : MonoBehaviour
    {
        [Header("Game Objects")]
        [SerializeField] private GameObject _holdingItemGameObj;
        [SerializeField] private List<GameObject> _itemGameObjs = new List<GameObject>();

        void Awake()
        {
            // TODO: Optimize game objects retrieval (automatically find all item game objects)
        }

        void Update()
        {
            HandleBarUI();
        }

        public void HandleBarUI()
        {
            if (InputManager.Instance.GetItemSelection() != InputManager.noItemSelected)
            {
                // TODO: swap item ui in item bar and holding item ui 
            }
        }

        public void UpdateItemBar(ItemScriptableObject holdingItem, List<ItemScriptableObject> items)
        {
            var holdingItemImage = _holdingItemGameObj.GetComponent<Image>();
            if (holdingItem.sprite != holdingItemImage.sprite)
            {
                holdingItemImage.sprite = holdingItem.sprite;
            }

            for (int i = 0; i < items.Count; i++)
            {
                var itemImage = _itemGameObjs[i].GetComponent<Image>();
                if (items[i].sprite != itemImage.sprite)
                {
                    itemImage.sprite = items[i].sprite;
                }
            }
        }
    }
}
