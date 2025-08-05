using UnityEngine;
using TMPro;
using System;


namespace Management
{
    public class ModifyHotKeyUI : MonoBehaviour
    {
        private string _keyName;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;

        // use to update new key in setting ui
        public static event Action OnKeyChanged;


        void Start()
        {
            CheckPropertiesValue();
        }


        void Update()
        {
            WaitingForInput();
        }


        public void InitializeUI(string keyName)
        {
            _keyName = keyName;

            _title.SetText("MODIFY " + _keyName.ToUpper() + " KEY");
            _message.SetText("Press new hotkey for " + _keyName + "!");
        }


        private void WaitingForInput()
        {
            if (!Input.anyKey) return;

            // wait for player press new hotkey
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    GameSetting.Instance.ModifyKeyBiding(_keyName, kcode);
                }
            }

            // update new key in setting ui
            OnKeyChanged?.Invoke();

            // close ui
            gameObject.SetActive(false);
        }


        private void CheckPropertiesValue()
        {
            if (_title == null || _message == null)
            {
                Debug.LogError("There is a text was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
