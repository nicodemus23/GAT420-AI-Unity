using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class AiSphereCastPerception : AiPerception
{
    [SerializeField][Range(2, 50)] int numRaycast = 2;
    [SerializeField][Range(1, 50)] float Radius = 1;


    public void OnDrawGizmos()
    {
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);

        foreach (Vector3 direction in directions)
        {
            // check if collision is self, skip if so
            // without transform.rotation (quaternion) it's just in world space. Direction alone is just local space
            Ray ray = new Ray(transform.position, transform.rotation * direction);

            Gizmos.color = Color.red;           // ray.direction is a unit vector on its own
            Gizmos.DrawWireSphere(ray.origin + ray.direction * distance, Radius); // draw sphere at end of ray
        }
    }
    public override GameObject[] GetGameObjects()
    {

        List<GameObject> result = new List<GameObject>();

        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);

        foreach (Vector3 direction in directions)
        {   
            // check if collision is self, skip if so
            // without transform.rotation (quaternion) it's just in world space. Direction alone is just local space
            Ray ray = new Ray(transform.position, transform.rotation * direction);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            if (Physics.SphereCast(ray, Radius, out RaycastHit raycastHit, distance))
            {   // when it hit something in the world, stop drawing the ray (return true)
                Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);
                if (raycastHit.collider.gameObject == gameObject) continue;
                // check if tag name is empty or if tag name matches
                if (tagName == "" || raycastHit.collider.CompareTag(tagName))
                {
                    result.Add(raycastHit.collider.gameObject);

                }
            }
            else
            {   // draws green when it doesn't hit anything
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
            }
        }

        // remove duplicates // returns new list so we need to reassign to "ourselves"
        result = result.Distinct().ToList();

        return result.ToArray();

    }

    // get open direction (no raycast hit)
    public bool GetOpenDirection(ref Vector3 openDirection)
    {
        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
        foreach (var direction in directions)
        {
            // cast ray from transform position towards direction (use game object orientation)
            Ray ray = new Ray(transform.position, transform.rotation * direction);
            // if there is NO raycast hit then that is an open direction
            if (!Physics.SphereCast(ray, Radius, out RaycastHit raycastHit, distance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
                // set open direction
                openDirection = ray.direction;
                return true;
            }
        }

        // no open direction
        return false;
    }

    // check direction (raycast hit) // returns true if raycast hit // is there something in the way?
    public bool CheckDirection(Vector3 direction)
    {
        // create ray in direction (use game object orientation)
        Ray ray = new Ray(transform.position, transform.rotation * direction);
        // check ray cast
        return Physics.SphereCast(ray, Radius, distance, layerMask);

    }

    public Vector3 ObstacleAvoidance()
    {
        Vector3 avoidanceForce = Vector3.zero;

        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
        foreach (var direction in directions)
        {
            Ray ray = new Ray(transform.position, transform.rotation * direction);
            if (!Physics.SphereCast(ray, Radius, out RaycastHit raycastHit, distance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.cyan);

            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.magenta);

                Vector3 openDirection = Vector3.zero;
                if (GetOpenDirection(ref openDirection))
                {
                    avoidanceForce += openDirection.normalized; // * avoidanceStrength;
                }
            }
        }
        return avoidanceForce;
    }
}



