using Management.ScriptableObjects;

namespace Management.Interface
{
    public interface ITalkable
    {
        public void Talk(ConversationScriptableObject conversation);
    }
}