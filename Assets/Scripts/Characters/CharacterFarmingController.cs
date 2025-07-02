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

        private CharacterInventory _characterInventory;

        private void Awake()
        {
            farmingState = CharacterFarmingState.Idle;

            _tilemapManager = FindObjectOfType<TilemapManager>();

            _characterInventory = GetComponentInChildren<CharacterInventory>();
        }

        void Start()
        {
            // Subscribe input events
            GameplayInputManager.OnFarmingButtonPress += UpdateFarmingState;
            GameplayInputManager.OnFarmingButtonRelease += SetIdleState;
        }

        private void Update() {
        }

        void OnDisable()
        {
            // Unsubscribe input events
            GameplayInputManager.OnFarmingButtonPress -= UpdateFarmingState;
            GameplayInputManager.OnFarmingButtonRelease -= SetIdleState;
        }

        public void HoeGround()
        {
            var frontTile = _tilemapManager.GetTileInFrontCharacter();

            // TODO: character can only hoe the default ground

            _tilemapManager.groundTilemap.SetTile(frontTile, _tilemapManager.hoedGroundTile);
        }

        public void Planting()
        {
            // TODO: planting logic
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
