using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Crafting;

namespace GameUI
{
    [RequireComponent(typeof(Image))]
    public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // Private properties
        private ItemScriptableObject _itemScriptableObj;
        private Transform _parent;
        [SerializeField] private Image image;

        // Public fields
        public Transform parent { get => _parent; set => _parent = value; }
        public ItemScriptableObject itemScriptableObj { get => _itemScriptableObj; set => _itemScriptableObj = value; }

        public void InitializeItem(ItemScriptableObject newItem)
        {
            _itemScriptableObj = newItem;
            image.sprite = newItem.avatarSprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parent = transform.parent;
            image.raycastTarget = false;
            transform.SetParent(transform.root);

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            transform.SetParent(_parent);
        }
    }
}
