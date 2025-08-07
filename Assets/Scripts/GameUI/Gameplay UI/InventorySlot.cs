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
            else if (transform.childCount == 1)
            {
                // TODO: swap 2 items
            }
        }
    }
}
