using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Management;
using System;
using System.IO;


namespace GameUI
{
    public class SavedFarmUI : MonoBehaviour
    {
        [Header("Character Attributes")]
        [SerializeField] private Image _characterAvatarImage;
        [SerializeField] private TMP_Text _characterNameText;

        [Header("Farm Attributes")]
        [SerializeField] private TMP_Text _farmNameText;
        [SerializeField] private TMP_Text _farmSizeText;
        [SerializeField] private TMP_Text _totalPlayTimeText;
        [SerializeField] private TMP_Text _gameTimeText;

        [Header("Buttons")]
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _deleteFarmButton;

        private FarmSize _farmSize;

        public static event Action OnFarmDeleted;


        void Start()
        {
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            _loadButton.onClick.AddListener(LoadGame);
            _deleteFarmButton.onClick.AddListener(DeleteSavedFarm);

            _loadButton.onClick.AddListener(PlayButtonPressSfx);
            _deleteFarmButton.onClick.AddListener(PlayButtonPressSfx);
        }


        void OnDisable()
        {
            _loadButton.onClick.RemoveAllListeners();
            _deleteFarmButton.onClick.RemoveAllListeners();
        }


        public void InitializeSavedFarm(string characterName, string farmName, double inGameMinutes, FarmSize farmSize)
        {
            // TODO: set up character avatar

            _farmSize = farmSize;

            _characterNameText.SetText(characterName);
            _farmNameText.SetText(farmName);
            _farmSizeText.SetText("Size: " + farmSize.ToString());

            // convert to "year ..., day ..." format
            string gameTime = "";
            if (inGameMinutes >= EnvironmentConstants.MINUTES_IN_YEAR)
            {
                gameTime += "Year ";
                gameTime += (inGameMinutes / EnvironmentConstants.MINUTES_IN_YEAR).ToString();
                gameTime += " - ";
            }

            int remainDays = (int)inGameMinutes % EnvironmentConstants.MINUTES_IN_YEAR;
            gameTime += "Day ";
            gameTime += (((remainDays / EnvironmentConstants.MINUTES_IN_DAY) + 1).ToString());

            _gameTimeText.SetText(gameTime);

            var playTime = inGameMinutes * (EnvironmentConstants.DAY_LENGTH / EnvironmentConstants.MINUTES_IN_DAY);
            _totalPlayTimeText.SetText("Play time: " + (((int)playTime / 60) + 1).ToString() + " minutes");
        }


        private void LoadGame()
        {
            // set up character customization storage
            CharacterCustomizationStorage.SetFarmAttribute(_characterNameText.text, _farmNameText.text);

            SceneManager.LoadScene(_farmSize + "Farm");
        }


        private void DeleteSavedFarm()
        {
            // get file path
            string fileName = "GameData/" + _farmNameText.text + ".json";
            // string filePath = Path.Combine(Application.dataPath, fileName);

            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            Debug.Log(filePath);

            // delete file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // update farm list ui
            OnFarmDeleted?.Invoke();
        }


        private void PlayButtonPressSfx()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pressButtonSfx);
        }


        private void CheckPropertiesValue()
        {
            if (_characterAvatarImage == null ||
                _characterNameText == null ||
                _farmNameText == null ||
                _totalPlayTimeText == null ||
                _gameTimeText == null ||
                _loadButton == null ||
                _farmSizeText == null ||
                _deleteFarmButton == null)
            {
                Debug.LogError("There is a component was not assigned to " + gameObject.name + ".");
            }
        }
    }
}
