using UnityEngine;
using Management;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(StateManager))]
    public class CharacterController : MonoBehaviour
    {
        public static CharacterDirection currentDirection;
        public CharacterMovement characterMovement {get; private set;}
        private StateManager _stateManager;
        
        private void Awake() {
            characterMovement = GetComponent<CharacterMovement>();
            _stateManager = GetComponent<StateManager>();

            currentDirection = CharacterDirection.Down;

            _stateManager.ChangeState(new CharacterIdleState(this));
        }

        private void Update()
        {
            UpdateCurrentDirection();
            MovingHandle();
        }

        private void UpdateCurrentDirection()
        {
            var _dir = InputManager.Instance.GetCharacterMoveDirection();

            switch (_dir)
            {
                case CharacterCommand.MoveDown:
                    currentDirection = CharacterDirection.Down;
                    break;
                case CharacterCommand.MoveUp:
                    currentDirection = CharacterDirection.Up;
                    break;
                case CharacterCommand.MoveRight:
                    currentDirection = CharacterDirection.Right;
                    break;
                case CharacterCommand.MoveLeft:
                    currentDirection = CharacterDirection.Left;
                    break;
            }
        }

        public void MovingHandle()
        {        
            switch (characterMovement.movementState)
            {
                case CharacterMovementState.Idle:
                    _stateManager.ChangeState(new CharacterIdleState(this));
                    break;
                case CharacterMovementState.Moving:
                    _stateManager.ChangeState(new CharacterMovingState(this));
                    break;
                case CharacterMovementState.Strolling:
                    _stateManager.ChangeState(new CharacterStrollingState(this));
                    break;
                case CharacterMovementState.Running:
                    _stateManager.ChangeState(new CharacterRunningState(this));
                    break;
            }
        }
    }    
}
