using UnityEngine;

namespace GameUI
{
    public class CharacterOptionsUIHandle : MonoBehaviour
    {
        private Transform _rightOptionsUI;
        private Transform _leftOptionsUI;

        private int _maxOptions;
        private int _selectedOptionIndex;

        void Awake()
        {
            _rightOptionsUI = transform.Find("Right_CharacterOptions");
            _leftOptionsUI = transform.Find("Left_CharacterOptions");
        }

        public void EnableOptionsUI(bool isLeft)
        {
            _rightOptionsUI.gameObject.SetActive(!isLeft);
            _leftOptionsUI.gameObject.SetActive(isLeft);
        }

        public void DisableOptionsUI()
        {
            _rightOptionsUI.gameObject.SetActive(false);
            _leftOptionsUI.gameObject.SetActive(false);
        }

        // TODO
        private void HightLightOption(int index)
        {

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

        private void SelectOption()
        {
            
        }
    }

}
