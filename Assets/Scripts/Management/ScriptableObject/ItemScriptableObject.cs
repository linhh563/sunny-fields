using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(fileName = "newItem",menuName = "ScriptableObjects/Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite avatarSprite;
        public bool stackable;
        public bool canSell;

        [Header("Trading")]
        public int buyFromStorePrice = 0;
        public int sellToStorePrice = 0;
    }
}