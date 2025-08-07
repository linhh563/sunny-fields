using System.Collections.Generic;
using UnityEngine;


namespace Management
{
    [System.Serializable]
    public class FarmConfig
    {
        public string farmName;
        public double playTimeSeconds;
        public Vector3 characterPosition;
        public CharacterDirection characterDirection;
        // TODO: store character's clothes
        
        public List<GroundConfig> groundStates;
        public List<ItemConfig> inventory;
        public List<PlantConfig> plants;
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
