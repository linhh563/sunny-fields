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
    private PlayerController _controller;
    private float _speed;

    public PlayerMovingState(PlayerController controller, float speed)
    {
        _controller = controller;
        _speed = speed;
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
                _controller.UpdatePlayerCurrentDirection(PlayerDirection.LEFT);
            else
                _controller.UpdatePlayerCurrentDirection(PlayerDirection.RIGHT);

            _controller.animationManager.PlayHorizontalAnimation();
            movement = new Vector2(InputManager.Instance.GetHorizontalMovement(), 0);
            _controller.Moving(movement, _speed);
            return;
        }

        if (InputManager.Instance.GetVerticalMovement() < 0)
        {
            _controller.animationManager.PlayMovingDownAnimation();
            _controller.UpdatePlayerCurrentDirection(PlayerDirection.DOWN);
        }
        else if (InputManager.Instance.GetVerticalMovement() > 0)
        {
            _controller.animationManager.PlayMovingUpAnimation();
            _controller.UpdatePlayerCurrentDirection(PlayerDirection.UP);
        }

        movement = new Vector2(0, InputManager.Instance.GetVerticalMovement());
        _controller.Moving(movement, _speed);
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

    public void Exit()
    {
    }
}

// public class PlayerRunningState : IState
// {
//     private PlayerController _controller;

//     public PlayerRunningState(PlayerController controller)
//     {
//         _controller = controller;
//     }

//     public void Enter()
//     {
//     }

//     public void Execute()
//     {
//         Vector2 movement;

//         if (InputManager.Instance.GetHorizontalMovement() != 0)
//         {
//             if (InputManager.Instance.GetHorizontalMovement() < 0)
//                 _controller.UpdateDirection(PlayerDirection.LEFT);
//             else
//                 _controller.UpdateDirection(PlayerDirection.RIGHT);

//             _controller.animationManager.PlayRunAnimation();
//             movement = new Vector2(InputManager.Instance.GetHorizontalMovement(), 0);
//             _controller.Running(movement);
//             return;
//         }

//         if (InputManager.Instance.GetVerticalMovement() < 0)
//         {
//             _controller.animationManager.PlayRunDownAnimation();
//             _controller.UpdateDirection(PlayerDirection.DOWN);
//         }
//         else if (InputManager.Instance.GetVerticalMovement() > 0)
//         {
//             _controller.animationManager.PlayRunUpAnimation();
//             _controller.UpdateDirection(PlayerDirection.UP);
//         }

//         movement = new Vector2(0, InputManager.Instance.GetVerticalMovement());
//         _controller.Running(movement);
//     }

//     public void Exit()
//     {
//     }
// }

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
        
        controller.movement.ChangeSpeed(PlayerMovement.defaultSpeed * 0.7f);
        controller.TillingGround();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        controller.movement.ChangeSpeed(PlayerMovement.defaultSpeed);
    }
}