using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrolState : AiState
{
    Vector3 destination;

    public AiPatrolState(AiStateAgent agent) : base(agent)
    {
    }
    public override void OnEnter()
    {   // Get a random node from the AiNavNode class and set it as the destination
        var navNode = AiNavNode.GetRandomAiNavNode();
        destination = navNode.transform.position;
    }
    public override void OnUpdate()
    {   // Move the agent towards the destination ... 
        agent.movement.MoveTowards(destination);
        // If the agent is close to the destination, set the state to AiIdleState
        if (Vector3.Distance(agent.transform.position, destination) < 1)
        {
            agent.stateMachine.SetState(nameof(AiIdleState));
        }
        // If the agent sees an enemy, set the state to AiChaseState
        var enemies = agent.enemyPerception.GetGameObjects();
        if (enemies.Length > 0)
        {
            agent.stateMachine.SetState(nameof(AiChaseState));
        }
    }

    public override void OnExit()
    {

    }

}
