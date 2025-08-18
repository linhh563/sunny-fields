using UnityEngine;
using Management;
using Crafting;
using GameUI;

namespace Characters
{
    public class CharacterInteractionDetect : MonoBehaviour
    {
        public delegate bool CollectItemEvent(ItemScriptableObject scriptableObj);
        public static event CollectItemEvent OnCollectItem;


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Collectable"))
            {
                // add new item to character inventory
                var itemScriptableObj = collision.gameObject.GetComponent<CollectableItem>().itemScriptableObject;
                if (itemScriptableObj == null) return;

                collision.gameObject.SetActive(false);

                OnCollectItem?.Invoke(itemScriptableObj);
            }
        }


        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                // compare the x position of main character and npc to enable respective options ui
                var isLeft = (transform.position.x < collision.transform.position.x) ? true : false;
                GameplayUIManager.Instance.EnableCharacterOptionsUI(true, isLeft, collision.gameObject.name, CharacterInteractType.NPC);
            }

            if (collision.gameObject.CompareTag("Bed"))
            {
                // compare the x position of main character and the bed to enable respective options ui
                var isLeft = (transform.position.x < collision.transform.position.x) ? true : false;
                GameplayUIManager.Instance.EnableCharacterOptionsUI(true, isLeft, collision.gameObject.name, CharacterInteractType.Item);
            }
        }


        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                GameplayUIManager.Instance.DisableCharacterOptionUI();
            }

            if (collision.gameObject.CompareTag("Bed"))
            {
                GameplayUIManager.Instance.DisableCharacterOptionUI();
            }
        }
    }
}