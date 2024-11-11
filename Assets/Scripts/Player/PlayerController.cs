using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof (PlayerAnimationManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    public StateManager stateManager {get; private set;}
    public PlayerAnimationManager animationManager {get; private set;}
    public PlayerDirection currentDirection {get; private set;}

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
        stateManager = GetComponent<StateManager>();
        animationManager = GetComponent<PlayerAnimationManager>();

        currentDirection = PlayerDirection.DOWN;
    }

    private void Start() {
        stateManager.ChangeState(new PlayerIdleState(this));
    }

    private void Update() {
        if (InputManager.Instance.IsPlayerMovement())
        {
            if (InputManager.Instance.IsPlayerRun())
            {
                stateManager.ChangeState(new PlayerRunningState(this));
                return;
            }
            stateManager.ChangeState(new PlayerMovingState(this));
        }
        else
        {
            stateManager.ChangeState(new PlayerIdleState(this));
        }
    }

    public void Moving(Vector2 _movement)
    {
        movement.ChangeSpeed(movement.defaultSpeed);
        movement.Moving(_movement);
    }

    public void Running(Vector2 _movement)
    {
        movement.ChangeSpeed(movement.defaultSpeed * 1.3f);
        movement.Moving(_movement);
    }

    public void UpdateDirection(PlayerDirection _direction)
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
}
