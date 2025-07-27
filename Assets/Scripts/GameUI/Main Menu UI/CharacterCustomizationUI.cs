using Management;
using Management.ScriptableObjects;
using TMPro;
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

        // character clothes index
        [SerializeField] TMP_Text _hairIndex;
        [SerializeField] TMP_Text _hatIndex;
        [SerializeField] TMP_Text _shirtIndex;
        [SerializeField] TMP_Text _pantIndex;

        // character customization images
        [SerializeField] Image _hairImage;
        [SerializeField] Image _hatImage;
        [SerializeField] Image _shirtImage;
        [SerializeField] Image _pantImage;

        private MainMenuUIManager _mainMenuUIManager;

        public delegate void CustomizeButtonClicked(ClotheType clotheType, bool isNextButton);
        public static CustomizeButtonClicked OnCustomizeButtonClicked;

        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();

            CheckPropertiesValue();
        }

        private void OnEnable()
        {
            AddButtonsListener();
            ResetClotheUI();
            UpdateClothesIndexUI();
        }

        void OnDisable()
        {
            RemoveListenerFromAllButtons();
        }

        private void ChangeNextHat()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Hat, true);

            // get hat scriptable object from CharacterCustomization script
            var nextHat = CharacterCustomization._hatCollection[CharacterCustomization._currentHatIndex] as ClotheScriptableObject;

            if (nextHat == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _hatImage.sprite = nextHat.sprite;
        }

        private void ChangePreviousHat()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Hat, false);

            // get hat scriptable object from CharacterCustomization script
            var previousHat = CharacterCustomization._hatCollection[CharacterCustomization._currentHatIndex] as ClotheScriptableObject;

            if (previousHat == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _hatImage.sprite = previousHat.sprite;
        }

        private void ChangeNextHair()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Hair, true);

            // get hair scriptable object from CharacterCustomization script
            var nextHair = CharacterCustomization._hairCollection[CharacterCustomization._currentHairIndex] as ClotheScriptableObject;

            if (nextHair == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _hairImage.sprite = nextHair.sprite;
        }

        private void ChangePreviousHair()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Hair, false);

            // get hat scriptable object from CharacterCustomization script
            var previousHair = CharacterCustomization._hairCollection[CharacterCustomization._currentHairIndex] as ClotheScriptableObject;

            if (previousHair == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _hairImage.sprite = previousHair.sprite;
        }

        private void ChangeNextShirt()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Shirt, true);

            // get hat scriptable object from CharacterCustomization script
            var nextShirt = CharacterCustomization._shirtCollection[CharacterCustomization._currentShirtIndex] as ClotheScriptableObject;

            if (nextShirt == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _shirtImage.sprite = nextShirt.sprite;
        }

        private void ChangePreviousShirt()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Shirt, false);

            // get hat scriptable object from CharacterCustomization script
            var previousShirt = CharacterCustomization._shirtCollection[CharacterCustomization._currentShirtIndex] as ClotheScriptableObject;

            if (previousShirt == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _shirtImage.sprite = previousShirt.sprite;
        }

        private void ChangeNextPant()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Pant, true);

            // get hat scriptable object from CharacterCustomization script
            var nextPant = CharacterCustomization._pantCollection[CharacterCustomization._currentPantIndex] as ClotheScriptableObject;

            if (nextPant == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _pantImage.sprite = nextPant.sprite;
        }

        private void ChangePreviousPant()
        {
            OnCustomizeButtonClicked?.Invoke(ClotheType.Pant, false);

            // get hat scriptable object from CharacterCustomization script
            var previousPant = CharacterCustomization._pantCollection[CharacterCustomization._currentPantIndex] as ClotheScriptableObject;

            if (previousPant == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            _pantImage.sprite = previousPant.sprite;
        }

        private void UpdateClothesIndexUI()
        {
            _hairIndex.SetText((CharacterCustomization._currentHairIndex + 1).ToString());
            _hatIndex.SetText((CharacterCustomization._currentHatIndex + 1).ToString());
            _shirtIndex.SetText((CharacterCustomization._currentShirtIndex + 1).ToString());
            _pantIndex.SetText((CharacterCustomization._currentPantIndex + 1).ToString());
        }

        private void AddButtonsListener()
        {
            _backBtn.onClick.AddListener(_mainMenuUIManager.DisableCustomizeCharacterUI);
            _backBtn.onClick.AddListener(CharacterCustomization.ResetClothesIndex);

            // add listener to character customization buttons
            _nextHatBtn.onClick.AddListener(ChangeNextHat);
            _previousHatBtn.onClick.AddListener(ChangePreviousHat);
            _nextHairBtn.onClick.AddListener(ChangeNextHair);
            _previousHairBtn.onClick.AddListener(ChangePreviousHair);
            _nextShirtBtn.onClick.AddListener(ChangeNextShirt);
            _previousShirtBtn.onClick.AddListener(ChangePreviousShirt);
            _nextPantBtn.onClick.AddListener(ChangeNextPant);
            _previousPantBtn.onClick.AddListener(ChangePreviousPant);

            _nextHairBtn.onClick.AddListener(UpdateClothesIndexUI);
            _previousHairBtn.onClick.AddListener(UpdateClothesIndexUI);
            _nextHatBtn.onClick.AddListener(UpdateClothesIndexUI);
            _previousHatBtn.onClick.AddListener(UpdateClothesIndexUI);
            _nextShirtBtn.onClick.AddListener(UpdateClothesIndexUI);
            _previousShirtBtn.onClick.AddListener(UpdateClothesIndexUI);
            _nextPantBtn.onClick.AddListener(UpdateClothesIndexUI);
            _previousPantBtn.onClick.AddListener(UpdateClothesIndexUI);
        }

        private void RemoveListenerFromAllButtons()
        {
            _backBtn.onClick.RemoveAllListeners();

            // remove all listeners in character customization buttons
            _nextHairBtn.onClick.RemoveAllListeners();
            _previousHairBtn.onClick.RemoveAllListeners();
            _nextHatBtn.onClick.RemoveAllListeners();
            _previousHatBtn.onClick.RemoveAllListeners();
            _nextShirtBtn.onClick.RemoveAllListeners();
            _previousShirtBtn.onClick.RemoveAllListeners();
            _nextPantBtn.onClick.RemoveAllListeners();
            _previousPantBtn.onClick.RemoveAllListeners();
        }

        private void ResetClotheUI()
        {
            // reset hat image
            var initialHat = CharacterCustomization._hatCollection[CharacterCustomization._currentHatIndex] as ClotheScriptableObject;
            _hatImage.sprite = initialHat.sprite;

            // reset hair image
            var initialHair = CharacterCustomization._hairCollection[CharacterCustomization._currentHairIndex] as ClotheScriptableObject;
            _hairImage.sprite = initialHair.sprite;

            // reset shirt image
            var initialShirt = CharacterCustomization._shirtCollection[CharacterCustomization._currentShirtIndex] as ClotheScriptableObject;
            _shirtImage.sprite = initialShirt.sprite;

            // reset pant image
            var initialPant = CharacterCustomization._pantCollection[CharacterCustomization._currentPantIndex] as ClotheScriptableObject;
            _pantImage.sprite = initialPant.sprite;
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
            if (_backBtn == null || _previousGenderBtn == null || _nextGenderBtn == null
            || _previousHatBtn == null || _nextHatBtn == null || _previousHairBtn == null
            || _nextHairBtn == null || _previousShirtBtn == null || _nextShirtBtn == null
            || _previousPantBtn == null || _nextPantBtn == null)
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
                return;
            }

            // check the serialize text's value
            if (_hairIndex == null || _hatIndex == null || _shirtIndex == null || _pantIndex == null)
            {
                Debug.LogError("There is a text mesh pro was not assigned in " + gameObject.name + ".");
                return;
            }

            // check the serialize images' value
            if (_hairImage == null || _hatImage == null || _shirtImage == null || _pantImage == null)
            {
                Debug.LogError("There is an image was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
