using Management;
using UnityEngine;

namespace Characters
{
    public class CharacterIdleState : IState
    {
        private CharacterController _controller;

        public CharacterIdleState(CharacterController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
        }

        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(CharacterAnimationName.IdleDown);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(CharacterAnimationName.IdleUp);
                    break;
                default:
                    _controller.animController.PlayAnimation(CharacterAnimationName.Idle);
                    break;
            }
        }

        public void Exit()
        {
        }
    }

    public class CharacterMovingState : IState
    {
        CharacterController _controller;

        public CharacterMovingState(CharacterController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            _controller.movementController.ChangeSpeed(CharacterMovement.defaultSpeed);
        }

        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(CharacterAnimationName.MovingDown);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(CharacterAnimationName.MovingUp);
                    break;
                default:
                    _controller.animController.PlayAnimation(CharacterAnimationName.Moving);
                    break;
            }

            _controller.movementController.Moving();
        }

        public void Exit()
        {

        }
    }

    public class CharacterStrollingState : IState
    {
        CharacterController _controller;

        public CharacterStrollingState(CharacterController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            _controller.movementController.ChangeSpeed(CharacterMovement.defaultSpeed * 0.65f);
        }

        public void Execute()
        {
            // update strolling animation
            _controller.movementController.Moving();
        }

        public void Exit()
        {
        }
    }

    public class CharacterRunningState : IState
    {
        CharacterController _controller;

        public CharacterRunningState(CharacterController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            _controller.movementController.ChangeSpeed(CharacterMovement.defaultSpeed * 1.5f);
        }

        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(CharacterAnimationName.RunDown);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(CharacterAnimationName.RunUp);
                    break;
                default:
                    _controller.animController.PlayAnimation(CharacterAnimationName.Run);
                    break;
            }

            _controller.movementController.Moving();
        }

        public void Exit()
        {
        }
    }

    public class CharacterHoeingState : IState
    {
        CharacterController _controller;

        public CharacterHoeingState(CharacterController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            // TODO: update hoeing animation
            _controller.farmingController.HoeGround();
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}