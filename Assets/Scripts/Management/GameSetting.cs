using UnityEngine;

namespace Management
{
    public class GameSetting : MonoBehaviour
    {
        public static GameSetting Instance { get; private set; }

        public float bgmVolume { get; private set; }
        public float sfxVolume { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }
    }
}
