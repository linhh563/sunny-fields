using UnityEngine;
using Management;
using Management.Interface;
using Management.ScriptableObjects;
using GameUI;
using UnityEngine.Rendering.Universal;

namespace Characters
{
    public class NPCConversation : MonoBehaviour, IInteractable, ITalkable
    {
        // TODO: modify this property to array to store multiple conversations
        [SerializeField] ConversationScriptableObject _conversations;

        void Awake()
        {
            // TEST: subscribe input events
            GameplayInputManager.OnInteractButtonPress += Interact;
        }

        void OnDisable()
        {
            // TEST: unsubscribe event
            GameplayInputManager.OnInteractButtonPress -= Interact;
        }

        public void Interact()
        {
            Talk(_conversations);
        }

        public void Talk(ConversationScriptableObject conversation)
        {
            // start conversation
            GameplayUIManager.Instance.PlayNextDialogue(conversation);
        }
    }
}
