using UnityEngine;

using Management;


namespace GameUI
{
    public class GameplayUIManager : MonoBehaviour
    {
        public static GameplayUIManager Instance;
        
        private CharacterOptionsUIHandle _characterOptionsUI;
        [SerializeField] private DialogueUIController _conversationUI;
        [SerializeField] private GameObject _bagUI;
        [SerializeField] private StoreUIController _storeUI;


        public ItemBar _itemBar { get; private set; }
        public DialogueUIController conversationUI { get => _conversationUI; set => _conversationUI = value; }
        public StoreUIController storeUI { get => _storeUI; set => _storeUI = value; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _itemBar = GetComponentInChildren<ItemBar>();
            _characterOptionsUI = GetComponentInChildren<CharacterOptionsUIHandle>();

            _conversationUI.gameObject.SetActive(false);
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


        public void EnableConversationUI(bool enable)
        {
            _conversationUI.gameObject.SetActive(enable);
        }


        private void EnableBagUI()
        {
            _bagUI.gameObject.SetActive(true);
        }


        public void EnableStoreUI(bool enable)
        {
            _storeUI.gameObject.SetActive(enable);
        }


        private void CheckPropertiesValue()
        {
            if (_characterOptionsUI == null ||
                _bagUI == null ||
                _conversationUI == null ||
                _storeUI == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}

