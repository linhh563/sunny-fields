using UnityEngine;
using Management.ScriptableObjects;
using Management;

namespace GameUI
{
    public class GameplayUIManager : MonoBehaviour
    {
        public static GameplayUIManager Instance;
        private CharacterOptionsUIHandle _characterOptionsUI;
        [SerializeField] private DialogueUIController _dialogueUI;
        [SerializeField] private GameObject _bagUI;

        public ItemBar _itemBar { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _itemBar = GetComponentInChildren<ItemBar>();
            _characterOptionsUI = GetComponentInChildren<CharacterOptionsUIHandle>();

            // _dialogueUI.gameObject.SetActive(false);
        }

        void Start()
        {
            CheckPropertiesValue();

            GameplayInputManager.OnBagKeyPress += EnableBagUI;
        }

        void OnDisable()
        {
            GameplayInputManager.OnBagKeyPress -= EnableBagUI;
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

        public void PlayNextDialogue(ConversationScriptableObject conversation)
        {
            _dialogueUI.PlayNextDialogue(conversation);

            // hide item bar when character is in conversation
            if (_dialogueUI.gameObject.activeSelf)
            {
                _itemBar.gameObject.SetActive(false);
                _characterOptionsUI.DisableOptionsUI();
            }
            else
            {
                _itemBar.gameObject.SetActive(true);
            }
        }

        private void EnableBagUI()
        {
            _bagUI.gameObject.SetActive(true);
        }

        private void CheckPropertiesValue()
        {
            if (_characterOptionsUI == null || _bagUI == null)
            {
                Debug.LogError("There is a game object was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}

