using Crafting;
using Environment;
using UnityEngine;


namespace Management
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private Characters.CharacterController _characterController;
        [SerializeField] private TilemapManager _tileMapManager;
        [SerializeField] private InventoryManager _inventoryManager;

        void Start()
        {
            CheckPropertiesValue();
            
            SetUpGameData();
        }


        private void SetUpGameData()
        {
            FarmConfig farmConfig = FarmConfig.LoadFarmConfigByFileName(CharacterCustomizationStorage.farmName);

            // set up character direction and position
            _characterController.Initialize(farmConfig.characterPosition, farmConfig.characterDirection);

            // set up ground state
            foreach (var tile in farmConfig.groundStates)
            {
                _tileMapManager.SetGroundTile(tile.position, tile.state);
            }

            // set up inventory
            InventoryManager.gold = farmConfig.gold;
            foreach (var item in farmConfig.inventory)
            {
                var itemObj = Resources.Load<ItemScriptableObject>("Items/" + item.itemName);

                _inventoryManager.AddItemToSlot(itemObj, item.slotIndex, item.quantity);
            }
        }


        public static void PauseGame()
        {
            Time.timeScale = 0f;
        }


        public static void ResumeGame()
        {
            Time.timeScale = 1;
        }


        private void CheckPropertiesValue()
        {
            if (_characterController == null ||
                _tileMapManager == null ||
                _inventoryManager == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
