using System.Collections.Generic;
using UnityEngine;

using Crafting;
using Management;
using GameUI;

namespace Characters
{
    public class CharacterInventory : MonoBehaviour
    {
        // TODO: delete SerializeField
        [SerializeField] private ItemScriptableObject _holdingItem;
        // TODO: delete SerializeField
        [SerializeField] private List<Item> _itemsInHotBar = new List<Item>();
        private List<ItemScriptableObject> _itemsInBag = new List<ItemScriptableObject>();

        void Start()
        {
            GameplayUIManager.Instance._itemBar.InitializeItemBarUI(_holdingItem, _itemsInHotBar);

            // Subscribe input events
            GameplayInputManager.OnItemSelected += SelectItem;
            ItemBar.OnItemButtonClicked += SelectItem;
        }

        void OnDisable()
        {
            // Unsubscribe input events
            GameplayInputManager.OnItemSelected -= SelectItem;
            ItemBar.OnItemButtonClicked -= SelectItem;
        }


        // select item by press hotkey
        private void SelectItem()
        {
            var itemIndex = GameplayInputManager.Instance.GetItemIndex();

            if (itemIndex == GameplayInputManager.noItemSelected) return;

            // swap item in hot bar and holding item
            var temp = _holdingItem;
            _holdingItem = _itemsInHotBar[itemIndex - 1].itemScriptableObject;
            _itemsInHotBar[itemIndex - 1].itemScriptableObject = temp;
        }

        // select item by click button
        private void SelectItem(int index)
        {
            // swap item in hot bar and holding item
            var temp = _holdingItem;
            _holdingItem = _itemsInHotBar[index - 1].itemScriptableObject;
            _itemsInHotBar[index - 1].itemScriptableObject = temp;
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

