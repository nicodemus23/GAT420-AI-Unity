using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Apple;

[RequireComponent(typeof(AiNavAgent))]
public class AiNavPath : MonoBehaviour
{
	public enum ePathType
	{
		Waypoint,
		Dijkstra,
		AStar
	}

	[SerializeField] private ePathType pathType; 
	[SerializeField] private AiNavNode startNode;
	[SerializeField] private AiNavNode endNode;

	AiNavAgent agent;
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
			if (pathType == ePathType.Waypoint) { targetNode = agent.GetNearestAiNavNode(); }
            else if(pathType == ePathType.Dijkstra || pathType == ePathType.AStar)
			{
				GeneratePath(startNode, endNode);
			}
          
        }
	}

	private void Start()
	{
		agent = GetComponent<AiNavAgent>();
		targetNode = (startNode != null) ? startNode : AiNavNode.GetRandomAiNavNode(); 
		
	}

	public bool HasTarget()
	{
		return targetNode != null;
	}

	public AiNavNode GetNextAINavNode(AiNavNode node)
	{
		if (pathType == ePathType.Waypoint) return node.GetRandomNeighbor();
		if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar) return GetNextPathAiNavNode(node);
		return null;
				
	}

	private void GeneratePath(AiNavNode startNode, AiNavNode endNode)
	{
		AiNavNode.ResetNodes();
		AiNavDijkstra.Generate(startNode, endNode, ref path);
	}

	private AiNavNode GetNextPathAiNavNode(AiNavNode node)
	{
		if (path.Count == 0) return null;

		int index = path.FindIndex(pathNode => pathNode == node);
		if (index == -1) return null;
		if (index +1 == path.Count) return null;
		AiNavNode nextNode = path[index + 1];

		return null;
	}
}
