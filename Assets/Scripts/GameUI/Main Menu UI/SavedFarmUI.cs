using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace GameUI
{
    public class SavedFarmUI : MonoBehaviour
    {
        [SerializeField] private Image _characterAvatarImage;
        [SerializeField] private TMP_Text _characterNameText;

        [SerializeField] private TMP_Text _farmNameText;
        [SerializeField] private TMP_Text _totalPlayTimeText;
        [SerializeField] private TMP_Text _gameTimeText;


        void Start()
        {
            CheckPropertiesValue();
        }


        public void InitializeSavedFarm(string characterName, string farmName, double inGameMinutes)
        {
            // TODO: set up character avatar

            _characterNameText.SetText(characterName);
            _farmNameText.SetText(farmName);

            // convert to "year ..., day ..." format
            _gameTimeText.SetText(inGameMinutes.ToString());
            // _totalPlayTimeText.SetText();
        }


        private void CheckPropertiesValue()
        {
            if (_characterAvatarImage == null ||
                _characterNameText == null ||
                _farmNameText == null ||
                _totalPlayTimeText == null ||
                _gameTimeText == null)
            {
                Debug.LogError("There is a component was not assigned to " + gameObject.name + ".");
            }
        }
    }
}
