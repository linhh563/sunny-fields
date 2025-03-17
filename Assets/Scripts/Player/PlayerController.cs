using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerController : MonoBehaviour
{
    public PlayerMovement movement {get; private set;}
    public StateManager stateManager { get; private set; }
    public PlayerAnimationManager animationManager { get; private set; }
    public PlayerDirection currentDirection { get; private set; }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        stateManager = GetComponent<StateManager>();
        animationManager = GetComponent<PlayerAnimationManager>();

        currentDirection = PlayerDirection.DOWN;
    }

    private void Start()
    {
        stateManager.ChangeState(new PlayerIdleState(this));
    }

    private void Update()
    {
        PlayerStatesHandle();
    }

    private void PlayerStatesHandle()
    {
        if (InputManager.Instance.IsPlayerTilling())
        {
            stateManager.ChangeState(new PlayerTillingState(this));
            return;
        }

        if (InputManager.Instance.IsPlayerMovement())
        {
            if (InputManager.Instance.IsPlayerRun())
            {
                stateManager.ChangeState(new PlayerMovingState(this, PlayerMovement.defaultSpeed * 1.3f));
                return;
            }
            stateManager.ChangeState(new PlayerMovingState(this, PlayerMovement.defaultSpeed));
            return;
        }

        stateManager.ChangeState(new PlayerIdleState(this));
    }

    public void Moving(Vector2 movement, float speed)
    {
        this.movement.ChangeSpeed(speed);
        this.movement.Moving(movement);
    }

    public void Running(Vector2 movement)
    {
        this.movement.ChangeSpeed(PlayerMovement.defaultSpeed * 1.3f);
        this.movement.Moving(movement);
    }

    public void UpdatePlayerCurrentDirection(PlayerDirection _direction)
    {
        if (currentDirection == _direction)
        {
            return;
        }

        currentDirection = _direction;
        Vector3 direction = transform.localScale;

        if (_direction == PlayerDirection.RIGHT)
        {
            direction = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (_direction == PlayerDirection.LEFT)
        {
            direction = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        transform.localScale = direction;
    }

    public void TillingGround()
    {
        TilesManager.Instance.TillingGroundNextToPlayer(transform.position, currentDirection);
    }

    public void Cultivating()
    {
        var groundState = TilesManager.Instance.GetGroundStateNextToPlayer(transform.position, currentDirection);
        var nextPosition = TilesManager.Instance.GetPositionNextToPlayer(transform.position, currentDirection);

        switch (groundState)
        {
            case GroundState.TILLABLE:
                // TODO: 
                break;
            case GroundState.TILLED:
                // TODO:
                break; 
            case GroundState.WATERED:
                // TODO:
                break; 
            default:
                break;
        }
    }
}
