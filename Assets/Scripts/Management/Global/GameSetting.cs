using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Management
{
    public class GameSetting : MonoBehaviour
    {
        public static GameSetting Instance { get; private set; }

        public GameLanguage gameLanguage { get; private set; }
        public float bgmVolume { get; private set; }
        public float sfxVolume { get; private set; }

        public Dictionary<string, KeyCode> keyBindings { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            InitializeKeyBiding();

            DontDestroyOnLoad(this);
        }


        private void InitializeKeyBiding()
        {
            if (keyBindings == null)
            {
                keyBindings = new Dictionary<string, KeyCode>();
            }

            // get the game config file and check it's exist
            string path = Path.Combine(Application.dataPath, FilePath.CONFIG_FILE_PATH);
            if (!File.Exists(path))
            {
                Debug.LogError("Not found file");
                return;
            }

            // read the file and parse it to object
            string content = File.ReadAllText(path);
            var hotkeysWrapper = JsonUtility.FromJson<GameConfig>(content).hotkeysWrapper;

            // create the key bindings for each action
            keyBindings.Clear();
            foreach (var hotkey in hotkeysWrapper)
            {
                // assign key code to action if it's existed
                if (System.Enum.TryParse(hotkey.key, out KeyCode keyCode))
                {
                    keyBindings.Add(hotkey.action, keyCode);
                }
            }
        }


        public void ModifyKeyBiding(string keyName, KeyCode newKey)
        {
            // TODO: check if new key is existed

            keyBindings[keyName] = newKey;
        }
    }
}
