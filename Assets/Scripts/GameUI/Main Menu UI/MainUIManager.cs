using GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Management
{
    public class MainUIManager : MonoBehaviour
    {
        [SerializeField] private Button _newGameBtn;
        [SerializeField] private Button _loadFarmBtn;
        [SerializeField] private Button _settingBtn;
        [SerializeField] private Button _exitButton;

        private MainMenuUIManager _mainMenuUIManager;


        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();

            CheckPropertiesValue();
        }


        void OnEnable()
        {
            AddButtonsListener();
        }


        void OnDisable()
        {
            _newGameBtn.onClick.RemoveAllListeners();
            _loadFarmBtn.onClick.RemoveAllListeners();
            _settingBtn.onClick.RemoveAllListeners();
        }


        private void PlayButtonPressSfx()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pressButtonSfx);
        }


        private void AddButtonsListener()
        {
            _newGameBtn.onClick.AddListener(_mainMenuUIManager.EnableCustomizeCharacterUI);
            _loadFarmBtn.onClick.AddListener(_mainMenuUIManager.EnableFarmListUI);
            _settingBtn.onClick.AddListener(_mainMenuUIManager.EnableSettingUI);
            _exitButton.onClick.AddListener(MainMenuManager.ExitGame);

            _newGameBtn.onClick.AddListener(PlayButtonPressSfx);
            _loadFarmBtn.onClick.AddListener(PlayButtonPressSfx);
            _settingBtn.onClick.AddListener(PlayButtonPressSfx);
            _exitButton.onClick.AddListener(PlayButtonPressSfx);
        }


        private void CheckPropertiesValue()
        {
            // check value of _mainMenuUIManager
            if (_mainMenuUIManager == null)
            {
                Debug.LogError("Can't get value of Main Menu UI Manager script in " + gameObject.name + ".");
                return;
            }

            // check the serialize properties' value
            if (_newGameBtn == null
            || _loadFarmBtn == null
            || _settingBtn == null
            || _exitButton == null
            )
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
