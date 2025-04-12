using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite sprite;
    }
}