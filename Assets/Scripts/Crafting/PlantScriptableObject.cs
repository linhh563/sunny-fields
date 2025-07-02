using UnityEngine;
using UnityEngine.Tilemaps;

namespace Crafting
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlantItem")]
    public class PlantScriptableObject : ItemScriptableObject
    {
        public TileBase[] grownTile;
        public int[] grownTime;
        public Sprite product;
    }
}
