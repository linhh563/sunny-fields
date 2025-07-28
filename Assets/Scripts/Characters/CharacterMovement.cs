using UnityEngine;
using Management;

namespace Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        private float _speed;
        private bool _isMoving;
        private bool _isRunning;
        private bool _isStrolling;
        public CharacterMovementState movementState { get; private set; }

        private void Awake()
        {
            _speed = CharacterDefaultStats.DEFAULT_SPEED;
            _isMoving = false;
            _isRunning = false;
            _isStrolling = false;

            movementState = CharacterMovementState.Idle;
        }

        void Start()
        {
            // Subscribe to input events
            GameplayInputManager.OnMovingButtonPress += EnableMovingState;

            GameplayInputManager.OnRunningButtonPress += EnableRunningState;
            GameplayInputManager.OnRunningButtonRelease += DisableRunningState;

            GameplayInputManager.OnStrollingButtonPress += ToggleStrollingState;

            GameplayInputManager.OnNothingPress += DisableMovingState;
        }

        private void Update()
        {
            UpdateMovementState();
        }

        void OnDisable()
        {
            // Unsubscribe from input events
            GameplayInputManager.OnMovingButtonPress -= EnableMovingState;

            GameplayInputManager.OnRunningButtonPress -= EnableRunningState;
            GameplayInputManager.OnRunningButtonRelease -= DisableRunningState;

            GameplayInputManager.OnStrollingButtonPress -= ToggleStrollingState;

            GameplayInputManager.OnNothingPress -= DisableMovingState;
        }

        public void ChangeSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }

        public void MoveToward(Vector3 direction)
        {
            Vector2 movementDir = Vector2.zero;

            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Up:
                    movementDir.y = Mathf.Abs(direction.y);
                    break;
                case CharacterDirection.Down:
                    movementDir.y = -Mathf.Abs(direction.y);
                    break;
                case CharacterDirection.Left:
                    movementDir.x = -Mathf.Abs(direction.x);
                    break;
                case CharacterDirection.Right:
                    movementDir.x = Mathf.Abs(direction.x);
                    break;
            }

            transform.Translate(movementDir * _speed * Time.deltaTime);
        }

        private void EnableMovingState()
        {
            _isMoving = true;
        }

        private void DisableMovingState()
        {
            _isMoving = false;
        }

        private void EnableRunningState()
        {
            _isRunning = true;
        }

        private void DisableRunningState()
        {
            _isRunning = false;
        }

        private void ToggleStrollingState()
        {
            _isStrolling = !_isStrolling;
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
