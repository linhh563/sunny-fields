using System.Collections.Generic;
using UnityEngine;
using Management;

namespace GameUI
{
    public class ItemBar : MonoBehaviour
    {
        private Sprite holdingItemSprite;
        private List<Sprite> itemSprites;

        void Update()
        {
            HandleBarUI();
        }

        public void HandleBarUI()
        {
            if (InputManager.Instance.GetItemSelection() != -1)
            {
                // TODO: swap item ui in item bar and holding item ui 
            }
        }
    }
}
