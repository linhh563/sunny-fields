using UnityEngine;
using System;
using System.Collections;

namespace Management
{
    public class TimeManager : MonoBehaviour
    {
        private float _dayLength;
        private static TimeSpan _currentTime;
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


        void OnDisable()
        {
            _currentTime = TimeSpan.Zero;
        }


        public static void ChangeNextDay()
        {
            // check total time from current to 6 a.m tomorrow
            var remainTime = EnvironmentConstants.MINUTES_IN_DAY - (_currentTime.Hours * 60 + _currentTime.Minutes);
            // new day start at 6 am
            remainTime += 360;

            // set time
            _currentTime += TimeSpan.FromMinutes(remainTime);


            OnDayChanged?.Invoke();
        }


        public static void SetupTime(double minuteTime)
        {
            _currentTime += TimeSpan.FromMinutes(minuteTime);
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


        public static double GetGameTime()
        {
            return (double)_currentTime.TotalMinutes;
        }
    }
}

