using System;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class GameplayInputManager : MonoBehaviour
    {
        private Dictionary<string, KeyCode> _keyBindings = new Dictionary<string, KeyCode>();

        public static event Action OnMovingButtonPress;

        void Awake()
        {
            InitializeKeyBindings();
        }

        void Update()
        {
            if (Input.GetKey(_keyBindings["MoveUp"])
                || Input.GetKey(_keyBindings["MoveDown"])
                || Input.GetKey(_keyBindings["MoveLeft"])
                || Input.GetKey(_keyBindings["MoveRight"]))
            {
                OnMovingButtonPress?.Invoke();
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
    }
}
