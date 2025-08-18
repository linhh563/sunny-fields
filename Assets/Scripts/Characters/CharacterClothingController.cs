using Management;
using Management.ScriptableObjects;
using UnityEngine;

namespace Characters
{
    public class CharacterClothingController : MonoBehaviour
    {
        [SerializeField] SpriteRenderer hat;
        [SerializeField] SpriteRenderer hair;
        [SerializeField] SpriteRenderer shirt;
        [SerializeField] SpriteRenderer pant;


        void Start()
        {
            CheckPropertiesValue();

            InitializeClothes();
        }


        void Update()
        {
            UpdateClothesDirection();
        }


        private void UpdateClothesDirection()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    hat.sprite = CharacterCustomizationStorage.hat.forwardSprite;
                    hair.sprite = CharacterCustomizationStorage.hair.forwardSprite;
                    shirt.sprite = CharacterCustomizationStorage.shirt.forwardSprite;
                    pant.sprite = CharacterCustomizationStorage.pant.forwardSprite;
                    break;

                case CharacterDirection.Up:
                    hat.sprite = CharacterCustomizationStorage.hat.behindSprite;
                    hair.sprite = CharacterCustomizationStorage.hair.behindSprite;
                    shirt.sprite = CharacterCustomizationStorage.shirt.behindSprite;
                    pant.sprite = CharacterCustomizationStorage.pant.behindSprite;
                    break;

                case CharacterDirection.Left:
                    hat.sprite = CharacterCustomizationStorage.hat.leftSprite;
                    hair.sprite = CharacterCustomizationStorage.hair.leftSprite;
                    shirt.sprite = CharacterCustomizationStorage.shirt.leftSprite;
                    pant.sprite = CharacterCustomizationStorage.pant.leftSprite;
                    break;

                case CharacterDirection.Right:
                    hat.sprite = CharacterCustomizationStorage.hat.rightSprite;
                    hair.sprite = CharacterCustomizationStorage.hair.rightSprite;
                    shirt.sprite = CharacterCustomizationStorage.shirt.rightSprite;
                    pant.sprite = CharacterCustomizationStorage.pant.rightSprite;
                    break;

                default:
                    break;
            }
        }


        private void InitializeClothes()
        {
            hat.sprite = CharacterCustomizationStorage.hat.forwardSprite;
            hair.sprite = CharacterCustomizationStorage.hair.forwardSprite;
            shirt.sprite = CharacterCustomizationStorage.shirt.forwardSprite;
            pant.sprite = CharacterCustomizationStorage.pant.forwardSprite;
        }


        private void CheckPropertiesValue()
        {
            if (hat == null || hair == null || shirt == null || pant == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
