using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public bool IsPlayerMovement()
    {
        return GetHorizontalMovement() != 0 || GetVerticalMovement() != 0;
    }

    public int GetHorizontalMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return 1;
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return -1;
        }

        return 0;
    }

    public int GetVerticalMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return 1;
        } 
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return -1;
        }

        return 0;
    }

    public bool IsPlayerRun()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}
