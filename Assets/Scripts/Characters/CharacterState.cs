using Management;

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
                    _controller.animController.PlayAnimation(AnimationName.IdleDown);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(AnimationName.IdleUp);
                    break;
                default:
                    _controller.animController.PlayAnimation(AnimationName.Idle);
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
            _controller.movementController.ChangeSpeed(CharacterDefaultStats.DefaultSpeed);
        }

        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(AnimationName.MovingDown);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(AnimationName.MovingUp);
                    break;
                default:
                    _controller.animController.PlayAnimation(AnimationName.Moving);
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
            _controller.movementController.ChangeSpeed(CharacterDefaultStats.DefaultSpeed * 0.65f);
        }

        public void Execute()
        {
            // TODO: update strolling animation
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
            _controller.movementController.ChangeSpeed(CharacterDefaultStats.DefaultSpeed * 1.5f);
        }

        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(AnimationName.RunDown);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(AnimationName.RunUp);
                    break;
                default:
                    _controller.animController.PlayAnimation(AnimationName.Run);
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