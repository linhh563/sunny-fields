using UnityEngine;

namespace Management.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Clothe")]
    public class ClotheScriptableObject : ScriptableObject
    {
        public string name;
        public ClotheType type;

        public Sprite forwardSprite;
        public Sprite behindSprite;
        public Sprite leftSprite;
        public Sprite rightSprite;

        // TODO: DELETE LEFT AND RIGHT SPRITE, ADD SIDE SPRITE
    }
}