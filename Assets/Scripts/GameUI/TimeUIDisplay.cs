using Management;
using TMPro;
using UnityEngine;
using System;

namespace GameUI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimeUIDisplay : MonoBehaviour
    {
        private TMP_Text _tmp_text;

        void Awake()
        {
            _tmp_text = GetComponent<TMP_Text>();

            // subscribe necessary events
            TimeManager.OnTimeChanged += ChangeTimeDisplayText;
        }

        void OnDisable()
        {
            // unsubscribe events
            TimeManager.OnTimeChanged -= ChangeTimeDisplayText;
        }

        private void ChangeTimeDisplayText(object sender, TimeSpan newTime)
        {
            if (newTime.Minutes % 30 != 0)
                return;
                
            _tmp_text.SetText("Day " + newTime.Days + "\n" + newTime.ToString(@"hh\:mm"));
        }
    }
}
