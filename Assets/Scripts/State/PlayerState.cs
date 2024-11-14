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
        switch (controller.currentDirection)
        {
            case PlayerDirection.DOWN:
                controller.animationManager.PlayIdleDownAnimation();
                break;
            case PlayerDirection.UP:
                controller.animationManager.PlayIdleUpAnimation();
                break;
            default:
                controller.animationManager.PlayIdleAnimation();
                break;
        }
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
                controller.UpdateDirection(PlayerDirection.LEFT);
            }
            else
            {
                controller.UpdateDirection(PlayerDirection.RIGHT);
            }

            controller.animationManager.PlayHorizontalAnimation();
            movement = new Vector2(InputManager.Instance.GetHorizontalMovement(), 0);
            controller.Moving(movement);
            return;
        }

        if (InputManager.Instance.GetVerticalMovement() < 0)
        {
            controller.animationManager.PlayMovingDownAnimation();
            controller.UpdateDirection(PlayerDirection.DOWN);
        }
        else if (InputManager.Instance.GetVerticalMovement() > 0)
        {
            controller.animationManager.PlayMovingUpAnimation();
            controller.UpdateDirection(PlayerDirection.UP);
        }

        movement = new Vector2(0, InputManager.Instance.GetVerticalMovement());
        controller.Moving(movement);
    }

    public void Exit()
    {
    }
}

public class PlayerRunningState : IState
{
    private PlayerController controller;

    public PlayerRunningState(PlayerController _controller)
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
                controller.UpdateDirection(PlayerDirection.LEFT);
            }
            else
            {
                controller.UpdateDirection(PlayerDirection.RIGHT);
            }

            controller.animationManager.PlayRunAnimation();
            movement = new Vector2(InputManager.Instance.GetHorizontalMovement(), 0);
            controller.Running(movement);
            return;
        }

        if (InputManager.Instance.GetVerticalMovement() < 0)
        {
            controller.animationManager.PlayRunDownAnimation();
            controller.UpdateDirection(PlayerDirection.DOWN);
        }
        else if (InputManager.Instance.GetVerticalMovement() > 0)
        {
            controller.animationManager.PlayRunUpAnimation();
            controller.UpdateDirection(PlayerDirection.UP);
        }

        movement = new Vector2(0, InputManager.Instance.GetVerticalMovement());
        controller.Running(movement);
    }

    public void Exit()
    {
    }
}

public class PlayerTillingState : IState
{
    private PlayerController controller;

    public PlayerTillingState(PlayerController _controller)
    {
        controller = _controller;
    }

    public void Enter()
    {
        switch (controller.currentDirection)
        {
            case PlayerDirection.DOWN:
                // play tilling down animation
            case PlayerDirection.UP:
                // play tilling up animation
            default:
                // play tilling animation
                break;
        }
        
        controller.TillingGround();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}