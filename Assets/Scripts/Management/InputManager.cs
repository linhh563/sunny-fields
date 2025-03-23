using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }

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

