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
        private float _minuteLength => _dayLength / EnvironmentConstants.MINUTES_IN_DAY;

        public static event EventHandler<TimeSpan> OnTimeChanged;
        public static Action OnDayChanged;

        void Awake()
        {
            _dayLength = EnvironmentConstants.DAY_LENGTH;
        }

        void Start()
        {
            StartCoroutine(AddMinute());
        }

        // increase time in game by 1 minute
        private IEnumerator AddMinute()
        {
            while (true)
            {
                _currentTime += TimeSpan.FromMinutes(1);
                OnTimeChanged?.Invoke(this, _currentTime);

                if (_currentTime.TotalMinutes % EnvironmentConstants.MINUTES_IN_DAY == 0)
                {
                    OnDayChanged?.Invoke();
                }

                yield return new WaitForSeconds(_minuteLength);
            }
        }
    }
}

