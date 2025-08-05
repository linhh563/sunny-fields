using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Management;

namespace GameUI
{
    public class SettingUIManager : MonoBehaviour
    {
        [SerializeField] private Button _backBtn;
        private MainMenuUIManager _mainMenuUIManager;

        [SerializeField] private TMP_Dropdown _languageDropdown;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        [SerializeField] private List<Button> _hotkeys;

        [SerializeField] private ModifyHotKeyUI _modifyHotKeyUI;


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

            AddListenerForButtons();
        }


        void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();
            RemoveListenerFromButtons();

            ModifyHotKeyUI.OnKeyChanged -= UpdateHotKey;
        }


        private void InitializeSettingUI()
        {
            _bgmSlider.value = GameSetting.Instance.bgmVolume;
            _sfxSlider.value = GameSetting.Instance.sfxVolume;

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


        private void OnModifyKeyPress(string keyName)
        {
            _modifyHotKeyUI.InitializeUI(keyName);
            _modifyHotKeyUI.gameObject.SetActive(true);
        }


        private void AddListenerForButtons()
        {
            foreach (var hotkey in _hotkeys)
            {
                // use lambda expression to add listener has arguments
                hotkey.onClick.AddListener(() => OnModifyKeyPress(hotkey.gameObject.name));
            }
        }


        private void RemoveListenerFromButtons()
        {
            foreach (var hotkey in _hotkeys)
            {
                hotkey.onClick.RemoveAllListeners();
            }
        }


        private void CheckPropertiesValue()
        {
            if (_mainMenuUIManager == null)
            {
                Debug.LogError("Can't Main Menu UI Manager script in " + gameObject.name + ".");
                return;
            }

            if (_backBtn == null || _hotkeys == null)
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
            }

            if (_languageDropdown == null || _bgmSlider == null || _sfxSlider == null)
            {
                Debug.LogError("There is a slider or dropdown was not assigned in " + gameObject.name + ".");
            }

            if (_modifyHotKeyUI == null)
            {
                Debug.LogError("There is a script was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
