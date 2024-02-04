using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    float timer = 0;
    public AiDeathState(AiStateAgent agent) : base(agent)
    {
    }
    public override void OnEnter()
    {
        agent.animator?.SetTrigger("Death");
        timer = Time.time + 2; // 2 seconds to wait before destroying the object
    }
    public override void OnUpdate()
    {   // 
       if (Time.time > timer)
        {   // 
            GameObject.Destroy(agent.gameObject);
        }
    }

    public override void OnExit()
    {
     
    }
}
