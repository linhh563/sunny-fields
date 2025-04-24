using System;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class GameplayInputManager : MonoBehaviour
    {
        public static GameplayInputManager Instance;

        private Dictionary<string, KeyCode> _keyBindings = new Dictionary<string, KeyCode>();

        public static int noItemSelected = -1;

        // Events
        public static event Action OnMovingButtonPress;
        public static event Action OnRunningButtonPress;
        public static event Action OnRunningButtonRelease;
        public static event Action OnStrollingButtonPress;
        public static event Action OnNothingPress;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            InitializeKeyBindings();
        }

        void Update()
        {
            if (IsPressMovingButton())
            {
                if (Input.GetKey(_keyBindings["Running"]))
                {
                    OnRunningButtonPress?.Invoke();
                }
                else
                {
                    OnRunningButtonRelease?.Invoke();
                    OnMovingButtonPress?.Invoke();
                }
            }
            else if (Input.GetKey(_keyBindings["Strolling"]))
            {
                OnStrollingButtonPress?.Invoke();
            }
            else
            {
                OnNothingPress?.Invoke();
            }
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

        private bool IsPressMovingButton()
        {
            return (Input.GetKey(_keyBindings["MoveUp"])
                || Input.GetKey(_keyBindings["MoveDown"])
                || Input.GetKey(_keyBindings["MoveLeft"])
                || Input.GetKey(_keyBindings["MoveRight"]));
        }

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
            return 0;
        }
    }
}
