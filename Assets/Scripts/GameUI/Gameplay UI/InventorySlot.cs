using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUI
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                var inventoryItem = eventData.pointerDrag.GetComponent<DragableItem>();
                inventoryItem.parent = transform;
            }
        }
    }
}
