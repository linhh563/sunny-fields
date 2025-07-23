using UnityEngine;

namespace GameUI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _customizeCharacterUI;
        [SerializeField] private GameObject _farmListUI;
        [SerializeField] private GameObject _settingUI;

        void Awake()
        {
            // check the serialize properties' value
            if (_customizeCharacterUI == null
            || _farmListUI == null
            || _settingUI == null)
            {
                Debug.LogError("There is an object was not assigned in " + gameObject.name + ".");
                return;
            }
        }

        public void EnableCustomizeCharacterUI()
        {
            _customizeCharacterUI.SetActive(true);
        }

        public void DisableCustomizeCharacterUI()
        {
            _customizeCharacterUI.SetActive(false);
        }

        public void EnableFarmListUI()
        {
            _farmListUI.SetActive(true);
        }

        public void DisableFarmListUI()
        {
            _farmListUI.SetActive(false);
        }

        public void EnableSettingUI()
        {
            _settingUI.SetActive(true);
        }

        public void DisableSettingUI()
        {
            _settingUI.SetActive(false);
        }
    }
}
