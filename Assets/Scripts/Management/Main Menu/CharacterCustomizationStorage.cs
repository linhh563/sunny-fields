using Management.ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class CharacterCustomizationStorage : MonoBehaviour
    {
        private ClotheScriptableObject _hair;
        private ClotheScriptableObject _hat;
        private ClotheScriptableObject _shirt;
        private ClotheScriptableObject _pant;

        void Awake()
        {
            ResetCustomization();

            CharacterCustomization.OnClotheChanged += ChangeClothe;

            DontDestroyOnLoad(this);
        }

        private void ResetCustomization()
        {
            _hair = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hair/Hair 1");
            _hat = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hats/Hat 1");
            _shirt = Resources.Load<ClotheScriptableObject>("Items/Clothes/Shirts/Shirt 1");
            _pant = Resources.Load<ClotheScriptableObject>("Items/Clothes/Pants/Pant 1");
        }

        public void ChangeClothe(ClotheScriptableObject newClothe, ClotheType type)
        {
            if (newClothe == null)
            {
                Debug.LogError("Can't convert the ClotheScriptableObject.");
                return;
            }

            switch (type)
            {
                case ClotheType.Hair:
                    _hair = newClothe;
                    break;

                case ClotheType.Hat:
                    _hat = newClothe;
                    break;

                case ClotheType.Shirt:
                    _shirt = newClothe;
                    break;

                case ClotheType.Pant:
                    _pant = newClothe;
                    break;

                default:
                    break;
            }
        }
    }
}
