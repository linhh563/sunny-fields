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
            var currentGridPos = _controller.tilemapManager.groundTilemap.WorldToCell(CharacterController.characterWorldPosition);
            var worrldPos = _controller.tilemapManager.groundTilemap.GetCellCenterWorld(currentGridPos);

            _controller.transform.position = worrldPos;
        }


        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(AnimationName.IDLE_DOWN);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(AnimationName.IDLE_UP);
                    break;
                default:
                    _controller.animController.PlayAnimation(AnimationName.IDLE);
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
            _controller.movementController.ChangeSpeed(CharacterDefaultStats.DEFAULT_SPEED);
        }


        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(AnimationName.MOVING_DOWN);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(AnimationName.MOVING_UP);
                    break;
                default:
                    _controller.animController.PlayAnimation(AnimationName.MOVING);
                    break;
            }

            // _controller._movementController.Moving();
            _controller.Moving();
        }


        public void Exit()
        {

        }
    }


    // public class CharacterStrollingState : IState
    // {
    //     CharacterController _controller;

    //     public CharacterStrollingState(CharacterController controller)
    //     {
    //         _controller = controller;
    //     }

    //     public void Enter()
    //     {
    //         _controller.movementController.ChangeSpeed(CharacterDefaultStats.DEFAULT_SPEED * 0.65f);
    //     }

    //     public void Execute()
    //     {
    //         // TODO: update strolling animation
    //         _controller.Moving();
    //     }

    //     public void Exit()
    //     {
    //     }
    // }


    public class CharacterRunningState : IState
    {
        CharacterController _controller;


        public CharacterRunningState(CharacterController controller)
        {
            _controller = controller;
        }


        public void Enter()
        {
            _controller.movementController.ChangeSpeed(CharacterDefaultStats.DEFAULT_SPEED * 1.5f);
        }


        public void Execute()
        {
            switch (CharacterController.currentDirection)
            {
                case CharacterDirection.Down:
                    _controller.animController.PlayAnimation(AnimationName.RUN_DOWN);
                    break;
                case CharacterDirection.Up:
                    _controller.animController.PlayAnimation(AnimationName.RUN_UP);
                    break;
                default:
                    _controller.animController.PlayAnimation(AnimationName.RUN);
                    break;
            }

            _controller.Moving();
        }
        

        public void Exit()
        {
        }
    }
}