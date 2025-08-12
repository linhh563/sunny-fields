using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Management;

namespace GameUI
{
    public class SettingUIManager : MonoBehaviour
    {
        [SerializeField] protected Button _backBtn;
        [SerializeField] protected TMP_Dropdown _languageDropdown;
        [SerializeField] protected Slider _bgmSlider;
        [SerializeField] protected Slider _sfxSlider;
        [SerializeField] protected List<Button> _hotkeys;
        [SerializeField] protected ModifyHotKeyUI _modifyHotKeyUI;

        private MainMenuUIManager _mainMenuUIManager;


        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();

            CheckPropertiesValue();
        }


        void OnEnable()
        {
            InitializeSettingUI();

            _backBtn.onClick.AddListener(_mainMenuUIManager.DisableSettingUI);
            ModifyHotKeyUI.OnKeyChanged += UpdateHotKey;

            AddListeners();
        }


        void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();
            RemoveAllListeners();

            ModifyHotKeyUI.OnKeyChanged -= UpdateHotKey;

            _modifyHotKeyUI.gameObject.SetActive(false);
        }


        private void InitializeSettingUI()
        {
            InitializeGameLanguage();
            InitializeVolumeSlider();
            UpdateHotKey();
        }


        private void UpdateHotKey()
        {
            foreach (var hotkey in _hotkeys)
            {
                // get tmp text of hotkey button
                TMP_Text keyText;
                if (hotkey.gameObject.transform.childCount == 0) break;
                keyText = hotkey.gameObject.GetComponentInChildren<TMP_Text>();

                // get game obj name
                var keyName = hotkey.gameObject.name;

                // check and set text equal key binding [obj name]
                if (GameSetting.Instance.keyBindings.ContainsKey(keyName))
                {
                    keyText.SetText(GameSetting.Instance.keyBindings[keyName].ToString());
                }
            }
        }


        private void InitializeGameLanguage()
        {
            _languageDropdown.ClearOptions();
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData(GameLanguage.Vietnamese.ToString(), null));
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData(GameLanguage.English.ToString(), null));

            _languageDropdown.value = (int)GameSetting.Instance.gameLanguage;
        }


        private void InitializeVolumeSlider()
        {
            _bgmSlider.value = GameSetting.Instance.bgmVolume;
            _sfxSlider.value = GameSetting.Instance.sfxVolume;
        }


        private void OnModifyKeyPress(string keyName)
        {
            _modifyHotKeyUI.InitializeUI(keyName);
            _modifyHotKeyUI.gameObject.SetActive(true);
        }


        private void OnLanguageChanged(int index)
        {
            GameSetting.Instance.ModifyGameLanguage((GameLanguage)index);
        }


        private void OnBgmVolumeChanged(float value)
        {
            GameSetting.Instance.ModifyBackgroundVolume(value);
        }


        private void OnSfxVolumeChanged(float value)
        {
            GameSetting.Instance.ModifySoundVolume(value);
        }


        private void AddListeners()
        {
            foreach (var hotkey in _hotkeys)
            {
                // use lambda expression to add listener has arguments
                hotkey.onClick.AddListener(() => OnModifyKeyPress(hotkey.gameObject.name));
            }

            // add listener for language dropdown
            _languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

            // add listener to sliders
            _bgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
            _sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        }


        private void RemoveAllListeners()
        {
            foreach (var hotkey in _hotkeys)
            {
                hotkey.onClick.RemoveAllListeners();
            }

            // remove all listeners from language dropdown
            _languageDropdown.onValueChanged.RemoveAllListeners();

            // remove all listeners from sliders
            _bgmSlider.onValueChanged.RemoveAllListeners();
            _sfxSlider.onValueChanged.RemoveAllListeners();
        }


        private void CheckPropertiesValue()
        {
            if (_backBtn == null ||
                _hotkeys == null ||
                _languageDropdown == null ||
                _bgmSlider == null ||
                _sfxSlider == null ||
                _modifyHotKeyUI == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }

            if (_mainMenuUIManager == null)
            {
                Debug.LogError("Can't load _mainMenuUIManager.");
            }
        }
    }
}
