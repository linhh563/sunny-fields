using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Management
{
    [System.Serializable]
    public class GameConfig
    {
        public GameLanguage language;
        public float bgmVolume;
        public float sfxVolume;
        public List<HotKeyEntry> hotkeysWrapper;


        // load game config from file and return it as object
        public static GameConfig LoadGameConfig()
        {
            // get the game config file and check it's exist
            string path = Path.Combine(Application.dataPath, FilePath.CONFIG_FILE_PATH);
            if (!File.Exists(path))
            {
                Debug.LogError("Not found file");
                return null;
            }

            // read the file and parse it to object
            string content = File.ReadAllText(path);
            var config = JsonUtility.FromJson<GameConfig>(content);

            return config;
        }


        // save new game config to file
        public static void SaveConfig(GameConfig config)
        {
            string content = JsonUtility.ToJson(config, true);
            string path = Path.Combine(Application.dataPath, FilePath.CONFIG_FILE_PATH);
            File.WriteAllText(path, content);
        }
    }

    [System.Serializable]
    public class HotKeyEntry
    {
        public string action;
        public string key;
    }
}