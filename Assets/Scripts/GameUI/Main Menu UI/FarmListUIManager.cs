using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class FarmListUIManager : MonoBehaviour
    {
        [SerializeField] Button _backBtn;
        private MainMenuUIManager _mainMenuUIManager;

        void Awake()
        {
            _mainMenuUIManager = GetComponentInParent<MainMenuUIManager>();

            CheckPropertiesValue();
        }

        void Update()
        {
            _backBtn.onClick.AddListener(_mainMenuUIManager.DisableFarmListUI);
        }

        private void CheckPropertiesValue()
        {
            if (_mainMenuUIManager == null)
            {
                Debug.LogError("Can't Main Menu UI Manager script in " + gameObject.name + ".");
                return;
            }

            if (_backBtn == null)
            {
                Debug.LogError("There is a button was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
