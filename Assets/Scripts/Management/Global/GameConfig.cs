using System.Collections.Generic;

namespace Management
{
    [System.Serializable]
    public class GameConfig
    {
        public List<HotKeyEntry> hotkeysWrapper;
    }

    [System.Serializable]
    public class HotKeyEntry
    {
        public string action;
        public string key;
    }
}