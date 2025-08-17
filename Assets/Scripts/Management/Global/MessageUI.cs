using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameUI
{
    public class MessageUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _button;


        void Start()
        {
            CheckPropertiesValue();
        }


        void OnEnable()
        {
            _button.onClick.AddListener(DisableUI);
        }


        void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }


        public void SetMessageText(string message)
        {
            _messageText.SetText(message);
        }


        private void DisableUI()
        {
            gameObject.SetActive(false);
        }


        private void CheckPropertiesValue()
        {
            if (_messageText == null ||
                _button == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
