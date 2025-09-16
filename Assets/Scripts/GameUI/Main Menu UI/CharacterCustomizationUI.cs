using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Management;
using Management.ScriptableObjects;
using System.IO;
using System.Collections.Generic;

namespace GameUI
{
    public class CharacterCustomizationUI : MonoBehaviour
    {
        [Header("Buttons")]
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
        [SerializeField] private Button _nextFarmSizeButton;
        [SerializeField] private Button _previousFarmSizeButton;
        [SerializeField] private Button _startButton;


        [Header("Texts")]
        // character clothes index
        [SerializeField] private TMP_Text _hairIndex;
        [SerializeField] private TMP_Text _hatIndex;
        [SerializeField] private TMP_Text _shirtIndex;
        [SerializeField] private TMP_Text _pantIndex;
        [SerializeField] private TMP_Text _farmSizeText;


        [Header("Images")]
        // character customization images
        [SerializeField] private Image _hairImage;
        [SerializeField] private Image _hatImage;
        [SerializeField] private Image _shirtImage;
        [SerializeField] private Image _pantImage;


        [Header("Text Fields")]
        [SerializeField] private TMP_InputField _characterNameTxtField;
        [SerializeField] private TMP_InputField _farmNameTxtField;

        [Header("UIs")]
        [SerializeField] private GameObject _discardMessageUI;
        [SerializeField] private GameObject _messageUI;


        private MainMenuUIManager _mainMenuUIManager;
        private FarmSize _farmSize;

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
            ResetCustomizeUI();

            _farmSize = FarmSize.Medium;
            UpdateFarmSizeText();

            _discardMessageUI.SetActive(false);
            _messageUI.SetActive(false);
        }


        void OnDisable()
        {
            CharacterCustomization.ResetClothesIndex();
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

            _hatImage.sprite = nextHat.forwardSprite;
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

            _hatImage.sprite = previousHat.forwardSprite;
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

            _hairImage.sprite = nextHair.forwardSprite;
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

            _hairImage.sprite = previousHair.forwardSprite;
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

            _shirtImage.sprite = nextShirt.forwardSprite;
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

            _shirtImage.sprite = previousShirt.forwardSprite;
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

            _pantImage.sprite = nextPant.forwardSprite;
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

            _pantImage.sprite = previousPant.forwardSprite;
        }


        private void ChangeNextFarmSize()
        {
            int newFarmSize = (int)_farmSize + 1;
            if (newFarmSize > 3)
            {
                _farmSize = FarmSize.Small;
            }
            else
            {
                _farmSize = (FarmSize)newFarmSize;
            }

            UpdateFarmSizeText();
        }


        private void ChangePreviousFarmSize()
        {
            int newFarmSize = (int)_farmSize - 1;
            if (newFarmSize == 0)
            {
                _farmSize = FarmSize.Large;
            }
            else
            {
                _farmSize = (FarmSize)newFarmSize;
            }

            UpdateFarmSizeText();
        }


        private void UpdateFarmSizeText()
        {
            _farmSizeText.SetText(_farmSize.ToString());
        }


        private void UpdateClothesIndexUI()
        {
            _hairIndex.SetText((CharacterCustomization._currentHairIndex + 1).ToString());
            _hatIndex.SetText((CharacterCustomization._currentHatIndex + 1).ToString());
            _shirtIndex.SetText((CharacterCustomization._currentShirtIndex + 1).ToString());
            _pantIndex.SetText((CharacterCustomization._currentPantIndex + 1).ToString());
        }


        private void LoadGameplayScene()
        {
            if (CheckCustomizeInfo())
            {
                // set up farm attributes
                CharacterCustomizationStorage.SetFarmAttribute(_characterNameTxtField.text, _farmNameTxtField.text);

                // check if there is no saved farm has the same name with new farm
                string gameData = Path.Combine(Application.streamingAssetsPath, "GameData");
                string file = gameData + "/" + _characterNameTxtField.text + ".json";
                string path = Path.Combine(Application.dataPath, file);

                if (File.Exists(path))
                {
                    // display message, farm was existed
                    _messageUI.SetActive(true);
                    _messageUI.GetComponent<MessageUI>().SetMessageText("The farm name was already existed.\nPlease enter other name.");

                    return;
                }

                // create new farm config file
                FarmConfig newFarm = new FarmConfig(_characterNameTxtField.text, _farmNameTxtField.text, _farmSize);

                // first day start at 6 am
                newFarm.gameTimeMinutes = 360;

                // set up default farm config
                var inventory = new List<ItemConfig>();

                inventory.Add(new ItemConfig()
                {
                    itemName = "Hoe",
                    slotIndex = 0,
                    quantity = 1
                });

                inventory.Add(new ItemConfig()
                {
                    itemName = "Water Can",
                    slotIndex = 1,
                    quantity = 1
                });

                inventory.Add(new ItemConfig()
                {
                    itemName = "Scythe",
                    slotIndex = 2,
                    quantity = 1
                });

                inventory.Add(new ItemConfig()
                {
                    itemName = "Corn Seed",
                    slotIndex = 3,
                    quantity = 5
                });

                newFarm.inventory = inventory;

                newFarm.SaveFarmConfig();

                SceneManager.LoadScene(_farmSize.ToString() + "Farm");
            }
            else
            {
                // show the message
                _messageUI.SetActive(true);
                _messageUI.GetComponent<MessageUI>().SetMessageText("There is a value was empty.\nPlease recheck and enter to that.");
            }
        }


        private void EnableDiscardMessageUI()
        {
            _discardMessageUI.SetActive(true);
        }


        private bool CheckCustomizeInfo()
        {
            if (_characterNameTxtField.text == "" || _farmNameTxtField.text == "")
                return false;

            return true;
        }


        private void PlayButtonPressSfx()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pressButtonSfx);
        }


        private void AddButtonsListener()
        {
            _backBtn.onClick.AddListener(EnableDiscardMessageUI);
            _backBtn.onClick.AddListener(PlayButtonPressSfx);

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

            _nextHatBtn.onClick.AddListener(PlayButtonPressSfx);
            _previousHatBtn.onClick.AddListener(PlayButtonPressSfx);
            _nextHairBtn.onClick.AddListener(PlayButtonPressSfx);
            _previousHairBtn.onClick.AddListener(PlayButtonPressSfx);
            _nextShirtBtn.onClick.AddListener(PlayButtonPressSfx);
            _previousShirtBtn.onClick.AddListener(PlayButtonPressSfx);
            _nextPantBtn.onClick.AddListener(PlayButtonPressSfx);
            _previousPantBtn.onClick.AddListener(PlayButtonPressSfx);

            _nextFarmSizeButton.onClick.AddListener(ChangeNextFarmSize);
            _previousFarmSizeButton.onClick.AddListener(ChangePreviousFarmSize);

            _nextFarmSizeButton.onClick.AddListener(PlayButtonPressSfx);
            _previousFarmSizeButton.onClick.AddListener(PlayButtonPressSfx);

            // add listener to start button
            _startButton.onClick.AddListener(LoadGameplayScene);
            _startButton.onClick.AddListener(PlayButtonPressSfx);
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

            _nextFarmSizeButton.onClick.RemoveAllListeners();
            _previousFarmSizeButton.onClick.RemoveAllListeners();
        }


        private void ResetClotheUI()
        {
            // reset hat image
            var initialHat = CharacterCustomization._hatCollection[CharacterCustomization._currentHatIndex] as ClotheScriptableObject;
            _hatImage.sprite = initialHat.forwardSprite;

            // reset hair image
            var initialHair = CharacterCustomization._hairCollection[CharacterCustomization._currentHairIndex] as ClotheScriptableObject;
            _hairImage.sprite = initialHair.forwardSprite;

            // reset shirt image
            var initialShirt = CharacterCustomization._shirtCollection[CharacterCustomization._currentShirtIndex] as ClotheScriptableObject;
            _shirtImage.sprite = initialShirt.forwardSprite;

            // reset pant image
            var initialPant = CharacterCustomization._pantCollection[CharacterCustomization._currentPantIndex] as ClotheScriptableObject;
            _pantImage.sprite = initialPant.forwardSprite;
        }


        private void ResetCustomizeUI()
        {
            // reset character name and farm name
            _farmNameTxtField.text = "";
            _characterNameTxtField.text = "";

            // reset farm size
            _farmSize = FarmSize.Medium;
            UpdateFarmSizeText();

            // reset character clothes
            ResetClotheUI();
            UpdateClothesIndexUI();
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
                || _previousPantBtn == null || _nextPantBtn == null || _startButton == null ||
                _nextFarmSizeButton == null || _previousFarmSizeButton == null)
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
                return;
            }

            // check the serialize text's value
            if (_hairIndex == null || _hatIndex == null || _shirtIndex == null || _pantIndex == null)
            {
                Debug.LogError("There is a text was not assigned in " + gameObject.name + ".");
                return;
            }

            // check the serialize images' value
            if (_hairImage == null || _hatImage == null || _shirtImage == null || _pantImage == null)
            {
                Debug.LogError("There is an image was not assigned in " + gameObject.name + ".");
                return;
            }

            if (_characterNameTxtField == null ||
                _farmNameTxtField == null)
            {
                Debug.LogError("There is a text fields was not assigned in " + gameObject.name + ".");
            }

            if (_discardMessageUI == null ||
                _messageUI == null)
            {
                Debug.LogError("There is a component was not assigned in" + gameObject.name + ".");
            }
        }
    }
}
