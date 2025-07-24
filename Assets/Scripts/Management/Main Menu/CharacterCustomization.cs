using Management.ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class CharacterCustomization : MonoBehaviour
    {
        public static int currentHairIndex { get; private set; }
        public static int currentHatIndex { get; private set; }
        public static int currentShirtIndex { get; private set; }
        public static int currentPantIndex { get; private set; }

        private static Object[] _hairSelections = null;
        private static Object[] _hatSelections = null;
        private static Object[] _shirtSelections = null;
        private static Object[] _pantSelections = null;

        void Awake()
        {
            LoadItemsFromResources();
            InitializeProperties();
        }

        private void LoadItemsFromResources()
        {
            _hairSelections = Resources.LoadAll("Items/Clothes/Hair", typeof(ClotheScriptableObject));
            _hatSelections = Resources.LoadAll("Items/Clothes/Hats", typeof(ClotheScriptableObject));
            _shirtSelections = Resources.LoadAll("Items/Clothes/Shirts", typeof(ClotheScriptableObject));
            _pantSelections = Resources.LoadAll("Items/Clothes/Pants", typeof(ClotheScriptableObject));

            if (_hairSelections == null
            || _hatSelections == null
            || _shirtSelections == null
            || _pantSelections == null)
            {
                Debug.LogError("Error in loading clothes from resources!");
            }
        }

        private void InitializeProperties()
        {
            currentHairIndex = 0;
            currentHatIndex = 0;
            currentShirtIndex = 0;
            currentPantIndex = 0;
        }

        public static ClotheScriptableObject NextHat()
        {
            currentHatIndex++;
            if (currentHatIndex > _hatSelections.Length - 1)
            {
                currentHatIndex = 0;
            }

            return _hatSelections[currentHatIndex] as ClotheScriptableObject;
        }

        public static void NextHair()
        {

        }

        public static void NextShirt()
        {

        }

        public static void NextPant()
        {

        }
    }
}