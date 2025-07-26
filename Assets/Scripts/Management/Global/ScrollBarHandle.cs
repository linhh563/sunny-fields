using UnityEngine;
using UnityEngine.UI;

namespace Management
{
    public class ScrollBarHandle : MonoBehaviour
    {
        [SerializeField] GameObject scrollBar;
        [SerializeField] GameObject itemList;
        // the capacity of the scroll view when the scroll bar hade
        [SerializeField] int itemsCapacity;

        void Update()
        {
            ToggleScrollBar();
        }

        private void ToggleScrollBar()
        {
            if (itemList.transform.childCount > itemsCapacity)
            {
                scrollBar.SetActive(true);
            }
            else
            {
                scrollBar.SetActive(false);
            }
        }
    }
}

