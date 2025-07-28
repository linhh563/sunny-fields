using Management.ScriptableObjects;
using UnityEngine;

namespace Management
{
    // ===== SUMMARY =====
    //      Use to store the character customization that player custom in character customization UI, or the one in saved farm.
    // ===================
    public class CharacterCustomizationStorage : MonoBehaviour
    {
        public static ClotheScriptableObject hair { get; private set; }
        public static ClotheScriptableObject hat { get; private set; }
        public static ClotheScriptableObject shirt { get; private set; }
        public static ClotheScriptableObject pant { get; private set; }

        void Awake()
        {
            ResetCustomization();

            CharacterCustomization.OnClotheChanged += ChangeClothe;

            DontDestroyOnLoad(this);
        }

        private void ResetCustomization()
        {
            hair = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hair/Hair 1");
            hat = Resources.Load<ClotheScriptableObject>("Items/Clothes/Hats/Hat 1");
            shirt = Resources.Load<ClotheScriptableObject>("Items/Clothes/Shirts/Shirt 1");
            pant = Resources.Load<ClotheScriptableObject>("Items/Clothes/Pants/Pant 1");
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
