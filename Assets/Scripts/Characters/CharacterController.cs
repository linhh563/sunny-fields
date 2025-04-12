using UnityEngine;
using Management;

namespace Characters
{    
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(CharacterFarmingController))]
    public class CharacterController : MonoBehaviour
    {
        public static CharacterDirection currentDirection { get; private set; }
        public static Vector3 characterPosition { get; private set; }

        // Controllers
        public CharacterAnimation animController { get; private set; }
        public CharacterInventory inventoryController { get; private set; }
        public CharacterMovement movementController {get; private set;}
        public CharacterFarmingController farmingController { get; private set; }


        private StateManager _stateManager;
        
        private void Awake() {
            _stateManager = GetComponent<StateManager>();

            // Set controllers
            movementController = GetComponent<CharacterMovement>();
            farmingController = GetComponent<CharacterFarmingController>();
            animController = GetComponentInChildren<CharacterAnimation>();
            inventoryController = GetComponentInChildren<CharacterInventory>();

            currentDirection = CharacterDirection.Down;

            _stateManager.ChangeState(new CharacterIdleState(this));
        }

        private void OnDisable() {
            characterPosition = Vector3.zero;
            currentDirection = CharacterDirection.Down;
        }

        private void Update()
        {
            UpdateCurrentDirection();
            UpdateCharacterPosition();
            FarmingHandle();
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
                    transform.localScale = new Vector3(1, 1, 1);
                    currentDirection = CharacterDirection.Right;
                    break;
                case CharacterCommand.MoveLeft:
                    transform.localScale = new Vector3(-1, 1, 1);
                    currentDirection = CharacterDirection.Left;
                    break;
            }
        }

        private void MovingHandle()
        {        
            switch (movementController.movementState)
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

        private void FarmingHandle()
        {
            // TODO: check character's farming state and change character's state
            switch (farmingController.farmingState)
            {
                case CharacterFarmingState.Hoeing:
                    _stateManager.ChangeState(new CharacterHoeingState(this));
                    break;
            }
        }

        private void UpdateCharacterPosition()
        {
            characterPosition = transform.position;
        }
    }    

    public enum CharacterDirection 
    {
        Up,
        Down,
        Left,
        Right
    }
}
