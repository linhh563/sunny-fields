using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

using Management;
using Environment;
using System.Linq;

namespace GameUI
{
    public class GameplaySettingUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;
        [SerializeField] private Button _saveGameButton;
        [SerializeField] private List<Button> _hotkeys;

        [Header("Dropdowns & Sliders")]
        [SerializeField] private TMP_Dropdown _languageDropdown;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        [Header("Game Objects")]
        [SerializeField] private GameObject _modifyHotKeyUI;
        [SerializeField] private InventoryManager _inventoryManager;


        void Start()
        {
            CheckPropertiesValue();
        }

        void OnEnable()
        {
            InitializeGameplaySettingUI();
            ModifyHotKeyUI.OnKeyChanged += UpdateHotKey;

            AddListeners();
        }

        void OnDisable()
        {
            RemoveAllListeners();
            ModifyHotKeyUI.OnKeyChanged -= UpdateHotKey;

            _modifyHotKeyUI.gameObject.SetActive(false);
        }


        private void InitializeGameplaySettingUI()
        {
            InitializeGameLanguage();
            InitializeVolumeSlider();
            UpdateHotKey();
        }


        private void InitializeGameLanguage()
        {
            _languageDropdown.ClearOptions();
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData(GameLanguage.Vietnamese.ToString(), null));
            _languageDropdown.options.Add(new TMP_Dropdown.OptionData(GameLanguage.English.ToString(), null));

            _languageDropdown.value = (int)GameSetting.Instance.gameLanguage;
        }


        private void InitializeVolumeSlider()
        {
            _bgmSlider.value = GameSetting.Instance.bgmVolume;
            _sfxSlider.value = GameSetting.Instance.sfxVolume;
        }


        private void UpdateHotKey()
        {
            foreach (var hotkey in _hotkeys)
            {
                // get tmp text of hotkey button
                TMP_Text keyText;
                if (hotkey.gameObject.transform.childCount == 0) break;
                keyText = hotkey.gameObject.GetComponentInChildren<TMP_Text>();

                // get game obj name
                var keyName = hotkey.gameObject.name;

                // check and set text equal key binding [obj name]
                if (GameSetting.Instance.keyBindings.ContainsKey(keyName))
                {
                    keyText.SetText(GameSetting.Instance.keyBindings[keyName].ToString());
                }
            }
        }


        private void OnModifyKeyPress(string keyName)
        {
            _modifyHotKeyUI.GetComponentInChildren<ModifyHotKeyUI>().InitializeUI(keyName);
            _modifyHotKeyUI.gameObject.SetActive(true);
        }


        private void OnLanguageChanged(int index)
        {
            GameSetting.Instance.ModifyGameLanguage((GameLanguage)index);
        }


        private void OnBgmVolumeChanged(float value)
        {
            GameSetting.Instance.ModifyBackgroundVolume(value);
        }


        private void OnSfxVolumeChanged(float value)
        {
            GameSetting.Instance.ModifySoundVolume(value);
        }


        private void SaveGame()
        {
            // load farm config and modify it by current farm state
            var farmConfig = FarmConfig.LoadFarmConfigByFileName(CharacterCustomizationStorage.farmName);

            farmConfig.gameTimeMinutes = TimeManager.GetGameTime();
            farmConfig.characterPosition = Characters.CharacterController.characterWorldPosition;
            farmConfig.characterDirection = Characters.CharacterController.currentDirection;

            // update grounds state
            var grounds = new List<GroundConfig>();
            foreach (var hoedGround in TilemapManager.hoedGrounds)
            {
                if (TilemapManager.wateredGrounds.Contains(hoedGround))
                {
                    grounds.Add(new GroundConfig
                    {
                        position = hoedGround,
                        state = GroundState.Watered
                    });
                }
                else
                {
                    grounds.Add(new GroundConfig
                    {
                        position = hoedGround,
                        state = GroundState.Hoed
                    });
                }
            }

            farmConfig.groundStates = grounds;

            // update inventory
            var holdingItem = _inventoryManager.holdingItem.GetComponentInChildren<DragableItem>();
            if (holdingItem != null)
            {
                farmConfig.holdingItem = new ItemConfig
                {
                    itemName = holdingItem.itemScriptableObj.itemName,
                    quantity = holdingItem.count
                };
            }

            var items = new List<ItemConfig>();
            for (int i = 0; i < _inventoryManager.inventorySlots.Length; i++)
            {
                var slot = _inventoryManager.inventorySlots[i];

                // check if slot have a item
                if (slot.gameObject.transform.childCount != 0)
                {
                    var item = slot.GetComponentInChildren<DragableItem>();

                    items.Add(new ItemConfig
                    {
                        itemName = item.itemScriptableObj.itemName,
                        slotIndex = i,
                        quantity = item.count
                    });
                }
            }

            farmConfig.inventory = items;
            farmConfig.gold = InventoryManager.gold;

            // update plants
            var plants = new List<PlantConfig>();
            foreach (var plant in PlantManager.plantList)
            {
                plants.Add(new PlantConfig
                {
                    plantName = plant.Value.plantScriptableObject.itemName,
                    position = plant.Key,
                    dayAge = plant.Value.dayAge,
                    isWatered = plant.Value.isWatered
                });
            }

            farmConfig.plants = plants;

            farmConfig.SaveFarmConfig();
        }


        private void BackToMainMenu()
        {
            SaveGame();

            SceneManager.LoadScene("MainMenu");
        }


        private void AddListeners()
        {
            foreach (var hotkey in _hotkeys)
            {
                // use lambda expression to add listener has arguments
                hotkey.onClick.AddListener(() => OnModifyKeyPress(hotkey.gameObject.name));
            }

            // add listener for language dropdown
            _languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

            // add listener to sliders
            _bgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
            _sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);

            _backBtn.onClick.AddListener(() => gameObject.SetActive(false));

            _saveGameButton.onClick.AddListener(BackToMainMenu);
        }


        private void RemoveAllListeners()
        {
            foreach (var hotkey in _hotkeys)
            {
                hotkey.onClick.RemoveAllListeners();
            }

            // remove all listeners from language dropdown
            _languageDropdown.onValueChanged.RemoveAllListeners();

            // remove all listeners from sliders
            _bgmSlider.onValueChanged.RemoveAllListeners();
            _sfxSlider.onValueChanged.RemoveAllListeners();

            _backBtn.onClick.RemoveAllListeners();
        }


        private void CheckPropertiesValue()
        {
            if (_backBtn == null ||
                _hotkeys == null ||
                _languageDropdown == null ||
                _bgmSlider == null ||
                _sfxSlider == null ||
                _modifyHotKeyUI == null ||
                _saveGameButton == null ||
                _inventoryManager == null)
            {
                Debug.LogError("There is a component was not assigned in " + gameObject.name + ".");
            }
        }
    }
}
