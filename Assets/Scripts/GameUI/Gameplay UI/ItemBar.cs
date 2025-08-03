using UnityEngine;
using Management;
using UnityEngine.UI;

namespace GameUI
{
    public class ItemBar : MonoBehaviour
    {
        [Header("Game Objects")]
        // [SerializeField] private GameObject _holdingItemGameObj;
        // [SerializeField] private List<GameObject> _itemGameObjs = new List<GameObject>();

        [SerializeField] private GameObject[] _itemBarHotkeyObjects;


        public delegate void ItemButtonClicked(int index);
        public static event ItemButtonClicked OnItemButtonClicked;

        void Start()
        {
            // Subscribe input events
            GameplayInputManager.OnItemSelected += RefreshItemBarUI;

            CheckPropertiesValue();
        }

        void OnDisable()
        {
            // Unsubscribe input events
            GameplayInputManager.OnItemSelected -= RefreshItemBarUI;
        }

        public void RefreshItemBarUI()
        {
            var itemIndex = GameplayInputManager.Instance.GetItemIndex();

            if (itemIndex == GameplayInputManager.noItemSelected) return;

            // SwapHoldingItemUI(itemIndex);
        }

        // public void InitializeItemBarUI(ItemScriptableObject holdingItem, List<Item> items)
        // {
        //     // assign image for holding item
        //     var holdingItemImage = _holdingItemGameObj.GetComponent<Image>();
        //     holdingItemImage.sprite = holdingItem.avatarSprite;

        //     // assign image for each item in hot bar
        //     for (int i = 0; i < items.Count; i++)
        //     {
        //         var itemImage = _itemGameObjs[i].GetComponent<Image>();

        //         if (items[i].itemScriptableObject == null || items[i].itemScriptableObject.avatarSprite == itemImage.sprite)
        //             break;

        //         itemImage.sprite = items[i].itemScriptableObject.avatarSprite;
        //     }
        // }

        private void ToggleItemBarHotkey(bool enable)
        {
            foreach (var obj in _itemBarHotkeyObjects)
            {
                obj.SetActive(enable);
            }
        }

        // private void SwapHoldingItemUI(int itemIndex)
        // {
        //     // swap item ui in item bar and holding item ui 
        //     var temp = _holdingItemGameObj.GetComponent<Image>().sprite;
        //     _holdingItemGameObj.GetComponent<Image>().sprite = _itemGameObjs[itemIndex - 1].GetComponent<Image>().sprite;
        //     _itemGameObjs[itemIndex - 1].GetComponent<Image>().sprite = temp;
        // }

        private void CheckPropertiesValue()
        {
            if (_itemBarHotkeyObjects == null)
            {
                Debug.LogError("There is a game object was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
