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

            InitializeGameSetting();

            DontDestroyOnLoad(this);
        }


        private void InitializeGameSetting()
        {
            // load key binding from config file
            if (keyBindings == null)
            {
                keyBindings = new Dictionary<string, KeyCode>();
            }

            var hotkeysWrapper = GameConfig.LoadGameConfig().hotkeysWrapper;

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


            // load game language from config file
            gameLanguage = GameConfig.LoadGameConfig().language;

            // load volume from config file
            bgmVolume = GameConfig.LoadGameConfig().bgmVolume;
            sfxVolume = GameConfig.LoadGameConfig().sfxVolume;
        }


        public void ModifyGameLanguage(GameLanguage newLanguage)
        {
            gameLanguage = newLanguage;

            // update game language in game config file
            var config = GameConfig.LoadGameConfig();
            config.language = newLanguage;

            config.SaveConfig();

            // TODO: CHANGE LANGUAGE IN GAME
        }


        // return true if new key can assign for the action
        public bool ModifyKeyBiding(string keyName, KeyCode newKey)
        {
            // check if new key is existed
            if (keyBindings.ContainsValue(newKey)) return false;

            // player can't assign mouse button for action
            if (newKey == KeyCode.Mouse0 ||
                newKey == KeyCode.Mouse1 ||
                newKey == KeyCode.Mouse2 ||
                newKey == KeyCode.Mouse3 ||
                newKey == KeyCode.Mouse4 ||
                newKey == KeyCode.Mouse5 ||
                newKey == KeyCode.Mouse6)
            {
                return false;
            }

            keyBindings[keyName] = newKey;

            // create the new hotkey list
            List<HotKeyEntry> newHotkeys = new List<HotKeyEntry>();
            foreach (var pair in keyBindings)
            {
                newHotkeys.Add(new HotKeyEntry()
                {
                    action = pair.Key,
                    key = pair.Value.ToString()
                });
            }

            // assign the hotkeys attribute from the game config object by new hotkey list
            var config = GameConfig.LoadGameConfig();
            config.hotkeysWrapper = newHotkeys;

            config.SaveConfig();
            return true;
        }


        public void ModifyBackgroundVolume(float newVolume)
        {
            bgmVolume = newVolume;

            var config = GameConfig.LoadGameConfig();
            config.bgmVolume = bgmVolume;

            config.SaveConfig();
        }


        public void ModifySoundVolume(float newVolume)
        {
            sfxVolume = newVolume;

            var config = GameConfig.LoadGameConfig();
            config.sfxVolume = sfxVolume;

            config.SaveConfig();
        }
    }
}
