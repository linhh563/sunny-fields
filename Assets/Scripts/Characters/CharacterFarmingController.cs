using Environment;
using Management;
using UnityEngine;

namespace Characters
{
    public class CharacterFarmingController : MonoBehaviour
    {
        public CharacterFarmingState farmingState { get; private set; }

        private TilemapManager _tilemapManager;

        private void Awake()
        {
            farmingState = CharacterFarmingState.Idle;

            _tilemapManager = FindObjectOfType<TilemapManager>();
        }

        private void Update() {
            UpdateFarmingState();
        }

        public void HoeGround()
        {
            var frontTile = _tilemapManager.GetTileInFrontCharacter();

            // character can only hoe the default ground
            // if (_tilemapManager.tilemap.GetTile(frontTile) != _tilemapManager.defaultGroundTile)
            //     return;

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
            if (!InputManager.Instance.IsCharacterFarming())
            {
                farmingState = CharacterFarmingState.Idle;
                return;
            } 

            // TODO: check character's holding item and update farming state

            // test
            farmingState = CharacterFarmingState.Hoeing;
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
