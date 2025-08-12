using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Management
{
    [System.Serializable]
    public class FarmConfig
    {
        public string characterName;
        public string farmName;
        public double gameTimeMinutes;
        public Vector3 characterPosition;
        public CharacterDirection characterDirection;
        // TODO: store character's clothes

        public List<GroundConfig> groundStates;
        public List<ItemConfig> inventory;
        public List<PlantConfig> plants;


        public FarmConfig(string newCharacterName, string newFarmName)
        {
            characterName = newCharacterName;
            farmName = newFarmName;

            gameTimeMinutes = 0f;

            characterPosition = Vector3.zero;
            characterDirection = CharacterDirection.Down;

            groundStates = new List<GroundConfig>();
            inventory = new List<ItemConfig>();
            plants = new List<PlantConfig>();
        }


        public static FarmConfig LoadFarmConfigByFileName(string fileName)
        {
            // load farm config file from resources & check if it existed
            string filePath = Path.Combine(Application.dataPath, FilePath.FARMS_FOLDER_PATH + "/" + fileName + ".json");

            return LoadFarmConfigByPath(filePath);
        }


        public static FarmConfig LoadFarmConfigByPath(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError("Farm file doesn't existed!");
                return null;
            }

            // read file and convert it to object format
            string content = File.ReadAllText(path);
            var farmConfig = JsonUtility.FromJson<FarmConfig>(content);

            return farmConfig;
        }


        public void SaveFarmConfig()
        {
            string content = JsonUtility.ToJson(this, true);

            string path = Path.Combine(Application.dataPath, FilePath.FARMS_FOLDER_PATH + "/" + farmName + ".json");
            File.WriteAllText(path, content);
        }
    }


    [System.Serializable]
    public class GroundConfig
    {
        public Vector3Int position;
        public GroundState state;
    }


    [System.Serializable]
    public class ItemConfig
    {
        public string itemName;
        public int quantity;
    }


    [System.Serializable]
    public class PlantConfig
    {
        public string plantName;
        public Vector3Int position;
        public int dayAge;
        public bool isWatered;
    }
}
