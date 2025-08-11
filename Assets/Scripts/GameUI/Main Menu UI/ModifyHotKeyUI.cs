using UnityEngine;
using TMPro;
using System;

using Management;


namespace GameUI
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

            bool assignSuccess = false;
            // wait for player press new hotkey
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    // check if new key can assign to the action
                    assignSuccess = GameSetting.Instance.ModifyKeyBiding(_keyName, kcode);

                    // modify the ui if player can't assign new hotkey
                    if (!assignSuccess)
                    {
                        _message.SetText("The key pressed is assigned in another action.\nPlease press another key!");
                        _message.color = Color.red;
                        return;
                    }

                    assignSuccess = true;
                    _message.color = Color.black;
                }
            }

            // update new key in setting ui
            OnKeyChanged?.Invoke();

            // if player assign new hotkey unsuccessful, don't close the ui
            gameObject.SetActive(!assignSuccess);
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
