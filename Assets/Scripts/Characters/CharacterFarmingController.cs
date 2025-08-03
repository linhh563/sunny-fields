using UnityEngine;
using Environment;
using Management;
using Crafting;
using UnityEngine.Tilemaps;


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

            // check in editor config properties
            if (plantPrefab == null)
            {
                Debug.LogError("Plant prefab is not assigned in " + gameObject.name + ".");
            }

            if (_inventory == null)
            {
                Debug.LogError("Inventory is not assigned in " + gameObject.name + ".");
            }
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
            {
                return;
            }

            // spawn hoed tile in front of the character in ground tile map
            _tilemapManager.groundTilemap.SetTile(frontTilePos, _tilemapManager.hoedGroundTile);
        }

        public void Planting()
        {
            var frontTilePos = _tilemapManager.GetTileInFrontCharacter();

            var frontTilePlantingMap = _tilemapManager.plantingTilemap.GetTile(frontTilePos);
            var frontTileGroundMap = _tilemapManager.groundTilemap.GetTile(frontTilePos);

            var worldFrontPos = _tilemapManager.groundTilemap.GetCellCenterWorld(frontTilePos);

            // if the ground had not hoed, player can't plant
            if (frontTileGroundMap == null) return;

            // check if the ground was hoed and not planted yet
            if (frontTilePlantingMap == null && frontTileGroundMap.name == _tilemapManager.hoedGroundTile.name)
            {
                // spawn new plant and set up its property
                GameObject plant = ObjectPoolManager.SpawnObject(plantPrefab, worldFrontPos, Quaternion.identity, ObjectPoolType.Plant);
                plant.GetComponent<Plant>().Initialize(_inventory.GetHoldingItem() as PlantScriptableObject);

                // decrease item count by 1
                _inventory.ConsumeItem();

                // marking the tile has planted
                _tilemapManager.plantingTilemap.SetTile(frontTilePos, _tilemapManager.whiteTile);
            }
        }

        public void Watering()
        {
            // TODO: watering logic
        }

        public void Harvesting()
        {
            // TODO: harvesting logic
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
                case "WaterCan":
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
