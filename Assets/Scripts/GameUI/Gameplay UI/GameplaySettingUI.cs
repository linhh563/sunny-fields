using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


namespace GameUI
{
    public class GameplaySettingUI : MonoBehaviour
    {
        [SerializeField] protected Button _backBtn;
        [SerializeField] protected TMP_Dropdown _languageDropdown;
        [SerializeField] protected Slider _bgmSlider;
        [SerializeField] protected Slider _sfxSlider;
        [SerializeField] protected List<Button> _hotkeys;
        [SerializeField] protected ModifyHotKeyUI _modifyHotKeyUI;
        [SerializeField] private Button _saveGameButton;


        void Start()
        {
            CheckPropertiesValue();
        }

        void OnEnable()
        {
            _backBtn.onClick.AddListener(() => gameObject.SetActive(false));            
        }

        void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();
        }


        private void CheckPropertiesValue()
        {
            if (_backBtn == null ||
                _hotkeys == null ||
                _languageDropdown == null ||
                _bgmSlider == null ||
                _sfxSlider == null ||
                _modifyHotKeyUI == null ||
                _saveGameButton == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
