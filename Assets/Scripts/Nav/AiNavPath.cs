using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AiNavAgent))]
public class AiNavPath : MonoBehaviour
{
	public enum ePathType
	{
		Waypoint,
		Dijkstra,
		AStar
	}

	[SerializeField] AiNavAgent agent;
	[SerializeField] private ePathType pathType;

	List<AiNavNode> path = new List<AiNavNode>();

	public AiNavNode targetNode { get; set; } = null;

    public Vector3 destination 
	{ 
		get 
		{ 
			return (targetNode != null) ? targetNode.transform.position : Vector3.zero; 
		} 
		set
		{
			if (pathType == ePathType.Waypoint) 
			{ 
				targetNode = agent.GetNearestAiNavNode(value); 
			}

            else if(pathType == ePathType.Dijkstra || pathType == ePathType.AStar)
			{
				AiNavNode startNode = agent.GetNearestAiNavNode();
				AiNavNode endNode = agent.GetNearestAiNavNode(value);
				GeneratePath(startNode, endNode);
				targetNode = startNode;
			}
          
        }
	}

	private void Start()
	{
		//agent = GetComponent<AiNavAgent>(); // should this be commented out?

		//targetNode = (startNode != null) ? startNode : AiNavNode.GetRandomAiNavNode(); 
		
	}

	public bool HasTarget()
	{
		return targetNode != null;
	}

	public AiNavNode GetNextAINavNode(AiNavNode node)
	{
		if (pathType == ePathType.Waypoint) return node.GetRandomNeighbor();
		if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar)
		{ 
			return GetNextPathAiNavNode(node); 
		}

		return null;
				
	}

	private void GeneratePath(AiNavNode startNode, AiNavNode endNode)
	{
		AiNavNode.ResetNodes();


		// this or a switch?
       // if (pathType == ePathType.Dijkstra) AiNavDijkstra.Generate(startNode, endNode, ref path);
       // if (pathType == ePathType.AStar) AiNavAStar.Generate(startNode, endNode, ref path);

		switch(pathType)
		{
			case ePathType.Dijkstra:
                AiNavDijkstra.Generate(startNode, endNode, ref path);
                break;
			case ePathType.AStar:
				AiNavAStar.Generate(startNode, endNode, ref path);
                break;
		}

        //AiNavDijkstra.Generate(startNode, endNode, ref path);
	}

	private AiNavNode GetNextPathAiNavNode(AiNavNode node)
	{
		if (path.Count == 0) return null;

		int index = path.FindIndex(pathNode => pathNode == node);
		if (index == -1) return null;
		if (index +1 >= path.Count) return null; // was ==
		AiNavNode nextNode = path[index + 1];

		return nextNode;
	}

	// draws path interactively?
    private void OnDrawGizmosSelected()
    {
        if (path.Count == 0) return;

        var pathArray = path.ToArray();

        for (int i = 1; i < path.Count - 1; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(pathArray[i].transform.position + Vector3.up, 1);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pathArray[0].transform.position + Vector3.up, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pathArray[pathArray.Length - 1].transform.position + Vector3.up, 1);
    }
}
