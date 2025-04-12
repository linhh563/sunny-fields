using UnityEngine;

namespace GameUI
{
    public class GameplayUIManager : MonoBehaviour
    {
        public static GameplayUIManager Instance;

        public ItemBar itemBar { get; private set; }

        void Awake()
        {
            itemBar = GetComponentInChildren<ItemBar>();

            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}

