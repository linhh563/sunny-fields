using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        public static int noItemSelected = -1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }

        // TODO: Delete CharacterCommand enum and modify this method
        public CharacterCommand GetCharacterMoveDirection()
        {
            if (Input.GetKey(KeyCode.W))
                return CharacterCommand.MoveUp;

            if (Input.GetKey(KeyCode.S))
                return CharacterCommand.MoveDown;

            if (Input.GetKey(KeyCode.A))
                return CharacterCommand.MoveLeft;

            if (Input.GetKey(KeyCode.D))
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
            return Input.GetKeyUp(KeyCode.LeftControl);
        }

        public bool IsCharacterRunning()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        public bool IsCharacterFarming()
        {
            return Input.GetKeyUp(KeyCode.F);
        }
    }
}

