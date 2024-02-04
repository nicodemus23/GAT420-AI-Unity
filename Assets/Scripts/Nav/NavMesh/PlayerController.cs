using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    void Update()
    {
        // check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {   // create ray from camera to mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // check if ray hits something
           if (Physics.Raycast(ray, out hit))
            {
                // move agent to hit point
                agent.SetDestination(hit.point);
            }

        }
    }
}
