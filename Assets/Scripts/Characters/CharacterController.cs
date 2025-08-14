using UnityEngine;
using Management;
using Environment;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(CharacterFarmingController))]
    public class CharacterController : MonoBehaviour
    {
        public static CharacterDirection currentDirection { get; private set; }

        // character position in world position (use to convert to tile map position)
        public static Vector3 characterWorldPosition { get; private set; }

        // CONTROLLERS 
        public CharacterAnimation animController { get; private set; }
        public CharacterInventory inventoryController { get; private set; }
        public CharacterMovement movementController { get; private set; }
        public CharacterFarmingController farmingController { get; private set; }
        public TilemapManager tilemapManager { get; private set; }

        private StateManager _stateManager;


        private void Awake()
        {
            _stateManager = GetComponent<StateManager>();

            // Set controllers
            movementController = GetComponent<CharacterMovement>();
            farmingController = GetComponent<CharacterFarmingController>();
            animController = GetComponentInChildren<CharacterAnimation>();
            inventoryController = GetComponentInChildren<CharacterInventory>();

            // Set default values
            currentDirection = CharacterDirection.Down;
        }


        void Start()
        {
            tilemapManager = FindObjectOfType<TilemapManager>();
            _stateManager.ChangeState(new CharacterIdleState(this));
        }


        private void OnDisable()
        {
            // Reset values
            characterWorldPosition = Vector3.zero;
            currentDirection = CharacterDirection.Down;
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


        private void HandleMovementState()
        {
            switch (movementController.movementState)
            {
                case CharacterMovementState.Idle:
                    _stateManager.ChangeState(new CharacterIdleState(this));
                    break;
                case CharacterMovementState.Moving:
                    _stateManager.ChangeState(new CharacterMovingState(this));
                    break;
                // case CharacterMovementState.Strolling:
                //     _stateManager.ChangeState(new CharacterStrollingState(this));
                //     break;
                case CharacterMovementState.Running:
                    _stateManager.ChangeState(new CharacterRunningState(this));
                    break;
            }
        }


        private void HandleFarmingState()
        {
            switch (farmingController.farmingState)
            {
                case CharacterFarmingState.Hoeing:
                    farmingController.HoeGround();
                    // Play character hoeing animation
                    break;

                case CharacterFarmingState.Planting:
                    farmingController.Planting();
                    // Play character planting animation
                    break;

                case CharacterFarmingState.Watering:
                    farmingController.Watering();
                    // Play character watering animation
                    break;

                case CharacterFarmingState.Harvesting:
                    farmingController.Harvesting();
                    // Play character harvesting animation
                    break;

                default:
                    break;
            }
        }


        private void UpdateCharacterWorldPosition()
        {
            characterWorldPosition = transform.position;
        }


        public void Moving()
        {
            // get the position of the tile in front of the character and convert it to world position
            var positionTowardCharacter = tilemapManager.groundTilemap.GetCellCenterWorld(tilemapManager.GetTileInFrontCharacter());
            var direction = positionTowardCharacter - transform.position;

            // call character move toward method
            movementController.MoveToward(direction.normalized);
        }


        public void Initialize(Vector3 position, CharacterDirection direction)
        {
            characterWorldPosition = position;
            currentDirection = direction;

            transform.position = position;
        }
    }    
}
