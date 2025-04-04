using System.Collections.Generic;
using UnityEngine;
using Crafting;
using Management;

namespace Characters
{
    public class CharacterInventory : MonoBehaviour
    {
        private Item _holdingItem;
        private List<Item> _itemsCanUse = new List<Item>();
        private List<Item> _itemsInBag = new List<Item>();

        void Update()
        {
            
        }

        private void SelectItem(Item item)
        {
            var itemIndex = InputManager.Instance.GetItemSelection();

            if (itemIndex == -1)
                return;

            // TODO: swap the parameter item with the holding item
        }

        private void GetItemInBag()
        {
            
        }
    }
}

