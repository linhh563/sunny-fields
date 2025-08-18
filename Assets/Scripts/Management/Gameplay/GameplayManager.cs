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

            // set up game time
            TimeManager.SetupTime(farmConfig.gameTimeMinutes);

            // set up clothes
            CharacterCustomizationStorage.SetupClothes(farmConfig.hat, farmConfig.hair, farmConfig.shirt, farmConfig.pant);

            // set up ground state
            foreach (var tile in farmConfig.groundStates)
            {
                _tileMapManager.SetGroundTile(tile.position, tile.state);
            }

            // set up inventory
            InventoryManager.gold = farmConfig.gold;

            // set up holding item
            if (farmConfig.holdingItem.itemName != "")
            {
                var itemObj = Resources.Load<ItemScriptableObject>("Items/" + farmConfig.holdingItem.itemName);

                _inventoryManager.SetHoldingItem(itemObj, farmConfig.holdingItem.quantity);
            }

            // set up all items in inventory
            foreach (var item in farmConfig.inventory)
            {
                var itemObj = Resources.Load<ItemScriptableObject>("Items/" + item.itemName);

                _inventoryManager.AddItemToSlot(itemObj, item.slotIndex, item.quantity);
            }

            // set up plant
            var plants = farmConfig.plants;

            // get plant prefab
            GameObject plantPrefab = Resources.Load<GameObject>("Prefabs/Plant");

            foreach (var plant in plants)
            {
                var worldPos = _tileMapManager.groundTilemap.GetCellCenterWorld(plant.position);
                var plantScriptableObj = Resources.Load<PlantScriptableObject>("Items/" + plant.plantName);

                // create new plant and set up it
                var newPlant = ObjectPoolManager.SpawnObject(plantPrefab, worldPos, Quaternion.identity, ObjectPoolType.Plant);
                newPlant.GetComponent<Plant>().SetupPlant(plantScriptableObj, plant.position, plant.dayAge, plant.isWatered);
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
