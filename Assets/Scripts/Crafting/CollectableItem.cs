using UnityEngine;
using Management.Interface;

namespace Crafting
{
    public class CollectableItem : MonoBehaviour, ICollectable
    {
        public ItemScriptableObject itemScriptableObject;

        public void Collected()
        {

        }
    }
}
