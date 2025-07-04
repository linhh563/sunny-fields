using UnityEngine;

namespace GameUI
{
    public class GameplayUIManager : MonoBehaviour
    {
        public static GameplayUIManager Instance;
        private CharacterOptionsUIHandle _characterOptionsUI;

        public ItemBar itemBar { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            itemBar = GetComponentInChildren<ItemBar>();
            _characterOptionsUI = GetComponentInChildren<CharacterOptionsUIHandle>();
        }

        public void EnableCharacterOptionsUI(bool enable, bool isLeft)
        {
            if (!enable)
            {
                _characterOptionsUI.DisableOptionsUI();
                return;
            }
                
            _characterOptionsUI.EnableOptionsUI(isLeft);
        }
    }
}

