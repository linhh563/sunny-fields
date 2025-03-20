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
            _controller.characterMovement.ChangeSpeed(CharacterMovement.defaultSpeed);
        }

        public void Execute()
        {
            // update moving animation
            _controller.characterMovement.Moving();
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
            _controller.characterMovement.ChangeSpeed(CharacterMovement.defaultSpeed * 0.65f);
        }

        public void Execute()
        {
            // update strolling animation
            _controller.characterMovement.Moving();
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
            _controller.characterMovement.ChangeSpeed(CharacterMovement.defaultSpeed * 1.5f);            
        }

        public void Execute()
        {
            // update walking animation
            _controller.characterMovement.Moving();
        }

        public void Exit()
        {
        }
    }
}