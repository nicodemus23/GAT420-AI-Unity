using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class AiNavDijkstra
{
    public static bool Generate(AiNavNode startNode, AiNavNode endNode, List<AiNavNode> path) // return bool if true
    {
        var nodes = new SimplePriorityQueue<AiNavNode>();

        startNode.Cost = 0;             // set startNode cost to 0

        nodes.EnqueueWithoutDuplicates(startNode, startNode.Cost); // add startNode to nodes

        bool found = false;
        while (!found && nodes.Count > 0)
        {
            var node = nodes.Dequeue(); // get node with lowest cost
            
            if (node == endNode)
            {
                found = true;
                break;
            }

            foreach (var neighbor in node.neighbors) // for each neighbor of node
            {
                float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position); // cost = node cost + distance to neighbor
                if (cost < neighbor.Cost)
                {
                    neighbor.Cost = cost; // set neighbor cost to cost
                    neighbor.Parent = node; // set neighbor parent to node
                    nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost);
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
