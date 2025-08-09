using System.Collections.Generic;
using UnityEngine;

namespace Management.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Conversation")]
    public class ConversationScriptableObject : ScriptableObject
    {
        public string NPCName;
        public Sprite NPCAvatar;
        public string[] dialogueLines;
        public List<int> endConversationIndex;
        
        public ConversationDecision[] decisions;
    }


    [System.Serializable]
    public class ConversationDecision
    {
        // the dialogue index that the decisions appear
        public int dialogueIndex;
        public string[] decisions;
        public DecisionType[] decisionTypes;
        public int[] nextDialogueIndex;
    }
}
