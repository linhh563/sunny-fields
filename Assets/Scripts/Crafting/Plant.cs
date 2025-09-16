using UnityEngine;
using Management;


namespace Crafting
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Plant : MonoBehaviour
    {
        [SerializeField] private InventoryManager _inventoryManager;

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


        void Start()
        {
            _inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();

            if (_inventoryManager == null)
            {
                Debug.LogError("load inventory manager fail");
            }
        }


        private void OnDisable()
        {
            TimeManager.OnDayChanged -= UpdateNotWateredDay;
            TimeManager.OnDayChanged -= UpdateAge;
        }


        public void Initialize(PlantScriptableObject plantObj)
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
            if (plantObj == null)
            {
                Debug.LogError("Plant scriptable object is null");
                return;
            }
            _scriptableObject = plantObj;

            // set seed sprite for plant
            _spriteRenderer.sprite = _scriptableObject.sprites[_phase];
        }


        public void SetupPlant(PlantScriptableObject plant, Vector3Int pos, int dayAge, bool isWatered)
        {
            Initialize(plant);

            _dayAge = dayAge;
            _isWatered = isWatered;

            // update plant phase
            // if (_dayAge == 0)
            // {
            //     _phase = 0;
            // }
            // else
            // {
            _phase = GetPlantPhase();
            // }

            _spriteRenderer.sprite = _scriptableObject.sprites[_phase];

            // add plant to plant list
            PlantManager.AddPlant(pos, this);
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
                ObjectPoolManager.ReturnObjectToPool(this.gameObject);

                Vector3Int pos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
                PlantManager.RemovePlant(pos);
            }
        }


        public void Harvest()
        {
            if (!_canHarvest) return;

            _inventoryManager.AddItem(_scriptableObject.product);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }


        private void UpdatePlantPhase()
        {
            if (_phase == _scriptableObject.totalPhase - 1)
            {
                return;
            }

            // plant move to new phase
            if (_dayAge >= _scriptableObject.grownTime[_phase])
            {
                _phase++;
                _spriteRenderer.sprite = _scriptableObject.sprites[_phase];

                if (_phase == _scriptableObject.totalPhase - 1)
                {
                    _canHarvest = true;
                }
            }
        }


        private int GetPlantPhase()
        {
            int phase = 0;

            if (_dayAge > _scriptableObject.grownTime[_scriptableObject.totalPhase - 1])
                return _scriptableObject.totalPhase - 1;

            while (_dayAge > _scriptableObject.grownTime[phase])
            {
                phase++;
            }

            return phase;
        }
    }
}