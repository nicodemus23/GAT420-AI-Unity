using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DERIVES FROM AIAgent
public class AIAutonomousAgent : AiAgent
{
    public AiPerception perception = null;

    private void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var go in gameObjects)
        {
            Debug.DrawLine(transform.position, go.transform.position, Color.red);
        }
    }

}
