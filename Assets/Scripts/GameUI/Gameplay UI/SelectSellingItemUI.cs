using System.Collections.Generic;
using Crafting;
using Management;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace GameUI
{
    public class SelectSellingItemUI : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private List<GameObject> _items;

        private GameObject _itemInSellUI;


        void Start()
        {
            _itemInSellUI = Resources.Load<GameObject>("Prefabs/ItemInSellUI");
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            _backButton.onClick.AddListener(DisableUI);
            GameplayInputManager.OnExitUIKeyPress += DisableUI;
        }


        void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
            GameplayInputManager.OnExitUIKeyPress -= DisableUI;
        }


        public void InitializeItems(List<DragableItem> sellingItems)
        {
            // foreach (var item in sellingItems)
            // {
            //     GameObject sellingItem = ObjectPoolManager.SpawnObject(_itemInSellUI, _itemsContainer);
            //     sellingItem.GetComponent<Image>().sprite = item.avatarSprite;
            //     sellingItem.GetComponent<Button>().onClick.AddListener(() => SelectItem(sellingItems.IndexOf(item)));

            //     _items.Add(sellingItem);
            // }

            for (int i = 0; i < sellingItems.Count; i++)
            {
                var item = sellingItems[i];

                _items[i].gameObject.SetActive(true);

                var itemAvatar = _items[i].transform.GetChild(0).GetComponent<Image>();
                itemAvatar.sprite = item.itemScriptableObj.avatarSprite;

                _items[i].gameObject.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
            }
        }


        private void SelectItem(DragableItem item)
        {
            GetComponentInParent<SellingUIController>().SetSelectItem(item);
            DisableUI();
        }


        private void DisableUI()
        {
            ClearItems();
            gameObject.SetActive(false);
        }


        public void ClearItems()
        {
            // clear items list
            foreach (var item in _items)
            {
                item.gameObject.SetActive(false);
            }
        }


        private void CheckPropertiesValue()
        {
            if (_backButton == null ||
                _itemsContainer == null ||
                _items.Count == 0)
            {
                Debug.LogError("There is a component was not assign in " + gameObject.name + ".");
            }

            if (_itemInSellUI == null)
            {
                Debug.LogError("Can't load Item In Sell UI Prefab");
            }
        }
    }
}
