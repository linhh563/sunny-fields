using Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class BagUIController : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _goldText;

        // [SerializeField] private Image _characterHat;
        // [SerializeField] private Image _characterHair;
        // [SerializeField] private Image _characterShirt;
        // [SerializeField] private Image _characterPant;


        void Start()
        {
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            _backButton.onClick.AddListener(DisableBagUI);
            _backButton.onClick.AddListener(PlayButtonPressSfx);
            GameplayInputManager.OnExitUIKeyPress += DisableBagUI;

            UpdateGoldText();
            // UpdateCharacterClothesUI();

            GameplayManager.PauseGame();
        }


        void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
            GameplayInputManager.OnExitUIKeyPress -= DisableBagUI;

            GameplayManager.ResumeGame();
        }


        private void DisableBagUI()
        {
            gameObject.SetActive(false);
        }


        // private void UpdateCharacterClothesUI()
        // {
        // _characterHat.sprite = CharacterCustomizationStorage.hat.forwardSprite;
        // _characterHair.sprite = CharacterCustomizationStorage.hair.forwardSprite;
        // _characterShirt.sprite = CharacterCustomizationStorage.shirt.forwardSprite;
        // _characterPant.sprite = CharacterCustomizationStorage.pant.forwardSprite;
        // }


        private void PlayButtonPressSfx()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pressButtonSfx);
        }


        private void UpdateGoldText()
        {
            _goldText.SetText(InventoryManager.gold.ToString());
        }


        private void CheckPropertiesValue()
        {
            if (_backButton == null)
            {
                Debug.LogError("There is a game object was not assigned in " + gameObject.name + ".");
            }

            if (_goldText == null)
            {
                Debug.LogError("There is a text mesh pro object was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
