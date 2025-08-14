using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Crafting;
using TMPro;

namespace GameUI
{
    [RequireComponent(typeof(Image))]
    public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // private properties
        private ItemScriptableObject _itemScriptableObj;
        private Transform _parent;
        private TMP_Text _countText;
        private int _count;

        [SerializeField] private Image _image;

        // public fields
        public Transform parent { get => _parent; set => _parent = value; }
        public ItemScriptableObject itemScriptableObj { get => _itemScriptableObj; set => _itemScriptableObj = value; }
        public int count { get => _count; set => _count = value; }
        

        void Start()
        {
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            _count = 1;
            _countText = GetComponentInChildren<TMP_Text>();
        }


        public void InitializeItem(ItemScriptableObject newItem)
        {
            _itemScriptableObj = newItem;

            _image.sprite = _itemScriptableObj.avatarSprite;
        }


        public void RefreshCount()
        {
            _countText.SetText(_count.ToString());

            // if item's count is 1, hide the count text
            _countText.gameObject.SetActive(!(count == 1));
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _parent = transform.parent;
            _image.raycastTarget = false;
            transform.SetParent(transform.root);

        }


        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            _image.raycastTarget = true;
            transform.SetParent(_parent);
        }


        public void SetCount(int count)
        {
            _count = count;
        }


        private void CheckPropertiesValue()
        {
            if (_image == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
