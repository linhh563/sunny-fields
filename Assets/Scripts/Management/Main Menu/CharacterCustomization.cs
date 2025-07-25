using GameUI;
using Management.ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class CharacterCustomization : MonoBehaviour
    {
        public static int _currentHairIndex { get; private set; }
        public static int _currentHatIndex { get; private set; }
        public static int _currentShirtIndex { get; private set; }
        public static int _currentPantIndex { get; private set; }

        public delegate void ChangeClotheEvent(ClotheScriptableObject newClothe, ClotheType type);
        public static event ChangeClotheEvent OnClotheChanged;

        public static Object[] _hairCollection { get; private set; }
        public static Object[] _hatCollection { get; private set; }
        public static Object[] _shirtCollection { get; private set; }
        public static Object[] _pantCollection { get; private set; }

        void Awake()
        {
            LoadItemsFromResources();
            ResetClothesIndex();

            // TESTING
            CharacterCustomizationUI.OnCustomizeButtonClicked += ChangeClothe;
        }

        private void LoadItemsFromResources()
        {
            _hairCollection = Resources.LoadAll("Items/Clothes/Hair", typeof(ClotheScriptableObject));
            _hatCollection = Resources.LoadAll("Items/Clothes/Hats", typeof(ClotheScriptableObject));
            _shirtCollection = Resources.LoadAll("Items/Clothes/Shirts", typeof(ClotheScriptableObject));
            _pantCollection = Resources.LoadAll("Items/Clothes/Pants", typeof(ClotheScriptableObject));

            if (_hairCollection == null
            || _hatCollection == null
            || _shirtCollection == null
            || _pantCollection == null)
            {
                Debug.LogError("Error in loading clothes from resources!");
            }
        }

        public static void ResetClothesIndex()
        {
            _currentHairIndex = 0;
            _currentHatIndex = 0;
            _currentShirtIndex = 0;
            _currentPantIndex = 0;
        }

        public void ChangeClothe(ClotheType clotheType, bool next)
        {
            switch (clotheType)
            {
                case ClotheType.Hair:
                    ChangeHairIndex(next);
                    OnClotheChanged?.Invoke(_hairCollection[_currentHairIndex] as ClotheScriptableObject, ClotheType.Hair);
                    break;

                case ClotheType.Hat:
                    ChangeHatIndex(next);
                    OnClotheChanged?.Invoke(_hatCollection[_currentHatIndex] as ClotheScriptableObject, ClotheType.Hat);
                    break;

                case ClotheType.Shirt:
                    ChangeShirtIndex(next);
                    OnClotheChanged?.Invoke(_shirtCollection[_currentShirtIndex] as ClotheScriptableObject, ClotheType.Shirt);
                    break;

                case ClotheType.Pant:
                    ChangePantIndex(next);
                    OnClotheChanged?.Invoke(_pantCollection[_currentPantIndex] as ClotheScriptableObject, ClotheType.Pant);
                    break;

                default:
                    break;
            }
        }

        private void ChangeHatIndex(bool next)
        {
            _currentHatIndex = next ? _currentHatIndex + 1 : _currentHatIndex - 1;

            if (_currentHatIndex > _hatCollection.Length - 1)
            {
                _currentHatIndex = 0;
            }
            else if (_currentHatIndex < 0)
            {
                _currentHatIndex = _hatCollection.Length - 1;
            }
        }

        private void ChangeHairIndex(bool next)
        {
            _currentHairIndex = next ? _currentHairIndex + 1 : _currentHairIndex - 1;

            if (_currentHairIndex > _hairCollection.Length - 1)
            {
                _currentHairIndex = 0;
            }
            else if (_currentHairIndex < 0)
            {
                _currentHairIndex = _hairCollection.Length - 1;
            }
        }

        private void ChangeShirtIndex(bool next)
        {
            _currentShirtIndex = next ? _currentShirtIndex + 1 : _currentShirtIndex - 1;

            if (_currentShirtIndex > _shirtCollection.Length - 1)
            {
                _currentShirtIndex = 0;
            }
            else if (_currentShirtIndex < 0)
            {
                _currentShirtIndex = _shirtCollection.Length - 1;
            }
        }

        private void ChangePantIndex(bool next)
        {
            _currentPantIndex = next ? _currentPantIndex + 1 : _currentPantIndex - 1;

            if (_currentPantIndex > _pantCollection.Length - 1)
            {
                _currentPantIndex = 0;
            }
            else if (_currentPantIndex < 0)
            {
                _currentPantIndex = _pantCollection.Length - 1;
            }
        }
    }
}