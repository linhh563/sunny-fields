using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlantItem")]
    public class PlantScriptableObject : ItemScriptableObject
    {
        public int totalPhase;
        public Sprite[] sprites;
        // represent for number of days to grow each phase
        // example: [3, 5, 9]
        // the plant need 3 days from planted to switch to first phase, and it need 5 days from planted to switch to second phase, ...
        // in other words, that plant need 2 days to switch from phase 1 to phase 2
        public int[] grownTime;
        public Sprite productItemAvatar;
    }
}
