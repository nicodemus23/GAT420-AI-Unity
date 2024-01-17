using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// DERIVES FROM AIAgent
public class AIAutonomousAgent : AiAgent
{
    [SerializeField] AiPerception seekPerception = null;
    [SerializeField] AiPerception fleePerception = null;
    [SerializeField] AiPerception flockPerception = null;
    [SerializeField] AiPerception obstaclePerception = null;

    private void Update()
    {

        // seek
        if (seekPerception != null)
        {

            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)

            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }

        //flee
        if (fleePerception != null)
        {

            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)

            {
                // Vector3 vector3 = Flee(gameObjects[0]);
                // movement.ApplyForce(Flee(gameObjects[0]));
                movement.ApplyForce(Flee(gameObjects[0]));

            }
        }

        // flock 
        if (flockPerception != null)
        {
            var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects));
                movement.ApplyForce(Separation(gameObjects, 3));
                movement.ApplyForce(Alignment(gameObjects));
            }
            //foreach (var go in gameObjects)
            //{
            //    Debug.DrawLine(transform.position, go.transform.position, Color.red);
            //}
        }

        if (obstaclePerception != null)
        {
            if (((AiRayCastPerception)obstaclePerception).CheckDirection(Vector3.forward))
            {
                Vector3 open = Vector3.zero;
                if (((AiRayCastPerception)obstaclePerception).GetOpenDirection(ref open)) // reference is passing a pointer to open variable
                {
                    movement.ApplyForce(GetSteeringForce(open) * 5); // apply force to open direction with weight of 5
                }
                //else
                //{
                //    Vector3 avoid = ((AiRayCastPerception)obstaclePerception).ObstacleAvoidance();
                //    movement.ApplyForce(avoid);
                //}
            }
            var gameObjects = obstaclePerception.GetGameObjects();
        }

        // obstacle avoidance
        //if (obstaclePerception != null)
        //{
        //    if (((AiRayCastPerception)obstaclePerception).CheckDirection(Vector3.forward))
        //    {
        //        Vector3 open = Vector3.zero;

        //        // check if there is an open direction
        //        if (((AiRayCastPerception)obstaclePerception).GetOpenDirection(ref open))
        //        {
        //            print("Open");
        //            movement.ApplyForce(GetSteeringForce(open) * 5);
        //        }
        //        //else
        //        //{
        //        //    Vector3 avoid = ((AiRayCastPerception)obstaclePerception).ObstacleAvoidance();
        //        //    movement.ApplyForce(avoid);
        //        //}
        //    }
        //}

        // cancel y movement in acceleration
        Vector3 acceleration = movement.Acceleration;
        acceleration.y = 0;
        movement.Acceleration = acceleration;

        transform.position = Utilities.Wrap(transform.position, new Vector3(-12, -10, -12), new Vector3(12, 10, 12));
    }




    private Vector3 Seek(GameObject target)
    {   // direction vector = target position - current position
        Vector3 direction = target.transform.position - transform.position;
        // returns force we want to apply 
        Vector3 force = GetSteeringForce(direction);

        return force;
    }
    // flee steering behavior = we are the head and the target is the tail
    private Vector3 Flee(GameObject target)
    {   // flipped head and tail from Seek()
        Vector3 direction = transform.position - target.transform.position;
        // returns force we want to apply 
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    // cohesion steering behavior = we are the head and the target is the tail
    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            positions += neighbor.transform.position;
        }

        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Separation(GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        // separation steering behavior = we are the head and the target is the tail
        foreach (var neighbor in neighbors)
        {
            Vector3 direction = (transform.position - neighbor.transform.position);
            if (direction.magnitude < radius)
            {
                separation += direction / direction.sqrMagnitude;
            }
        }

        Vector3 force = GetSteeringForce(separation);
        return force;
    }

    private Vector3 Alignment(GameObject[] neighbors)
    {
        Vector3 velocities = Vector3.zero;
        foreach (var neighbor in neighbors)
        {   // add up all the velocities of the neighbors
            velocities += neighbor.GetComponent<AIAutonomousAgent>().movement.Velocity;
        }

        Vector3 averageVelocity = velocities / neighbors.Length;
        Vector3 force = GetSteeringForce(averageVelocity);

        return force;
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {   // desired velocity = direction * max speed
        Vector3 desired = direction.normalized * movement.maxSpeed;
        // steering = desired - velocity
        Vector3 steering = desired - movement.Velocity;

        Vector3 force = Vector3.ClampMagnitude(steering, movement.maxForce);

        return force;
    }

}

// direction vector = head - tail 
// velocity 
// acceleration
// steer = desired - velocity