using Characters;
using Crafting;
using GameUI;
using UnityEngine;

namespace Management
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private InventorySlot _holdingItem;
        [SerializeField] private InventorySlot[] _inventorySlots;

        public static int gold;
        private GameObject inventoryItemPrefab;

        public InventorySlot[] inventorySlots { get => _inventorySlots; set => _inventorySlots = value; }
        public InventorySlot holdingItem { get => _holdingItem; set => _holdingItem = value; }

        void Start()
        {
            inventoryItemPrefab = Resources.Load<GameObject>("Prefabs/InventoryItem");

            GameplayInputManager.OnItemSelected += ChangeSelectedItem;
            CharacterInteractionDetect.OnCollectItem += AddItem;

            CheckPropertiesValue();

            // TEST
            ItemInStoreUI.OnItemBought += AddItems;
            SellingUIController.OnItemsSold += RemoveItems;
        }


        void OnDisable()
        {
            GameplayInputManager.OnItemSelected -= ChangeSelectedItem;
            CharacterInteractionDetect.OnCollectItem -= AddItem;

            // TEST
            ItemInStoreUI.OnItemBought -= AddItems;
            SellingUIController.OnItemsSold -= RemoveItems;
        }


        private void ChangeSelectedItem()
        {
            var itemIndex = GameplayInputManager.Instance.GetItemIndex();

            if (itemIndex == GameplayInputManager.noItemSelected) return;

            if (_holdingItem.transform.childCount == 0 && _inventorySlots[itemIndex - 1].transform.childCount == 0) return;


            if (_holdingItem.transform.childCount == 0)
            {
                var selectedItem = _inventorySlots[itemIndex - 1].GetComponentInChildren<DragableItem>().gameObject;
                selectedItem.transform.SetParent(_holdingItem.transform);
            }

            else if (_inventorySlots[itemIndex - 1].transform.childCount == 0)
            {
                var holdingItem = _holdingItem.GetComponentInChildren<DragableItem>().gameObject;
                holdingItem.transform.SetParent(_inventorySlots[itemIndex - 1].transform);
            }

            else
            {
                var holdingItem = _holdingItem.GetComponentInChildren<DragableItem>().gameObject;
                var selectedItem = _inventorySlots[itemIndex - 1].GetComponentInChildren<DragableItem>().gameObject;

                holdingItem.SetActive(false);
                selectedItem.transform.SetParent(_holdingItem.transform);
                holdingItem.transform.SetParent(_inventorySlots[itemIndex - 1].transform);
                holdingItem.SetActive(true);
            }
        }


        // return true if inventory is not full and add new item to inventory, otherwise return false
        public bool AddItem(ItemScriptableObject item)
        {
            // check if any slot has the same item with count lower than max
            foreach (var slot in _inventorySlots)
            {
                var itemInSlot = slot.GetComponentInChildren<DragableItem>();

                if (itemInSlot != null &&
                    itemInSlot.itemScriptableObj == item &&
                    itemInSlot.quantity < InventoryConstant.MAX_ITEM_STACK_COUNT &&
                    itemInSlot.itemScriptableObj.stackable)
                {
                    itemInSlot.quantity++;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }

            // find any empty slot
            foreach (var slot in _inventorySlots)
            {
                var itemInSlot = slot.GetComponentInChildren<DragableItem>();

                if (itemInSlot == null)
                {
                    SpawnNewItem(item, slot);
                    return true;
                }
            }

            return false;
        }


        public void AddItemToSlot(ItemScriptableObject item, int slotIndex, int quantity)
        {
            SpawnNewItem(item, inventorySlots[slotIndex]);

            var newItem = inventorySlots[slotIndex].GetComponentInChildren<DragableItem>();

            newItem.SetCount(quantity);
            newItem.RefreshCount();
        }


        private void SpawnNewItem(ItemScriptableObject item, InventorySlot slot)
        {
            var newItemGameObj = ObjectPoolManager.SpawnObject(inventoryItemPrefab, slot.transform);

            // set item scriptable object
            var inventoryItem = newItemGameObj.GetComponent<DragableItem>();

            inventoryItem.InitializeItem(item, 1);
        }


        public ItemScriptableObject GetHoldingItem()
        {
            // if holding item is null or not active, return null
            if (_holdingItem.transform.childCount == 0 ||
                _holdingItem.transform.GetChild(0).gameObject.activeSelf == false)
            {
                return null;
            }

            return _holdingItem.GetComponentInChildren<DragableItem>().itemScriptableObj;
        }


        public void SetHoldingItem(ItemScriptableObject item, int quantity)
        {
            var newItemGameObj = ObjectPoolManager.SpawnObject(inventoryItemPrefab, this.holdingItem.transform);

            // set item scriptable object
            var holdingItem = newItemGameObj.GetComponent<DragableItem>();

            holdingItem.InitializeItem(item, quantity);
            holdingItem.RefreshCount();
        }


        public void ConsumeItem()
        {
            if (_holdingItem.transform.childCount == 0) return;

            var holdingItem = _holdingItem.GetComponentInChildren<DragableItem>();
            if (holdingItem == null) return;

            holdingItem.quantity--;
            if (holdingItem.quantity == 0)
            {
                Destroy(holdingItem.gameObject);
                return;
            }

            holdingItem.RefreshCount();
        }


        private void AddItems(ItemScriptableObject item, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                AddItem(item);
            }
        }


        private void RemoveItems(ItemScriptableObject removedItem, int removedQuantity)
        {
            // check item in holding item
            if (holdingItem.transform.childCount != 0)
            {
                var item = holdingItem.GetComponentInChildren<DragableItem>();
                if (item.itemScriptableObj.itemName == removedItem.itemName)
                {
                    item.quantity -= removedQuantity;
                    if (item.quantity == 0)
                    {
                        Destroy(item.gameObject);
                        return;
                    }

                    item.RefreshCount();
                    return;
                }
            }

            // check item in inventory
            foreach (var itemSlot in inventorySlots)
            {
                if (itemSlot.transform.childCount != 0)
                {
                    var item = itemSlot.GetComponentInChildren<DragableItem>();
                    if (item.itemScriptableObj.itemName == removedItem.itemName)
                    {
                        item.quantity -= removedQuantity;
                        if (item.quantity == 0)
                        {
                            Destroy(item.gameObject);
                            return;
                        }

                        item.RefreshCount();
                        return;
                    }
                }
            }
        }


        public void RefreshAllItems()
        {
            foreach (var itemSlot in _inventorySlots)
            {
                var item = itemSlot.GetComponentInChildren<DragableItem>();
                if (item != null)
                {
                    item.RefreshCount();
                }
            }
        }


        private void CheckPropertiesValue()
        {
            if (_inventorySlots == null)
            {
                Debug.LogError("There is a property is null in " + gameObject.name + ".");
            }

            if (inventoryItemPrefab == null)
            {
                Debug.LogError("Can't load inventory item prefab from resources.");
            }
        }
    }
}