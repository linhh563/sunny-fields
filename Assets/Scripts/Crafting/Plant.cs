using UnityEngine;
using Management;
using System;

namespace Crafting
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Plant : MonoBehaviour
    {
        private PlantScriptableObject _scriptableObject;
        // age is calculated by days
        private int _dayAge;
        private int _minuteCounting;
        private SpriteRenderer _spriteRenderer;
        
        void Awake()
        {
            _minuteCounting = 0;
            _dayAge = 0;

            // get components
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            // subscribe necessary events
            TimeManager.OnTimeChanged += UpdateAge;
        }

        void OnDisable()
        {
            TimeManager.OnTimeChanged -= UpdateAge;
        }

        private void UpdateAge(object sender, TimeSpan timeSpan)
        {
            if (_minuteCounting == EnvironmentConstants.MINUTES_IN_DAY)
            {
                _dayAge++;
                _minuteCounting = 0;
            }
            else
            {
                _minuteCounting++;
            }
        }

        public void Initialize(PlantScriptableObject scriptableObject)
        {
            _scriptableObject = scriptableObject;

            _spriteRenderer.sprite = _scriptableObject.sprite;
        }
    }
}

