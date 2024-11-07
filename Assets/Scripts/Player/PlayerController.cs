using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof (PlayerAnimationManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    public StateManager stateManager {get; private set;}
    public PlayerAnimationManager animationManager {get; private set;}

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
        stateManager = GetComponent<StateManager>();
        animationManager = GetComponent<PlayerAnimationManager>();
    }

    private void Start() {
        stateManager.ChangeState(new PlayerIdleState(this));
    }

    private void Update() {
        if (InputManager.Instance.IsPlayerMovement())
        {
            stateManager.ChangeState(new PlayerMovingState(this));
        }
        else
        {
            stateManager.ChangeState(new PlayerIdleState(this));
        }
    }

    public void Moving(Vector2 _movement)
    {
        movement.Moving(_movement);
    }

    public void ChangeHorizontalDirection(int _direction)
    {
        Vector3 direction = transform.localScale;

        if (_direction == 1)
        {
            direction = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (_direction == -1)
        {
            direction = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        transform.localScale = direction;
    }
}
