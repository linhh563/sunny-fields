using Management.ScriptableObjects;
using UnityEngine;

namespace Management
{
    // use to store the character customization that player custom in character customization UI, or the one in saved farm.
    public class CharacterCustomizationStorage : MonoBehaviour
    {
        public static ClotheScriptableObject hair { get; private set; }
        public static ClotheScriptableObject hat { get; private set; }
        public static ClotheScriptableObject shirt { get; private set; }
        public static ClotheScriptableObject pant { get; private set; }

        public static string farmName;
        public static string characterName;


        void Awake()
        {
            ResetCustomization();

            CharacterCustomization.OnClotheChanged += ChangeClothe;

            DontDestroyOnLoad(this);
        }


        private void ResetCustomization()
        {
            hair = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hairs/Hair 1");
            hat = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hats/Hat 1");
            shirt = Resources.Load<ClotheScriptableObject>("Items/Clothes/Shirts/Shirt 1");
            pant = Resources.Load<ClotheScriptableObject>("Items/Clothes/Pants/Pant 1");
        }


        public static void SetFarmAttribute(string characterName, string farmName)
        {
            CharacterCustomizationStorage.characterName = characterName;
            CharacterCustomizationStorage.farmName = farmName;
        }


        public static void SetupClothes(string hatName, string hairName, string shirtName, string pantName)
        {
            var newHat = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hats/" + hatName);
            if (newHat != null)
                hat = newHat;

            var newHair = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hairs/" + hairName);
            if (newHair != null)
                hair = newHair;

            var newShirt = Resources.Load<ClotheScriptableObject>("Items/Clothes/Shirts/" + shirtName);
            if (newShirt != null)
                shirt = newShirt;

            var newPant = Resources.Load<ClotheScriptableObject>("Items/Clothes/Pants/" + pantName);
            if (newPant != null)
                pant = newPant;
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
                    hair = newClothe;
                    break;

                case ClotheType.Hat:
                    hat = newClothe;
                    break;

                case ClotheType.Shirt:
                    shirt = newClothe;
                    break;

                case ClotheType.Pant:
                    pant = newClothe;
                    break;

                default:
                    break;
            }
        }
    }
}
