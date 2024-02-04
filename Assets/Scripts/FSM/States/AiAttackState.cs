using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : AiState
{
    float timer = 0;
    public AiAttackState(AiStateAgent agent) : base(agent)
    {
    }
    public override void OnEnter()
    {
        agent.animator?.SetTrigger("Attack");
        timer = Time.time + 2; 
    }
    public override void OnUpdate()
    {
        if (Time.time > timer)
        {
            agent.stateMachine.SetState(nameof(AiIdleState));
        }

        Debug.Log("attack state updated");
    }

    public override void OnExit()
    {

    }

}
