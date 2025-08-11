using UnityEngine;

using Crafting;
using Management;
using GameUI;


namespace Characters
{
    public class Trader : MonoBehaviour
    {
        [SerializeField] private ItemScriptableObject[] _sellItems;
        [SerializeField] private Transform _storeUIPanel;

        private GameObject _itemInStorePrefab;

        void Awake()
        {
            _itemInStorePrefab = Resources.Load<GameObject>("Prefabs/ItemInStoreUI");

            CheckPropertiesValue();
        }


        void Start()
        {
            // TODO: initialize items only when character want to buy something from this npc
            InitializeSellItem();
        }


        private void InitializeSellItem()
        {
            foreach (var item in _sellItems)
            {
                // create item in store and set up its attributes
                var itemInStoreObj = ObjectPoolManager.SpawnObject(_itemInStorePrefab, _storeUIPanel);
                itemInStoreObj.GetComponent<ItemInStoreUI>().InitializeItemUI(item);
            }
        }


        private void CheckPropertiesValue()
        {
            if (_sellItems.Length == 0 ||
                _storeUIPanel == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }

            if (_itemInStorePrefab == null)
            {
                Debug.LogError("Can't load resources in " + gameObject.name + ".");
            }
        }
    }
}
