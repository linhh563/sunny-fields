using System;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class GameplayInputManager : MonoBehaviour
    {
        public static GameplayInputManager Instance;

        // private Dictionary<string, KeyCode> _keyBindings = new Dictionary<string, KeyCode>();

        public static int noItemSelected = -1;

        // Events
        public static event Action OnMovingKeyPress;
        public static event Action OnRunningKeyPress;
        public static event Action OnRunningKeyRelease;
        public static event Action OnStrollingKeyPress;
        public static event Action OnNothingPress;

        public static event Action OnItemSelected;

        public static event Action OnFarmingKeyPress;
        public static event Action OnFarmingKeyRelease;

        public static event Action OnInteractKeyPress;

        public static event Action OnBagKeyPress;
        public static event Action OnExitUIKeyPress;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }


        void Update()
        {
            HandleMovementInput();
            HandleSelectItemInput();
            HandleFarmingInput();
            HandleCharacterInteractInput();
            HandleBagInput();
            HandleExitUIInput();
        }


        private void HandleMovementInput()
        {
            if (IsPressMovingButton())
            {
                if (Input.GetKey(GameSetting.Instance.keyBindings["Running"]))
                {
                    OnRunningKeyPress?.Invoke();
                }
                else
                {
                    OnRunningKeyRelease?.Invoke();
                    OnMovingKeyPress?.Invoke();
                }
            }
            else if (Input.GetKey(GameSetting.Instance.keyBindings["Strolling"]))
            {
                OnStrollingKeyPress?.Invoke();
            }
            else
            {
                OnNothingPress?.Invoke();
            }
        }


        private void HandleFarmingInput()
        {
            if (Input.GetKey(GameSetting.Instance.keyBindings["Farming"]))
            {
                OnFarmingKeyPress?.Invoke();
            }

            if (Input.GetKeyUp(GameSetting.Instance.keyBindings["Farming"]))
            {
                OnFarmingKeyRelease?.Invoke();
            }
        }


        private void HandleSelectItemInput()
        {
            var input = Input.inputString;
            if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5" || input == "6" || input == "7")
            {
                OnItemSelected?.Invoke();
            }
        }


        private bool IsPressMovingButton()
        {
            return (Input.GetKey(GameSetting.Instance.keyBindings["MoveUp"])
                || Input.GetKey(GameSetting.Instance.keyBindings["MoveDown"])
                || Input.GetKey(GameSetting.Instance.keyBindings["MoveLeft"])
                || Input.GetKey(GameSetting.Instance.keyBindings["MoveRight"]));
        }


        private void HandleCharacterInteractInput()
        {
            if (Input.GetKeyDown(GameSetting.Instance.keyBindings["Interact"]))
            {
                OnInteractKeyPress?.Invoke();
            }
        }


        private void HandleBagInput()
        {
            if (Input.GetKeyDown(GameSetting.Instance.keyBindings["Bag"]))
            {
                OnBagKeyPress?.Invoke();
            }
        }


        private void HandleExitUIInput()
        {
            if (Input.GetKeyDown(GameSetting.Instance.keyBindings["ExitUI"]))
            {
                OnExitUIKeyPress?.Invoke();
            }
        }


        public CharacterCommand GetCharacterMoveDirection()
        {
            if (Input.GetKey(GameSetting.Instance.keyBindings["MoveUp"]))
                return CharacterCommand.MoveUp;

            if (Input.GetKey(GameSetting.Instance.keyBindings["MoveDown"]))
                return CharacterCommand.MoveDown;

            if (Input.GetKey(GameSetting.Instance.keyBindings["MoveLeft"]))
                return CharacterCommand.MoveLeft;

            if (Input.GetKey(GameSetting.Instance.keyBindings["MoveRight"]))
                return CharacterCommand.MoveRight;

            return CharacterCommand.DoNothing;
        }


        public int GetItemIndex()
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
    }
}
