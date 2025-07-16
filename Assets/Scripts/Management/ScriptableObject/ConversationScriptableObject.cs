using UnityEngine;

namespace Management.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Conversation")]
    public class ConversationScriptableObject : ScriptableObject
    {
        public string NPCName;
        // public Sprite NPCAvatar;
        // public bool npcTalkFirst;

        [TextArea(3, 20)]
        public string[] NPCParagraphs;
        // public string[] mainCharacterDialogues;
    }
}
