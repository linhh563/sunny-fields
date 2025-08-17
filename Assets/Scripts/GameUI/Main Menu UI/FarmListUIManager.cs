using System.IO;
using Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class FarmListUIManager : MonoBehaviour
    {
        [SerializeField] private Button _backBtn;
        [SerializeField] private Transform _farmContainer;
        [SerializeField] private TMP_Text _messageText;

        private MainMenuUIManager _mainMenuUIManager;
        private GameObject _savedFarmUI;

        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();
            _savedFarmUI = Resources.Load<GameObject>("Prefabs/SavedFarmUI");

            CheckPropertiesValue();
        }

        void OnEnable()
        {
            _backBtn.onClick.AddListener(_mainMenuUIManager.DisableFarmListUI);
            _backBtn.onClick.AddListener(PlayButtonPressSfx);

            SavedFarmUI.OnFarmDeleted += RefreshFarmList;

            LoadFarmList();
        }

        void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();
            SavedFarmUI.OnFarmDeleted -= RefreshFarmList;

            HideAllSavedFarm();
        }


        private void LoadFarmList()
        {
            string folderPath = Path.Combine(Application.dataPath, FilePath.FARMS_FOLDER_PATH);
            var files = Directory.GetFiles(folderPath);

            // if there is no saved farm, display a message
            if (files.Length == 0)
            {
                _messageText.gameObject.SetActive(true);
                _messageText.SetText("There is no Saved farm in list!!!");
                return;
            }

            _messageText.gameObject.SetActive(false);
            foreach (var file in files)
            {
                // only get json file, ignore .meta file
                if (file[file.Length - 1] != 'a')
                {
                    // get each farm attributes
                    var farmConfig = FarmConfig.LoadFarmConfigByPath(file);

                    // create saved farm ui and set up it
                    var farm = ObjectPoolManager.SpawnObject(_savedFarmUI, _farmContainer);
                    farm.GetComponent<SavedFarmUI>().InitializeSavedFarm(farmConfig.characterName, farmConfig.farmName, farmConfig.gameTimeMinutes, farmConfig.farmSize);
                }
            }
        }


        private void RefreshFarmList()
        {
            HideAllSavedFarm();
            LoadFarmList();
        }


        private void HideAllSavedFarm()
        {
            foreach (Transform obj in _farmContainer)
            {
                ObjectPoolManager.ReturnObjectToPool(obj.gameObject);
            }
        }


        private void PlayButtonPressSfx()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pressButtonSfx);
        }



        private void CheckPropertiesValue()
        {
            if (_mainMenuUIManager == null)
            {
                Debug.LogError("Can't Main Menu UI Manager script in " + gameObject.name + ".");
            }

            if (_savedFarmUI == null)
            {
                Debug.LogError("Can't load Saved farm UI in " + gameObject.name + ".");
            }

            if (_backBtn == null ||
                _farmContainer == null ||
                _messageText == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
