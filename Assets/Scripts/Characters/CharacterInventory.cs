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
        [SerializeField] private List<ItemScriptableObject> _itemsCanUse = new List<ItemScriptableObject>();
        // private List<Item> _itemsInBag = new List<Item>();

        void Awake()
        {
            GameplayUIManager.Instance.itemBar.UpdateItemBar(_holdingItem, _itemsCanUse);
        }

        void Update()
        {
            
        }

        private void SelectItem(Item item)
        {
            var itemIndex = InputManager.Instance.GetItemSelection();

            if (itemIndex == InputManager.noItemSelected)
                return;

            // TODO: swap the parameter item with the holding item
        }

        private void GetItemInBag()
        {
            
        }
    }
}

