using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float defaultSpeed  {get; private set;}
    private float speed;

    private void Awake() {
        defaultSpeed = 5f;
        speed = defaultSpeed;
    }

    public void ChangeSpeed(float _speed)
    {
        speed = _speed;
    }

    public void Moving(Vector2 movement)
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
}