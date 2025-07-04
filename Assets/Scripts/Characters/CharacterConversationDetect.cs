using UnityEngine;
using Management;
using GameUI;

namespace Characters
{
    public class CharacterConversationDetect : MonoBehaviour
    {
        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "NPC")
            {
                // compare the x position of main character and npc to enable respective options ui
                var isLeft = (transform.position.x < collision.transform.position.x) ? true : false;
                GameplayUIManager.Instance.EnableCharacterOptionsUI(true, isLeft);

            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "NPC")
            {
                GameplayUIManager.Instance.EnableCharacterOptionsUI(false, true);
            }
        }
    }
}