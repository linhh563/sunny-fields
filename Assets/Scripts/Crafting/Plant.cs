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
        private SpriteRenderer _spriteRenderer;

        // represent for each phase of growing
        private int _phase;

        public void Initialize(PlantScriptableObject scriptableObject)
        {
            _phase = 0;
            _dayAge = 0;

            // get components
            _spriteRenderer = GetComponent<SpriteRenderer>();

            // subscribe necessary events
            TimeManager.OnDayChanged += UpdateAge;

            // check if scriptable object is null
            if (scriptableObject == null)
            {
                Debug.LogError("Plant scriptable object is null");
                return;
            }

            _scriptableObject = scriptableObject;

            // set seed sprite for plant
            _spriteRenderer.sprite = _scriptableObject.sprites[_phase];
        }

        void OnDisable()
        {
            TimeManager.OnDayChanged -= UpdateAge;
        }

        private void UpdateAge()
        {
            _dayAge++;
            UpdatePlantPhase();
        }

        private void UpdatePlantPhase()
        {
            if (_phase == _scriptableObject.totalPhase - 1)
            {
                return;
            }

            // plant move to new phase
            if (_dayAge == _scriptableObject.grownTime[_phase])
            {
                _phase++;
                _spriteRenderer.sprite = _scriptableObject.sprites[_phase];
            }
        }
    }
}

