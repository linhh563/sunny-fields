using UnityEngine;

namespace Management
{
    public class StateManager : MonoBehaviour
    {
        private IState _currentState;

        private void Update() {
            if (_currentState != null)
            {
                _currentState.Execute();
            }
        }

        public void ChangeState(IState newState)
        {
            if ((_currentState != null) && (_currentState.GetType() == newState.GetType()))
            {
                return;
            }

            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = newState;
            if (_currentState != null)
            {
                _currentState.Enter();
            }
        }
    }
}
