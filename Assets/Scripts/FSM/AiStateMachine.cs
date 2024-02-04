using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AiStateMachine
{
    private Dictionary<string, AiState> states = new Dictionary<string, AiState>();

    // property
    public AiState CurrentState { get; private set; } = null;

    public void Update()
    {
        CurrentState?.OnUpdate(); // null check before calling OnUpdate()
    }

    public void AddState(string name, AiState state)
    {   // make sure state machine doesn't already contain state
        Debug.Assert(!states.ContainsKey(name), "State machine already contains state " + name);
        // add state to dictionary
        states[name] = state;
    }

    public void SetState(string name)
    {   // make sure state machine contains state
        Debug.Assert(states.ContainsKey(name), "State machine does not contain state " + name);

        //AiState state = states[name];
        var newState = states[name];

        // if current state is the same as new state, return (don't re-enter same state)
        if (CurrentState == newState)
        {
            return;
        }
        // exit current state and enter next state ...
        CurrentState?.OnExit(); // null check before calling OnExit()
        // set current state to new state
        CurrentState = newState;
        // enter new state
        CurrentState?.OnEnter(); // null check before calling OnEnter()

    }
}
