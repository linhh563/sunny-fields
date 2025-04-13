using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        public static int noItemSelected = -1;

        private Dictionary<string, KeyCode> _keyBindings = new Dictionary<string, KeyCode>();

        private void Awake()
        {
            InitializeKeyBindings();

            if (Instance == null)
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }

        private void InitializeKeyBindings()
        {
            // Initialize key bindings for character movement
            _keyBindings.Add("MoveUp", KeyCode.UpArrow);
            _keyBindings.Add("MoveDown", KeyCode.DownArrow);
            _keyBindings.Add("MoveLeft", KeyCode.LeftArrow);
            _keyBindings.Add("MoveRight", KeyCode.RightArrow);

            _keyBindings.Add("Strolling", KeyCode.LeftControl);
            _keyBindings.Add("Running", KeyCode.LeftShift);

            // Initialize key bindings for character farming
            _keyBindings.Add("Farming", KeyCode.F);

            _keyBindings.Add("Interact", KeyCode.E);
            _keyBindings.Add("Inventory", KeyCode.I);
            _keyBindings.Add("Pause", KeyCode.Escape);
        }

        // TODO: Delete CharacterCommand enum and modify this method
        public CharacterCommand GetCharacterMoveDirection()
        {
            if (Input.GetKey(_keyBindings["MoveUp"]))
                return CharacterCommand.MoveUp;

            if (Input.GetKey(_keyBindings["MoveDown"]))
                return CharacterCommand.MoveDown;

            if (Input.GetKey(_keyBindings["MoveLeft"]))
                return CharacterCommand.MoveLeft;

            if (Input.GetKey(_keyBindings["MoveRight"]))
                return CharacterCommand.MoveRight;

            return CharacterCommand.DoNothing;
        }

        public int GetItemSelection()
        {
            switch (Input.inputString)
            {
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "5":
                    return 5;
                case "6":
                    return 6;
                case "7":
                    return 7;
                default:
                    return noItemSelected;
            }
        }

        public bool IsCharacterStrolling()
        {
            return Input.GetKeyUp(_keyBindings["Strolling"]);
        }

        public bool IsCharacterRunning()
        {
            return Input.GetKey(_keyBindings["Running"]);
        }

        public bool IsCharacterFarming()
        {
            return Input.GetKeyUp(_keyBindings["Farming"]);
        }
    }
}

