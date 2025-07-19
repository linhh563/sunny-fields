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
        private CharacterInventory _characterInventory;
        [SerializeField] private GameObject plantPrefab;

        private void Awake()
        {
            farmingState = CharacterFarmingState.Idle;
            _characterInventory = GetComponentInChildren<CharacterInventory>();
        }

        void Start()
        {
            _tilemapManager = FindObjectOfType<TilemapManager>();

            // Subscribe input events
            GameplayInputManager.OnFarmingButtonPress += UpdateFarmingState;
            GameplayInputManager.OnFarmingButtonRelease += SetIdleState;

            // check in editor config properties
            if (plantPrefab == null)
            {
                Debug.LogError("Plant prefab is null");
            }
        }

        private void Update()
        {
        }

        void OnDisable()
        {
            // Unsubscribe input events
            GameplayInputManager.OnFarmingButtonPress -= UpdateFarmingState;
            GameplayInputManager.OnFarmingButtonRelease -= SetIdleState;
        }

        public void HoeGround()
        {
            var frontTilePos = _tilemapManager.GetTileInFrontCharacter();
            var frontTile = _tilemapManager.groundTilemap.GetTile(frontTilePos);

            // character can only hoe the default ground
            if (_tilemapManager.defaultGroundTile.name != frontTile.name && _tilemapManager.defaultGroundTile_2.name != frontTile.name)
            {
                return;
            }

            // hoeing logic
            _tilemapManager.groundTilemap.SetTile(frontTilePos, _tilemapManager.hoedGroundTile);
        }

        public void Planting()
        {
            var frontTilePos = _tilemapManager.GetTileInFrontCharacter();

            var frontTilePlantingMap = _tilemapManager.plantingTilemap.GetTile(frontTilePos);
            var frontTileGroundMap = _tilemapManager.groundTilemap.GetTile(frontTilePos);

            // check if the ground was hoed and not planted yet
            if (frontTilePlantingMap == null && frontTileGroundMap.name == _tilemapManager.hoedGroundTile.name)
            {
                // planting logic
                // TODO: use objects pooling to improve performance
                var plant = Instantiate(plantPrefab, _tilemapManager.groundTilemap.GetCellCenterWorld(frontTilePos), Quaternion.identity);                
                plant.GetComponent<Plant>().Initialize(_characterInventory.GetHoldingItem() as PlantScriptableObject);

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
            var _item = _characterInventory.GetHoldingItem();

            // TODO: check if holding item is plant seed
            if (_item.GetType() == typeof(PlantScriptableObject))
            {
                farmingState = CharacterFarmingState.Planting;
                return;
            }

            switch (_item.itemName)
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
