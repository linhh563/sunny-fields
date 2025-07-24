using Management;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class CharacterCustomizationUI : MonoBehaviour
    {
        [SerializeField] private Button _backBtn;

        // customize character buttons
        [SerializeField] private Button _previousGenderBtn;
        [SerializeField] private Button _nextGenderBtn;
        [SerializeField] private Button _previousHatBtn;
        [SerializeField] private Button _nextHatBtn;
        [SerializeField] private Button _previousHairBtn;
        [SerializeField] private Button _nextHairBtn;
        [SerializeField] private Button _previousShirtBtn;
        [SerializeField] private Button _nextShirtBtn;
        [SerializeField] private Button _previousPantBtn;
        [SerializeField] private Button _nextPantBtn;

        // character customization images
        [SerializeField] Image _hairImage;
        [SerializeField] Image _hatImage;
        [SerializeField] Image _shirtImage;
        [SerializeField] Image _pantImage;

        private MainMenuUIManager _mainMenuUIManager;

        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();

            CheckPropertiesValue();
        }

        private void OnEnable() {
            _backBtn.onClick.AddListener(_mainMenuUIManager.DisableCustomizeCharacterUI);
            
            // add listener for character customization buttons
            _nextHatBtn.onClick.AddListener(ChangeNextHat);
        }

        void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();

            _nextHairBtn.onClick.RemoveAllListeners();
        }

        private void ChangeNextHat()
        {
            Debug.Log("Change next hat");

            var nextHat = CharacterCustomization.NextHat();
            if (nextHat == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
            }
        }

        private void CheckPropertiesValue()
        {
            // check value of _mainMenuUIManager
            if (_mainMenuUIManager == null)
            {
                Debug.LogError("Can't get value of Main Menu UI Manager script in " + gameObject.name + ".");
                return;
            }

            // check the serialize buttons' value
            if (_backBtn == null
            || _previousGenderBtn == null
            || _nextGenderBtn == null
            || _previousHatBtn == null
            || _nextHatBtn == null
            || _previousHairBtn == null
            || _nextHairBtn == null
            || _previousShirtBtn == null
            || _nextShirtBtn == null
            || _previousPantBtn == null
            || _nextPantBtn == null
            )
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
                return;
            }

            // check the serialize images' value
            if (_hairImage == null
            || _hatImage == null
            || _shirtImage == null
            || _pantImage == null
            )
            {
                Debug.LogError("There is an image was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
