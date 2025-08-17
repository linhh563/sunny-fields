using UnityEngine;
using UnityEngine.UI;

using Management;


namespace GameUI
{
    public class DiscardCustomizeMessageUI : MonoBehaviour
    {
        [SerializeField] private Button _discardButton;
        [SerializeField] private Button _continueCustomizeButton;

        private CharacterCustomizationUI _characterCustomizeUI;


        void Start()
        {
            _characterCustomizeUI = transform.parent.gameObject.GetComponentInParent<CharacterCustomizationUI>();

            CheckPropertiesValue();
        }


        void OnEnable()
        {
            _discardButton.onClick.AddListener(DiscardCustomize);
            _continueCustomizeButton.onClick.AddListener(ContinueCustomize);

            _discardButton.onClick.AddListener(PlayButtonPressSfx);
            _continueCustomizeButton.onClick.AddListener(PlayButtonPressSfx);
        }


        void OnDisable()
        {
            _discardButton.onClick.RemoveAllListeners();
            _continueCustomizeButton.onClick.RemoveAllListeners();
        }


        private void DiscardCustomize()
        {
            transform.parent.gameObject.SetActive(false);
            _characterCustomizeUI.gameObject.SetActive(false);
        }


        private void ContinueCustomize()
        {
            transform.parent.gameObject.SetActive(false);
        }


        private void PlayButtonPressSfx()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pressButtonSfx);
        }


        private void CheckPropertiesValue()
        {
            if (_characterCustomizeUI == null)
            {
                Debug.LogError("Can't get Character Customization.");
            }

            if (_discardButton == null ||
                    _continueCustomizeButton == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
