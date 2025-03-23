using Management;
using UnityEngine;

namespace Characters
{
    public class CharacterFarmingController : MonoBehaviour
    {
        public CharacterFarmingState farmingState { get; private set; }

        private void Awake()
        {
            farmingState = CharacterFarmingState.Idle;
        }

        private void Update() {
            UpdateFarmingState();
        }

        public void Hoeing()
        {
            // TODO: get the tile in front of the character
            // TODO: check if that tile can hoe and change state of that tile
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
                return;

            // TODO: check character's holding item and update farming state
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
