using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class SettingUIManager : MonoBehaviour
    {
        [SerializeField] private Button _backBtn;
        private MainMenuUIManager _mainMenuUIManager;

        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();

            CheckPropertiesValue();
        }

        void OnEnable()
        {
            _backBtn.onClick.AddListener(_mainMenuUIManager.DisableSettingUI);            
        }

        void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();
        }

        private void CheckPropertiesValue()
        {
            if (_mainMenuUIManager == null)
            {
                Debug.LogError("Can't Main Menu UI Manager script in " + gameObject.name + ".");
                return;
            }

            if (_backBtn == null)
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
