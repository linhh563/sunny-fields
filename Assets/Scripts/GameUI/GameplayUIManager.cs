using UnityEngine;

namespace GameUI
{
    public class GameplayUIManager : MonoBehaviour
    {
        public static GameplayUIManager Instance;

        public ItemBar itemBar { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            itemBar = GetComponentInChildren<ItemBar>();
        }
    }
}

