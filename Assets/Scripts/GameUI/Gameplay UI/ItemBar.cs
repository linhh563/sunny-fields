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

        void Start()
        {
            // Subscribe input events
            GameplayInputManager.OnItemSelected += RefreshItemBarUI;
        }

        void OnDisable()
        {
            // Unsubscribe input events
            GameplayInputManager.OnItemSelected -= RefreshItemBarUI;
        }

        public void RefreshItemBarUI()
        {
            var itemIndex = GameplayInputManager.Instance.GetItemIndex();

            if (itemIndex == GameplayInputManager.noItemSelected) return;
            
            // swap item ui in item bar and holding item ui 
            var temp = _holdingItemGameObj.GetComponent<Image>().sprite;
            _holdingItemGameObj.GetComponent<Image>().sprite = _itemGameObjs[itemIndex - 1].GetComponent<Image>().sprite;
            _itemGameObjs[itemIndex - 1].GetComponent<Image>().sprite = temp;
        }

        public void InitializeItemBarUI(ItemScriptableObject holdingItem, List<ItemScriptableObject> items)
        {
            var holdingItemImage = _holdingItemGameObj.GetComponent<Image>();
            if (holdingItem.avatarSprite != holdingItemImage.sprite)
            {
                holdingItemImage.sprite = holdingItem.avatarSprite;
            }

            for (int i = 0; i < items.Count; i++)
            {
                var itemImage = _itemGameObjs[i].GetComponent<Image>();
                if (items[i].avatarSprite != itemImage.sprite)
                {
                    itemImage.sprite = items[i].avatarSprite;
                }
            }
        }
    }
}
