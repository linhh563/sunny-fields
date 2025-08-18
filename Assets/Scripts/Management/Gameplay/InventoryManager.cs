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
            ItemInStoreUI.OnItemBought += BuyItems;

            // BagUIController.OnBagOpened += RefreshAllItems;
        }


        void OnDisable()
        {
            GameplayInputManager.OnItemSelected -= ChangeSelectedItem;
            CharacterInteractionDetect.OnCollectItem -= AddItem;

            // TEST
            ItemInStoreUI.OnItemBought -= BuyItems;

            // BagUIController.OnBagOpened -= RefreshAllItems;
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
                    itemInSlot.count < InventoryConstant.MAX_ITEM_STACK_COUNT &&
                    itemInSlot.itemScriptableObj.stackable)
                {
                    itemInSlot.count++;
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

            inventoryItem.InitializeItem(item);
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


        public void SetHoldingItem(ItemScriptableObject item)
        {
            var newItemGameObj = ObjectPoolManager.SpawnObject(inventoryItemPrefab, this.holdingItem.transform);

            // set item scriptable object
            var holdingItem = newItemGameObj.GetComponent<DragableItem>();

            holdingItem.InitializeItem(item);
            holdingItem.RefreshCount();
        }


        public void ConsumeItem()
        {
            if (_holdingItem.transform.childCount == 0) return;

            var holdingItem = _holdingItem.GetComponentInChildren<DragableItem>();
            if (holdingItem == null) return;

            holdingItem.count--;
            if (holdingItem.count == 0)
            {
                Destroy(holdingItem.gameObject);
                return;
            }

            holdingItem.RefreshCount();
        }


        private void BuyItems(ItemScriptableObject item, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                AddItem(item);
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