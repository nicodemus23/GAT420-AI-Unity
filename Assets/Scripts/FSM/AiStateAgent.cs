using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAgent : AiAgent
{   // this is the animator component that will be used to control animations
    public Animator animator;
    public float health = 100;
    // this is the perception component that will be used to detect enemies
    public AiPerception enemyPerception;
    // this is the agent that will be controlled by the state machine
    public AiStateMachine stateMachine = new AiStateMachine();

    

    private void Start()
    {
        // add states to  machine here
        stateMachine.AddState(nameof(AiIdleState), new AiIdleState(this));
        stateMachine.AddState(nameof(AiDeathState), new AiDeathState(this));
        stateMachine.AddState(nameof(AiAttackState), new AiAttackState(this));
        stateMachine.AddState(nameof(AiPatrolState), new AiPatrolState(this));
        stateMachine.AddState(nameof(AiChaseState), new AiChaseState(this));
    }

    private void Update()
    {
        
        if (health <= 0)
        {
            stateMachine.SetState(nameof(AiDeathState));
        }
        // update animator speed
        animator?.SetFloat("Speed", movement.Velocity.magnitude);
        // call update on current state of state machine
        stateMachine.Update();
    }

    private void OnGUI()
    {
        // draw label of current state above agent
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        // get point above agent
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        // draw label with current state name
        //GUI.Label(rect, stateMachine.CurrentState.name);
    }
}
