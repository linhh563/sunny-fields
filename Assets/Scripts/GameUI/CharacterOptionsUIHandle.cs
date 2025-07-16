using UnityEngine;

namespace GameUI
{
    public class CharacterOptionsUIHandle : MonoBehaviour
    {
        [SerializeField] private Transform _rightInteractOptionsUI;
        [SerializeField] private Transform _leftInteractOptionsUI;

        private int _maxOptions;
        private int _selectedOptionIndex;

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
