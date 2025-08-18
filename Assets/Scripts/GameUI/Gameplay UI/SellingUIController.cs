using System.Collections.Generic;
using Crafting;
using Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameUI
{
    public class SellingUIController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _selectItemButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _addItemButton;
        [SerializeField] private Button _removeItemButton;

        [Header("Texts")]
        [SerializeField] private TMP_Text _characterBudgetText;
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private TMP_Text _priceText;

        [Header("Images")]
        [SerializeField] private Image _itemsImage;

        [Header("UIs")]
        [SerializeField] private SelectSellingItemUI _selectItemUI;


        private InventoryManager _inventoryManager;
        private DragableItem _itemToBuy;
        private int _itemQuantity;
        private List<DragableItem> _listItem = new List<DragableItem>();


        public delegate void SellItem(ItemScriptableObject item, int quantity);
        public static event SellItem OnItemsSold;


        void Start()
        {            
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            _inventoryManager = FindObjectOfType<InventoryManager>();
            InitializeUI();

            AddListeners();
        }


        void OnDisable()
        {
            RemoveListeners();
        }


        private void InitializeUI()
        {
            _characterBudgetText.SetText(InventoryManager.gold.ToString() + " G");

            _addItemButton.gameObject.SetActive(false);
            _removeItemButton.gameObject.SetActive(false);
            _quantityText.gameObject.SetActive(false);

            _itemsImage.gameObject.SetActive(false);

            _priceText.SetText("0 G");
            _sellButton.interactable = false;

            GetSellItems();
        }


        private void DisableUI()
        {
            _listItem.Clear();
            gameObject.SetActive(false);
        }


        private void GetSellItems()
        {
            _listItem.Clear();

            // get selling item from holding item
            if (_inventoryManager.holdingItem.transform.childCount != 0)
            {
                var item = _inventoryManager.holdingItem.GetComponentInChildren<DragableItem>();
                if (item.itemScriptableObj.canSell)
                    _listItem.Add(item);
                // sellingItems.Add(item);
            }

            // get selling items from inventory
            foreach (var itemSlot in _inventoryManager.inventorySlots)
            {
                if (itemSlot.transform.childCount != 0)
                {
                    var item = itemSlot.GetComponentInChildren<DragableItem>();
                    if (item.itemScriptableObj.canSell)
                        _listItem.Add(item);
                }
            }
        }


        private void EnableSelectItemPanel()
        {
            GetSellItems();

            _selectItemUI.ClearItems();
            _selectItemUI.gameObject.SetActive(true);

            // set up items can sell
            _selectItemUI.InitializeItems(_listItem);
        }


        public void SetSelectItem(DragableItem item)
        {
            _itemToBuy = item;

            _addItemButton.gameObject.SetActive(true);
            _removeItemButton.gameObject.SetActive(true);
            _quantityText.gameObject.SetActive(true);

            _itemsImage.gameObject.SetActive(true);

            _itemsImage.sprite = _itemToBuy.itemScriptableObj.avatarSprite;
            _itemQuantity = 0;
            _quantityText.SetText("0");

            _removeItemButton.interactable = false;
            _addItemButton.interactable = true;
        }


        private void SellItems()
        {
            // update character budget
            InventoryManager.gold += (_itemQuantity * _itemToBuy.itemScriptableObj.sellToStorePrice);
            _characterBudgetText.SetText(InventoryManager.gold + " G");

            // update character inventory
            OnItemsSold?.Invoke(_itemToBuy.itemScriptableObj, _itemQuantity);

            InitializeUI();

            // reset item quantity and price
            // _itemQuantity = 0;
            // _quantityText.SetText("0");
            // _priceText.SetText("0 G");

            // _removeItemButton.interactable = false;
            // _sellButton.interactable = false;
        }


        private void UpdateItemQuantity(int quantity)
        {
            _removeItemButton.interactable = true;
            _addItemButton.interactable = true;
            _sellButton.interactable = true;

            _itemQuantity += quantity;

            if (_itemQuantity <= 0)
            {
                _itemQuantity = 0;
                _sellButton.interactable = false;
                _removeItemButton.interactable = false;
            }

            if (_itemQuantity >= _itemToBuy.quantity)
            {
                _itemQuantity = _itemToBuy.quantity;
                _addItemButton.interactable = false;
            }

            _quantityText.SetText(_itemQuantity.ToString());

            UpdateSellPrice();
        }


        private void UpdateSellPrice()
        {
            int sellPrice = _itemToBuy.itemScriptableObj.sellToStorePrice * _itemQuantity;
            _priceText.SetText(sellPrice.ToString() + " G");
        }


        private void AddListeners()
        {
            _backButton.onClick.AddListener(DisableUI);
            GameplayInputManager.OnExitUIKeyPress += DisableUI;

            _selectItemButton.onClick.AddListener(EnableSelectItemPanel);

            _addItemButton.onClick.AddListener(() => UpdateItemQuantity(1));
            _removeItemButton.onClick.AddListener(() => UpdateItemQuantity(-1));

            _sellButton.onClick.AddListener(SellItems);
        }


        private void RemoveListeners()
        {
            _backButton.onClick.RemoveAllListeners();
            _selectItemButton.onClick.RemoveAllListeners();
            GameplayInputManager.OnExitUIKeyPress -= DisableUI;

            _addItemButton.onClick.RemoveAllListeners();
            _removeItemButton.onClick.RemoveAllListeners();

            _sellButton.onClick.RemoveAllListeners();
        }


        private void CheckPropertiesValue()
        {
            if (_selectItemButton == null ||
                _sellButton == null ||
                _backButton == null ||
                _addItemButton == null ||
                _removeItemButton == null ||
                _priceText == null ||
                _quantityText == null ||
                _characterBudgetText == null ||
                _itemsImage == null ||
                _selectItemUI == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }

            if (_inventoryManager == null)
            {
                Debug.LogError("Can't load Inventory Manager");
            }
        }
    }
}
