using UnityEngine;

using Management;
using UnityEngine.UI;


namespace GameUI
{
    public class GameplayUIManager : MonoBehaviour
    {
        public static GameplayUIManager Instance;

        private CharacterOptionsUIHandle _characterOptionsUI;

        [SerializeField] private Button _settingButton;

        [SerializeField] private DialogueUIController _conversationUI;
        [SerializeField] private GameObject _bagUI;
        [SerializeField] private StoreUIController _storeUI;
        [SerializeField] private GameplaySettingUI _settingUI;


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
            _settingButton.onClick.AddListener(() => EnableSettingUI(true));
        }


        void OnDisable()
        {
            GameplayInputManager.OnBagKeyPress -= EnableBagUI;
            _settingButton.onClick.RemoveAllListeners();
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


        public void EnableSettingUI(bool enable)
        {
            _settingUI.gameObject.SetActive(enable);
        }


        private void CheckPropertiesValue()
        {
            if (_characterOptionsUI == null ||
                _bagUI == null ||
                _conversationUI == null ||
                _storeUI == null ||
                _settingUI == null ||
                _settingButton == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}

