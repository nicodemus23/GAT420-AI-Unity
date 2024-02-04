using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AiIdleState : AiState
{
    float timer;
    public AiIdleState(AiStateAgent agent) : base(agent)
    {
    }
    public override void OnEnter()
    {
        timer = Time.time + Random.Range(1, 2);
        Debug.Log("idle state entered");
    }
    public override void OnUpdate()
    {
        if (timer > timer)
        {
            agent.stateMachine.SetState(nameof(AiPatrolState));

        }

        var enemies = agent.enemyPerception.GetGameObjects();
        if (enemies.Length > 0)
        {
            agent.stateMachine.SetState(nameof(AiAttackState));
        }
        else
        {
            agent.stateMachine.SetState(nameof(AiIdleState));
        }
       
        Debug.Log("idle state updated");
    }

    public override void OnExit()
    {
        Debug.Log("idle state exited");
    }

}
