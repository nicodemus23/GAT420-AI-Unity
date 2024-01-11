using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgentSpawner : MonoBehaviour
{
    [SerializeField] AiAgent[] agents;
    [SerializeField] LayerMask layerMask;

    int index = 0;

    void Update()
    {   // press tab to switch agent to spawn
        if (Input.GetKeyDown(KeyCode.Tab)) index = (++index % agents.Length);
        // click to spawn or old left control and mouse buttons ot spawn multiple
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl)))
        {   
            // get ray from camera position screenposition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // raycast and see if it hits and object with the layer mask
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                // spawn agent at hit point with random rotation
               Instantiate(agents[index], hitInfo.point, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up));
            }
        }
    }
}
