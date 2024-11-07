using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController controller;

    public PlayerIdleState(PlayerController _controller)
    {
        controller = _controller;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        controller.animationManager.PlayIdleAnimation();
    }

    public void Exit()
    {
    }
}

public class PlayerMovingState : IState
{
    private PlayerController controller;

    public PlayerMovingState(PlayerController _controller)
    {
        controller = _controller;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        Vector2 movement;

        if (InputManager.Instance.GetHorizontalMovement() != 0)
        {
            if (InputManager.Instance.GetHorizontalMovement() < 0)
            {
                controller.ChangeHorizontalDirection(-1);
            }
            else
            {
                controller.ChangeHorizontalDirection(1);
            }

            controller.animationManager.PlayHorizontalAnimation();
            movement = new Vector2(InputManager.Instance.GetHorizontalMovement(), 0);
            controller.Moving(movement);
            return;
        }

        if (InputManager.Instance.GetVerticalMovement() < 0)
        {
            controller.animationManager.PlayMovingDownAnimation();
        }
        else if (InputManager.Instance.GetVerticalMovement() > 0)
        {
            controller.animationManager.PlayMovingUpAnimation();
        }
        movement = new Vector2(0, InputManager.Instance.GetVerticalMovement());
        controller.Moving(movement);

    }

    public void Exit()
    {
    }
}

public class PlayerVerticalMovingState : IState
{
    public void Enter()
    {
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}