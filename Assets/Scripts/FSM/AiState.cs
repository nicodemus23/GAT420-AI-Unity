using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiState
{
    protected AiStateAgent agent;
    public AiState(AiStateAgent agent)
    {
        this.agent = agent;
    }

    public string name { get { return GetType().Name; } }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
