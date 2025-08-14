using System.IO;
using Management;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class FarmListUIManager : MonoBehaviour
    {
        [SerializeField] private Button _backBtn;
        [SerializeField] private Transform _farmContainer;

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

            LoadFarmList();
        }

        void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();

            HideAllSavedFarm();
        }


        private void LoadFarmList()
        {
            string folderPath = Path.Combine(Application.dataPath, FilePath.FARMS_FOLDER_PATH);
            var files = Directory.GetFiles(folderPath);

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


        private void HideAllSavedFarm()
        {
            foreach (Transform obj in _farmContainer)
            {
                ObjectPoolManager.ReturnObjectToPool(obj.gameObject);
            }
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
                    _farmContainer == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
