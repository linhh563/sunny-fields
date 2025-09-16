using UnityEngine;
using UnityEngine.Audio;


namespace Management
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioMixer _audioMixer;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _backGroundMusic;
        [SerializeField] private AudioSource _sfxMusic;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip _bgmClip;

        [SerializeField] private AudioClip _hoeSfx;
        [SerializeField] private AudioClip _waterSfx;
        [SerializeField] private AudioClip _pressButtonSfx;

        public AudioClip hoeSfx { get => _hoeSfx; }
        public AudioClip waterSfx { get => _waterSfx; }
        public AudioClip pressButtonSfx { get => _pressButtonSfx; }


        void Awake()
        {
            CreateInstance();            
        }


        void Start()
        {
            CheckPropertiesValue();

            PlayBgm();
        }


        private void PlayBgm()
        {
            _backGroundMusic.clip = _bgmClip;
            _backGroundMusic.Play();
        }


        public void PlaySFX(AudioClip audio)
        {
            _sfxMusic.PlayOneShot(audio);
        }


        public void ModifyMusicVolume(float volume)
        {
            _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        }


        public void ModifySfxVolume(float volume)
        {
            _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }


        private void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }


        private void CheckPropertiesValue()
        {
            if (_backGroundMusic == null ||
                _sfxMusic == null ||
                _bgmClip == null ||
                _audioMixer == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
