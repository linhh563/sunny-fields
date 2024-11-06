using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (InputManager.Instance.IsGetHorizontalMovement() != 0)
        {
            movement.Moving(new Vector2(InputManager.Instance.IsGetHorizontalMovement(), 0));
            return;
        }
        if (InputManager.Instance.IsGetVerticalMovement() != 0)
        {
            movement.Moving(new Vector2(0, InputManager.Instance.IsGetVerticalMovement()));
        }
    }
}
