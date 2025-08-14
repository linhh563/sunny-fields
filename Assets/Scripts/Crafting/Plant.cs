using UnityEngine;
using Management;

namespace Crafting
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Plant : MonoBehaviour
    {
        private PlantScriptableObject _scriptableObject;
        // age is calculated by days
        private int _dayAge;
        private int _notWateredDay;
        private SpriteRenderer _spriteRenderer;

        // represent for current growing phase of the plant
        private int _phase;
        private bool _isWatered;
        private bool _canHarvest;

        // public fields
        public PlantScriptableObject plantScriptableObject { get => _scriptableObject; set => _scriptableObject = value; }
        public int dayAge { get => _dayAge; set => _dayAge = value; }
        public bool isWatered { get => _isWatered; set => _isWatered = value; }
        

        public void Initialize(PlantScriptableObject scriptableObject)
        {
            // set up default values
            _phase = 0;
            _dayAge = 0;
            _notWateredDay = 0;
            _canHarvest = false;

            // get components
            _spriteRenderer = GetComponent<SpriteRenderer>();

            // subscribe necessary events
            TimeManager.OnDayChanged += UpdateNotWateredDay;
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
            // unsubscribe events
            TimeManager.OnDayChanged -= UpdateNotWateredDay;
            TimeManager.OnDayChanged -= UpdateAge;
        }


        public void UpdateAge()
        {
            if (!_isWatered) return;

            _dayAge++;
            UpdatePlantPhase();
            _notWateredDay = 0;

            _isWatered = false;
        }


        public void UpdateWateredState()
        {
            _isWatered = true;
        }


        private void UpdateNotWateredDay()
        {
            _notWateredDay++;

            CheckDie();
        }


        private void CheckDie()
        {
            // if the days plant was not watered greater than the limit, plant will die
            if (_notWateredDay > _scriptableObject.noWateredLimit)
            {
                Debug.Log("Plant died");
                // TODO: CHANGE PLANT SPRITE
            }
        }


        public void Harvest()
        {
            if (!_canHarvest) return;

            Debug.Log("Harvesting...");
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

                if (_phase == _scriptableObject.totalPhase - 1)
                {
                    _canHarvest = true;
                }
            }
        }
    }
}