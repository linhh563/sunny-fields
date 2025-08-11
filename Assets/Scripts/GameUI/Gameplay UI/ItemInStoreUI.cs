using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Crafting;
using Management;


namespace GameUI
{
    public class ItemInStoreUI : MonoBehaviour
    {
        private ItemScriptableObject _itemInfo;


        [Header("Buttons")]
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _removeItemButton;
        [SerializeField] private Button _addItemButton;

        [Header("Images & Texts")]
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemPriceText;
        [SerializeField] private TMP_Text _buyQuantityText;
        [SerializeField] private TMP_Text _totalPriceText;

        private int _buyPrice;
        private int _buyQuantity;

        public delegate void BoughtItem(ItemScriptableObject item, int itemQuantity);
        public static event BoughtItem OnItemBought;

        void Start()
        {
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            AddButtonListeners();
        }


        void OnDisable()
        {
            RemoveAllButtonListeners();
        }


        private void UpdateBuyQuantity(int quantity)
        {
            _buyQuantity += quantity;
            _buyQuantityText.SetText(_buyQuantity.ToString());
            UpdateTotalPrice();

            if (_buyQuantity <= 0)
            {
                _buyQuantity = 0;

                if (InventoryManager.gold > _buyPrice)
                    _addItemButton.interactable = true;

                _buyButton.interactable = false;
                _removeItemButton.interactable = false;
                return;
            }
            else if ((InventoryManager.gold - (_buyQuantity * _buyPrice)) < _buyPrice)
            {
                if (_buyQuantity > 0)
                    _removeItemButton.interactable = true;

                _addItemButton.interactable = false;
                return;
            }

            // _addItemButton.interactable = true;
            // _removeItemButton.interactable = true;
            // _buyButton.interactable = true;

            UpdateButtonsInteract(true, true, true);
        }


        private void UpdateTotalPrice()
        {
            int price = _buyPrice * _buyQuantity;

            if (price > 0)
            {
                _totalPriceText.gameObject.SetActive(true);
                _totalPriceText.SetText(price.ToString() + " G");
            }
            else
            {
                _totalPriceText.gameObject.SetActive(false);
            }
        }


        public void InitializeItemUI(ItemScriptableObject item)
        {
            _itemInfo = item;

            _buyPrice = item.buyFromStorePrice;
            _buyQuantity = 0;

            // _removeItemButton.interactable = false;
            // _buyButton.interactable = false;

            UpdateButtonsInteract(false, true, false);

            _itemImage.sprite = item.avatarSprite;
            _itemName.SetText(item.itemName);
            _itemPriceText.SetText(_buyPrice.ToString() + " G");
            _buyQuantityText.SetText("0");

            _totalPriceText.gameObject.SetActive(false);
        }


        private void BuyItem()
        {
            // add the item with responsive quantity to character inventory
            OnItemBought?.Invoke(_itemInfo, _buyQuantity);

            // update character gold
            InventoryManager.gold -= _buyPrice * _buyQuantity;
            GameplayUIManager.Instance.storeUI.UpdateCharacterBudget();

            // reset quantity and price
            _buyQuantity = 0;
            _buyQuantityText.SetText(_buyQuantity.ToString());

            _totalPriceText.gameObject.SetActive(false);
            UpdateButtonsInteract(false, true, false);

            // _removeItemButton.interactable = false;
            // _buyButton.interactable = false;

            UpdateTotalPrice();
        }


        private void UpdateButtonsInteract(bool buyBtnState, bool addItemBtnState, bool removeItemBtnState)
        {
            _buyButton.interactable = buyBtnState;
            _addItemButton.interactable = addItemBtnState;
            _removeItemButton.interactable = removeItemBtnState;
        }


        private void AddButtonListeners()
        {
            _buyButton.onClick.AddListener(BuyItem);

            _removeItemButton.onClick.AddListener(() => UpdateBuyQuantity(-1));
            _addItemButton.onClick.AddListener(() => UpdateBuyQuantity(1));
        }


        private void RemoveAllButtonListeners()
        {
            _buyButton.onClick.RemoveAllListeners();
            _removeItemButton.onClick.RemoveAllListeners();
            _addItemButton.onClick.RemoveAllListeners();
        }


        private void CheckPropertiesValue()
        {
            if (_buyButton == null ||
                _removeItemButton == null ||
                _addItemButton == null ||
                _itemImage == null ||
                _itemName == null ||
                _itemPriceText == null ||
                _buyQuantityText == null ||
                _totalPriceText == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
