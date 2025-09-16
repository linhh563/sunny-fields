using Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameUI
{
    public class StoreUIController : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _storeName;
        [SerializeField] private TMP_Text _budgetText;

        [SerializeField] private Transform _itemsContainer;

        public Transform itemsContainer { get => _itemsContainer; set => _itemsContainer = value; }

        void Start()
        {
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            UpdateCharacterBudget();

            // subscribe events
            GameplayInputManager.OnExitUIKeyPress += DisableUI;
            _backButton.onClick.AddListener(DisableUI);
            _backButton.onClick.AddListener(PlayButtonPressSfx);

            // pause game
        }


        void OnDisable()
        {
            GameplayInputManager.OnExitUIKeyPress -= DisableUI;
            _backButton.onClick.RemoveAllListeners();

            // resume game
        }


        private void DisableUI()
        {
            gameObject.SetActive(false);
        }


        public void UpdateCharacterBudget()
        {
            _budgetText.SetText(InventoryManager.gold.ToString() + " G");
        }


        public void SetStoreName(string name)
        {
            _storeName.SetText(name);
        }


        private void PlayButtonPressSfx()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pressButtonSfx);
        }


        private void CheckPropertiesValue()
        {
            if (_backButton == null ||
                _storeName == null ||
                _budgetText == null ||
                _itemsContainer == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
