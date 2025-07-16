using System.Collections.Generic;
using UnityEngine;
using System;

using Crafting;
using Management;
using GameUI;
using UnityEngine.TextCore.Text;

namespace Characters
{
    public class CharacterInventory : MonoBehaviour
    {
        // TODO: delete SerializeField
        [SerializeField] private ItemScriptableObject _holdingItem;
        // TODO: delete SerializeField
        [SerializeField] private List<ItemScriptableObject> _itemsInHotBar = new List<ItemScriptableObject>();
        private List<ItemScriptableObject> _itemsInBag = new List<ItemScriptableObject>();

        void Start()
        {
            GameplayUIManager.Instance._itemBar.InitializeItemBarUI(_holdingItem, _itemsInHotBar);

            // Subscribe input events
            GameplayInputManager.OnItemSelected += SelectItem;
        }

        void OnDisable()
        {
            // Unsubscribe input events
            GameplayInputManager.OnItemSelected -= SelectItem;
        }

        private void SelectItem()
        {
            var itemIndex = GameplayInputManager.Instance.GetItemIndex();

            if (itemIndex == GameplayInputManager.noItemSelected)
                return;

            var temp = _holdingItem;
            _holdingItem = _itemsInHotBar[itemIndex - 1];
            _itemsInHotBar[itemIndex - 1] = temp;
        }

        private void GetItemInBag()
        {

        }

        public ItemScriptableObject GetHoldingItem()
        {
            return _holdingItem;
        }
    }
}

