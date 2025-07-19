using Management;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            var currentGridPos = _controller._tilemapManager.groundTilemap.WorldToCell(CharacterController.CharacterWorldPosition);
            var worrldPos = _controller._tilemapManager.groundTilemap.GetCellCenterWorld(currentGridPos);

            _controller.transform.position = worrldPos;
        }

        public void Execute()
        {
            switch (CharacterController._currentDirection)
            {
                case CharacterDirection.Down:
                    _controller._animController.PlayAnimation(AnimationName.IDLE_DOWN);
                    break;
                case CharacterDirection.Up:
                    _controller._animController.PlayAnimation(AnimationName.IDLE_UP);
                    break;
                default:
                    _controller._animController.PlayAnimation(AnimationName.IDLE);
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
            _controller._movementController.ChangeSpeed(CharacterDefaultStats.DEFAULT_SPEED);
        }

        public void Execute()
        {
            switch (CharacterController._currentDirection)
            {
                case CharacterDirection.Down:
                    _controller._animController.PlayAnimation(AnimationName.MOVING_DOWN);
                    break;
                case CharacterDirection.Up:
                    _controller._animController.PlayAnimation(AnimationName.MOVING_UP);
                    break;
                default:
                    _controller._animController.PlayAnimation(AnimationName.MOVING);
                    break;
            }

            _controller._movementController.Moving();
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
            _controller._movementController.ChangeSpeed(CharacterDefaultStats.DEFAULT_SPEED * 0.65f);
        }

        public void Execute()
        {
            // TODO: update strolling animation
            _controller._movementController.Moving();
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
            _controller._movementController.ChangeSpeed(CharacterDefaultStats.DEFAULT_SPEED * 1.5f);
        }

        public void Execute()
        {
            switch (CharacterController._currentDirection)
            {
                case CharacterDirection.Down:
                    _controller._animController.PlayAnimation(AnimationName.RUN_DOWN);
                    break;
                case CharacterDirection.Up:
                    _controller._animController.PlayAnimation(AnimationName.RUN_UP);
                    break;
                default:
                    _controller._animController.PlayAnimation(AnimationName.RUN);
                    break;
            }

            _controller._movementController.Moving();
        }

        public void Exit()
        {
        }
    }
}