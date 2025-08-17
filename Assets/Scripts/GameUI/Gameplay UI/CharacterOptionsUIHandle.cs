using Management;
using TMPro;
using UnityEngine;

namespace GameUI
{
    public class CharacterOptionsUIHandle : MonoBehaviour
    {
        [SerializeField] private Transform _rightInteractOptionsUI;
        [SerializeField] private Transform _leftInteractOptionsUI;
        [SerializeField] private TMP_Text _rightOptionText;
        [SerializeField] private TMP_Text _leftOptionText;


        private int _maxOptions;
        private int _selectedOptionIndex;


        void Start()
        {
            CheckPropertiesValue();
        }


        public void EnableOptionsUI(bool isLeft)
        {
            _rightInteractOptionsUI.gameObject.SetActive(!isLeft);
            _leftInteractOptionsUI.gameObject.SetActive(isLeft);
        }


        public void DisableOptionsUI()
        {
            _rightInteractOptionsUI.gameObject.SetActive(false);
            _leftInteractOptionsUI.gameObject.SetActive(false);
        }


        public void SetOptionText(string objName, CharacterInteractType type)
        {
            switch (type)
            {
                case CharacterInteractType.Item:
                    _rightOptionText.SetText(objName + " (" + GameSetting.Instance.keyBindings["Interact"] + ")");
                    _leftOptionText.SetText(objName + " (" + GameSetting.Instance.keyBindings["Interact"] + ")");
                    break;

                case CharacterInteractType.NPC:
                    _rightOptionText.SetText(objName + " (" + GameSetting.Instance.keyBindings["InteractNPC"] + ")");
                    _leftOptionText.SetText(objName + " (" + GameSetting.Instance.keyBindings["InteractNPC"] + ")");
                    break;
            }
        }


        // TODO: subscribe a handle input event
        private void NextOption()
        {
            if (_selectedOptionIndex == _maxOptions - 1)
            {
                _selectedOptionIndex = 0;
                return;
            }

            _selectedOptionIndex++;
        }


        // TODO: subscribe a handle input event
        private void PreviousOption()
        {
            if (_selectedOptionIndex == 0)
            {
                _selectedOptionIndex = _maxOptions - 1;
                return;
            }

            _selectedOptionIndex--;
        }


        private void CheckPropertiesValue()
        {
            if (_rightInteractOptionsUI == null ||
                _leftInteractOptionsUI == null ||
                _rightOptionText == null ||
                _leftOptionText == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
