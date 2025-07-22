using UnityEngine;
using Management;
using System;
using Environment;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(CharacterFarmingController))]
    public class CharacterController : MonoBehaviour
    {
        public static CharacterDirection _currentDirection { get; private set; }

        // character position in world position (use to convert to tile map position)
        public static Vector3 CharacterWorldPosition { get; private set; }

        // CONTROLLERS 
        public CharacterAnimation _animController { get; private set; }
        public CharacterInventory _inventoryController { get; private set; }
        public CharacterMovement _movementController { get; private set; }
        public CharacterFarmingController _farmingController { get; private set; }
        public TilemapManager _tilemapManager { get; private set; }

        private StateManager _stateManager;

        private void Awake()
        {
            _stateManager = GetComponent<StateManager>();

            // Set controllers
            _movementController = GetComponent<CharacterMovement>();
            _farmingController = GetComponent<CharacterFarmingController>();
            _animController = GetComponentInChildren<CharacterAnimation>();
            _inventoryController = GetComponentInChildren<CharacterInventory>();

            // Set default values
            _currentDirection = CharacterDirection.Down;
        }

        void Start()
        {
            _tilemapManager = FindObjectOfType<TilemapManager>();
            _stateManager.ChangeState(new CharacterIdleState(this));
        }

        private void OnDisable()
        {
            // Reset values
            CharacterWorldPosition = Vector3.zero;
            _currentDirection = CharacterDirection.Down;
        }

        private void Update()
        {
            UpdateCurrentDirection();
            UpdateCharacterWorldPosition();
            HandleFarmingState();
            HandleMovementState();
        }

        private void UpdateCurrentDirection()
        {
            var _dir = GameplayInputManager.Instance.GetCharacterMoveDirection();

            switch (_dir)
            {
                case CharacterCommand.MoveDown:
                    _currentDirection = CharacterDirection.Down;
                    break;
                case CharacterCommand.MoveUp:
                    _currentDirection = CharacterDirection.Up;
                    break;
                case CharacterCommand.MoveRight:
                    transform.localScale = new Vector3(1, 1, 1);
                    _currentDirection = CharacterDirection.Right;
                    break;
                case CharacterCommand.MoveLeft:
                    transform.localScale = new Vector3(-1, 1, 1);
                    _currentDirection = CharacterDirection.Left;
                    break;
            }
        }

        private void HandleMovementState()
        {
            switch (_movementController.movementState)
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

        private void HandleFarmingState()
        {
            switch (_farmingController.farmingState)
            {
                case CharacterFarmingState.Hoeing:
                    _farmingController.HoeGround();
                    // Play character hoeing animation
                    break;

                case CharacterFarmingState.Planting:
                    _farmingController.Planting();
                    // Play character planting animation
                    break;

                case CharacterFarmingState.Watering:
                    _farmingController.Watering();
                    // Play character watering animation
                    break;

                case CharacterFarmingState.Harvesting:
                    _farmingController.Harvesting();
                    // Play character harvesting animation
                    break;

                default:
                    break;
            }
        }

        private void UpdateCharacterWorldPosition()
        {
            CharacterWorldPosition = transform.position;
        }

        public void Moving()
        {
            // get the position of the tile in front of the character and convert it to world position
            var positionTowardCharacter = _tilemapManager.groundTilemap.GetCellCenterWorld(_tilemapManager.GetTileInFrontCharacter());
            var direction = positionTowardCharacter - transform.position;

            // call character move toward method
            _movementController.MoveToward(direction.normalized);
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
