using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public IState currentState {get; private set;}

    private void Update() {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public void ChangeState(IState _newState)
    {
        if (currentState != null && _newState.GetType() == currentState.GetType())
        {
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = _newState;

        if (currentState != null)
        {
            currentState.Enter();
        }
    }
}
