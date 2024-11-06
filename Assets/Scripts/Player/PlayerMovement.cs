using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float defaultSpeed = 5f;
    private float speed;

    private void Awake() {
        speed = defaultSpeed;
    }

    public void Moving(Vector2 movement)
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
}