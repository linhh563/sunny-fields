using System.Collections.Generic;
using Crafting;
using UnityEngine;


namespace Management
{
    public class PlantManager : MonoBehaviour
    {
        public static Dictionary<Vector3Int, Plant> plantList = new Dictionary<Vector3Int, Plant>();


        public static void AddPlant(Vector3Int position, Plant plant)
        {
            plantList.Add(position, plant);
        }
    }
}
