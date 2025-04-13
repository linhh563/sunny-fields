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
            var itemIndex = InputManager.Instance.GetItemSelection();
            if (itemIndex != InputManager.noItemSelected)
            {
                // TODO: swap item ui in item bar and holding item ui 
                var temp = _holdingItemGameObj.GetComponent<Image>().sprite;
                _holdingItemGameObj.GetComponent<Image>().sprite = _itemGameObjs[itemIndex - 1].GetComponent<Image>().sprite;
                _itemGameObjs[itemIndex - 1].GetComponent<Image>().sprite = temp;
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
