using Management;
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

            hat.sprite = CharacterCustomizationStorage.hat.forwardSprite;
            hair.sprite = CharacterCustomizationStorage.hair.forwardSprite;
            shirt.sprite = CharacterCustomizationStorage.shirt.forwardSprite;
            pant.sprite = CharacterCustomizationStorage.pant.forwardSprite;
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
                    break;

                case CharacterDirection.Up:
                    hat.sprite = CharacterCustomizationStorage.hat.behindSprite;
                    break;

                case CharacterDirection.Left:
                    hat.sprite = CharacterCustomizationStorage.hat.leftSprite;
                    break;

                case CharacterDirection.Right:
                    hat.sprite = CharacterCustomizationStorage.hat.rightSprite;
                    break;

                default:
                    break;
            }
        }

        private void CheckPropertiesValue()
        {
            if (hat == null || hair == null || shirt == null || pant == null)
            {
                Debug.LogError("There is a sprite renderer was not assigned in " + gameObject.name + ".");
                return;
            }
        }
    }
}
