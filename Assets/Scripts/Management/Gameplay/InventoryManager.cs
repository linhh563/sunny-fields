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
        private GameObject inventoryItemPrefab;

        void Start()
        {
            inventoryItemPrefab = Resources.Load<GameObject>("Prefabs/InventoryItem");

            GameplayInputManager.OnItemSelected += ChangeSelectedItem;
            CharacterInteractionDetect.OnCollectItem += AddItem;

            CheckPropertiesValue();
        }


        void OnDisable()
        {
            GameplayInputManager.OnItemSelected -= ChangeSelectedItem;
            CharacterInteractionDetect.OnCollectItem -= AddItem;
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


        public void ConsumeItem()
        {
            if (_holdingItem.transform.childCount == 0) return;

            var holdingItem = _holdingItem.GetComponentInChildren<DragableItem>();
            if (holdingItem == null) return;

            holdingItem.count--;
            holdingItem.RefreshCount();

            if (holdingItem.count == 0)
            {
                Destroy(holdingItem.gameObject);
            }
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

        private void SpawnNewItem(ItemScriptableObject item, InventorySlot slot)
        {
            var newItemGameObj = ObjectPoolManager.SpawnObject(inventoryItemPrefab, slot.transform);

            // set item scriptable object
            var inventoryItem = newItemGameObj.GetComponent<DragableItem>();
            // inventoryItem.itemScriptableObj = item;
            inventoryItem.InitializeItem(item);
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