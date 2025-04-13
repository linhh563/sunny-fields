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
        [SerializeField] private List<ItemScriptableObject> _itemsInHotBar = new List<ItemScriptableObject>();
        private List<ItemScriptableObject> _itemsInBag = new List<ItemScriptableObject>();

        void Start()
        {
            GameplayUIManager.Instance.itemBar.UpdateItemBar(_holdingItem, _itemsInHotBar);
        }

        void Update()
        {
            SelectItem();
        }

        private void SelectItem()
        {
            var itemIndex = InputManager.Instance.GetItemSelection();

            if (itemIndex == InputManager.noItemSelected)
                return;

            var temp = _holdingItem;
            _holdingItem = _itemsInHotBar[itemIndex - 1];
            _itemsInHotBar[itemIndex - 1] = temp;
        }

        private void GetItemInBag()
        {
            
        }
    }
}

