using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class CharacterCustomizationUI : MonoBehaviour
    {
        [SerializeField] private Button _backBtn;

        private MainMenuUIManager _mainMenuUIManager;

        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();

            CheckPropertiesValue();
        }

        void Update()
        {
            _backBtn.onClick.AddListener(_mainMenuUIManager.DisableCustomizeCharacterUI);
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
            if (_backBtn == null)
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
