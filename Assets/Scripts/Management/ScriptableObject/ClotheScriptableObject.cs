using UnityEngine;

namespace Management.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Clothe")]
    public class ClotheScriptableObject : ScriptableObject
    {
        public string name;
        public ClotheType type;
        public Sprite sprite;
    }
}