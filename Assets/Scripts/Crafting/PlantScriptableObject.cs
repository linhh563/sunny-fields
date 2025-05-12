using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlantItem")]
    public class PlantScriptableObject : ItemScriptableObject
    {
        public Sprite phase_1_Sprite;
        public Sprite phase_2_Sprite;
        public Sprite phase_3_Sprite;
        public Sprite product;


        public int daysToPhase2;
        public int daysToPhase3;
    }
}
