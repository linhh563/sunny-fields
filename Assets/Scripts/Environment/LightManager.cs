using System;
using Management;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Environment
{
    [RequireComponent(typeof(Light2D))]
    public class LightManager : MonoBehaviour
    {
        private Light2D _light2D;

        [SerializeField] private Gradient _gradient;

        private void Awake()
        {
            _light2D = GetComponent<Light2D>();

            // subscribe necessary events
            TimeManager.OnTimeChanged += UpdateLightColor;
        }

        void OnDisable()
        {
            // unsubscribe events
            TimeManager.OnTimeChanged -= UpdateLightColor;
        }

        private void UpdateLightColor(object sender, TimeSpan newTime)
        {
            _light2D.color = _gradient.Evaluate(PercentOfDay(newTime));
        }

        private float PercentOfDay(TimeSpan time)
        {
            return (float)time.TotalMinutes % EnvironmentConstants.MinuteInDay / EnvironmentConstants.MinuteInDay;
        }
    }
}
