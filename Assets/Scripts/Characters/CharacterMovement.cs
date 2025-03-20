using UnityEngine;
using Management;

namespace Characters
{    
    public enum CharacterMovementState
    {
        Idle,
        Moving,
        Running,
        Strolling
    }

    public class CharacterMovement : MonoBehaviour
    {
        public static float defaultSpeed = 5.0f;
        private float _speed;
        private bool _isMoving;
        private bool _isRunning;
        private bool _isStrolling;
        public CharacterMovementState movementState {get; private set;}

        private void Awake() {
            _speed = defaultSpeed;
            _isMoving = false;
            _isRunning = false;
            _isStrolling = false;

            movementState = CharacterMovementState.Idle;
        }

        private void Update() {
            UpdateMovingState();
            UpdateStrollingState();
            UpdateRunningState();
            UpdateMovementState();
        }

        public void ChangeSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }

        public void Moving()
        {
            Vector2 _movementDir = Vector2.zero;

            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Up:
                    _movementDir = Vector2.up;
                    break;
                case CharacterDirection.Down:
                    _movementDir = Vector2.down;
                    break;
                case CharacterDirection.Left:
                    _movementDir = Vector2.left;
                    break;
                case CharacterDirection.Right:
                    _movementDir = Vector2.right;
                    break;
            }

            transform.Translate(_movementDir * _speed * Time.deltaTime);
        }

        private void UpdateMovingState()
        {
            if (InputManager.Instance.GetCharacterMoveDirection() != CharacterCommand.DoNothing)
            {
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
        }

        private void UpdateStrollingState()
        {
            if (InputManager.Instance.IsCharacterStrolling())
            {
                _isStrolling = !_isStrolling;
            }
        }

        private void UpdateRunningState()
        {
            if (InputManager.Instance.IsCharacterRunning())
            {
                _isRunning = true;
            }
            else
            {
                _isRunning = false;
            }
        }

        private void UpdateMovementState()
        {
            // Strolling is a movement mode, character can stroll if it's not moving, so _isStrolling is not included in this condition
            if (!_isMoving && !_isRunning)
            {
                movementState = CharacterMovementState.Idle;
            }
            // Running has the highest priority
            else if (_isRunning)
            {
                movementState = CharacterMovementState.Running;
            }
            // Character just can strolling when it's moving
            else if (_isStrolling && _isMoving)
            {
                movementState = CharacterMovementState.Strolling;
            }
            else if (_isMoving)
            {
                movementState = CharacterMovementState.Moving;
            }
        }
    }
}
