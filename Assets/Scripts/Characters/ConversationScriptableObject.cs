using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Conversation")]
    public class ConversationScriptableObject : ScriptableObject
    {
        public string NPCName;
        public Sprite NPCAvatar;
        public bool npcTalkFirst;
        public string[] NPCDialogues;
        public string[] mainCharacterDialogues;
    }
}
