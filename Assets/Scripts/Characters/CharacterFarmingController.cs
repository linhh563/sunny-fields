using UnityEngine;
using Environment;
using Management;
using Crafting;


namespace Characters
{
    public class CharacterFarmingController : MonoBehaviour
    {
        public CharacterFarmingState farmingState { get; private set; }
        private TilemapManager _tilemapManager;
        [SerializeField] private GameObject plantPrefab;

        [SerializeField] private InventoryManager _inventory;

        private void Awake()
        {
            farmingState = CharacterFarmingState.Idle;
        }


        void Start()
        {
            _tilemapManager = FindObjectOfType<TilemapManager>();

            // Subscribe input events
            GameplayInputManager.OnFarmingKeyPress += UpdateFarmingState;
            GameplayInputManager.OnFarmingKeyRelease += SetIdleState;

            CheckPropertiesValue();
        }


        void OnDisable()
        {
            // Unsubscribe input events
            GameplayInputManager.OnFarmingKeyPress -= UpdateFarmingState;
            GameplayInputManager.OnFarmingKeyRelease -= SetIdleState;
        }


        public void HoeGround()
        {
            var frontTilePos = _tilemapManager.GetTileInFrontCharacter();

            var frontTileBaseMap = _tilemapManager.baseTilemap.GetTile(frontTilePos);

            // character can only hoe the default ground
            if (_tilemapManager.defaultGroundTile.name != frontTileBaseMap.name && _tilemapManager.defaultGroundTile_2.name != frontTileBaseMap.name)
                return;

            // spawn hoed tile in front of the character in ground tile map
            _tilemapManager.groundTilemap.SetTile(frontTilePos, _tilemapManager.hoedGroundTile);

            // add the ground position to hoed ground list if it had not existed ywt
            if (TilemapManager.hoedGrounds.Contains(frontTilePos))
                return;

            _tilemapManager.AddHoedGround(frontTilePos);
        }


        public void Planting()
        {
            var frontTilePos = _tilemapManager.GetTileInFrontCharacter();

            var frontTilePlantingMap = _tilemapManager.plantingTilemap.GetTile(frontTilePos);
            var frontTileGroundMap = _tilemapManager.groundTilemap.GetTile(frontTilePos);

            var worldFrontPos = _tilemapManager.groundTilemap.GetCellCenterWorld(frontTilePos);

            // if the ground had not hoed, player can't plant
            if (frontTileGroundMap == null) return;

            // character can only plating if the ground is hoed or watered and not planted yet
            if (frontTilePlantingMap == null &&
                (frontTileGroundMap.name == _tilemapManager.hoedGroundTile.name ||
                frontTileGroundMap.name == _tilemapManager.wateredGroundTile.name))
            {
                // spawn new plant and set up its property
                GameObject plant = ObjectPoolManager.SpawnObject(plantPrefab, worldFrontPos, Quaternion.identity, ObjectPoolType.Plant);
                plant.GetComponent<Plant>().Initialize(_inventory.GetHoldingItem() as PlantScriptableObject);

                // TODO: if the plant was planted in the watered ground, increase its age immediately
                if (frontTileGroundMap.name == _tilemapManager.wateredGroundTile.name)
                {
                    plant.GetComponent<Plant>().UpdateWateredState();
                }

                PlantManager.AddPlant(frontTilePos, plant.GetComponent<Plant>());

                // decrease item count by 1
                _inventory.ConsumeItem();

                // marking the tile has planted
                _tilemapManager.plantingTilemap.SetTile(frontTilePos, _tilemapManager.whiteTile);
            }
        }


        public void Watering()
        {
            var frontTilePos = _tilemapManager.GetTileInFrontCharacter();
            var frontTileGroundMap = _tilemapManager.groundTilemap.GetTile(frontTilePos);

            // if the ground is not hoed, player can't water
            if (frontTileGroundMap == null) return;

            // character can only water the hoed ground
            if (frontTileGroundMap.name == _tilemapManager.hoedGroundTile.name)
            {
                // ground watered logic
                _tilemapManager.groundTilemap.SetTile(frontTilePos, _tilemapManager.wateredGroundTile);

                if (TilemapManager.wateredGrounds.Contains(frontTilePos))
                    return;
                    
                _tilemapManager.AddWateredGround(frontTilePos);

                // if there is a plant in ground, water it
                if (PlantManager.plantList.ContainsKey(frontTilePos))
                {
                    var plant = PlantManager.plantList[frontTilePos];
                    plant.UpdateWateredState();
                }
            }
        }


        public void Harvesting()
        {
            // get the tile in front of the character
            var frontTilePos = _tilemapManager.GetTileInFrontCharacter();

            // check if there is a plant in front character            
            if (PlantManager.plantList.ContainsKey(frontTilePos))
            {
                var plant = PlantManager.plantList[frontTilePos];
                plant.Harvest();
            }
        }


        private void UpdateFarmingState()
        {
            var item = _inventory.GetHoldingItem();

            if (item == null) return;

            // check if holding item is plant seed
            if (item.GetType() == typeof(PlantScriptableObject))
            {
                farmingState = CharacterFarmingState.Planting;
                return;
            }

            switch (item.itemName)
            {
                case "Hoe":
                    farmingState = CharacterFarmingState.Hoeing;
                    break;
                case "Water Can":
                    farmingState = CharacterFarmingState.Watering;
                    break;
                case "Scythe":
                    farmingState = CharacterFarmingState.Harvesting;
                    break;
                default:
                    farmingState = CharacterFarmingState.Idle;
                    break;
            }
        }


        private void SetIdleState()
        {
            farmingState = CharacterFarmingState.Idle;
        }


        private void CheckPropertiesValue()
        {
            if (plantPrefab == null)
            {
                Debug.LogError("Plant prefab is not assigned in " + gameObject.name + ".");
            }

            if (_inventory == null)
            {
                Debug.LogError("Inventory is not assigned in " + gameObject.name + ".");
            }
        }
    }

    public enum CharacterFarmingState
    {
        Idle,
        Hoeing,
        Planting,
        Watering,
        Harvesting
    }
}
