using UnityEngine;
using System;
using System.Collections;

namespace Management
{
    public class TimeManager : MonoBehaviour
    {
        private float _dayLength;
        private TimeSpan _currentTime;
        // the duration in real life (second) corresponding to a minute in game
        private float _minuteLength => _dayLength / EnvironmentConstants.MinuteInDay;

        public static event EventHandler<TimeSpan> OnTimeChanged;

        void Awake()
        {
            _dayLength = EnvironmentConstants.DayLength;
        }

        void Start()
        {
            StartCoroutine(AddMinute());
        }

        // increase time in game by 1 minute
        private IEnumerator AddMinute()
        {
            while(true)
            {
                _currentTime += TimeSpan.FromMinutes(1);
                OnTimeChanged?.Invoke(this, _currentTime);

                yield return new WaitForSeconds(_minuteLength);
            }
        }
    }
}

