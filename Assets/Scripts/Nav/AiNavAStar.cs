using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue; // .dll added to project in Assets

public class AiNavAStar // no monobehavior = no update
{                                           // static = no instance 
    public static bool Generate(AiNavNode startNode, AiNavNode endNode, ref List<AiNavNode> path) // return bool if found the endNode // returns a list of nodes
    {   // PriorityQueue of simple nav nodes
        var nodes = new SimplePriorityQueue<AiNavNode>();

        
        startNode.Cost = 0; // set startNode cost to 0
        float heuristic = Vector3.Distance(startNode.transform.position, endNode.transform.position);

        // enqueue source node with the source cost as the priority
        nodes.EnqueueWithoutDuplicates(startNode, startNode.Cost + heuristic); // add startNode to nodes

        bool found = false; 
        while (!found && nodes.Count > 0) // while not found and nodes not empty
        {
            var node = nodes.Dequeue(); // get node with lowest cost // dequeue first node in nodes // nodes is a priority queue
            
            if (node == endNode) 
            {
                found = true;
                break; 
            }

            foreach (var neighbor in node.neighbors) // for each neighbor of node // neighbors is a list of nodes // node is a simple nav node // neighbor is a simple nav node // node.neighbors is a list of simple nav nodes
            {
                float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position); // cost = node cost + distance to neighbor
                if (cost < neighbor.Cost) 
                {
                    neighbor.Cost = cost; // set neighbor cost to cost
                    neighbor.Parent = node; // set neighbor parent to node

                    heuristic = Vector3.Distance(neighbor.transform.position, endNode.transform.position);
                    nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost + heuristic); // add neighbor to nodes // nodes is a priority queue // neighbor is a simple nav node // neighbor.cost is a float
                }
            }
        }

        path.Clear();
        if (found)
        {
            CreatePathFromParents(endNode, ref path); // create path from parents // ref path is a reference to the path list
        }

        return found;
    }

    public static void CreatePathFromParents(AiNavNode node, ref List<AiNavNode> path)
    {
        // while node not null
        while (node != null)
        {
            // add node to list path
            path.Add(node);
            // set node to node parent
            node = node.Parent;
        }

        // reverse path
        path.Reverse();
    }


}
