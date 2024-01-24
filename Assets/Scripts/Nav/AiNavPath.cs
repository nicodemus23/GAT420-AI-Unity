using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiNavPath : MonoBehaviour
{
	[SerializeField] private AiNavNode startNode;

	public AiNavNode targetNode { get; set; } = null;
	public Vector3 destination 
	{ 
		get 
		{ 
			return (targetNode != null) ? targetNode.transform.position : Vector3.zero; 
		} 
	}

	private void Start()
	{
		targetNode = (startNode != null) ? startNode : AiNavNode.GetRandomAiNavNode(); 
		
	}

	public bool HasPath()
	{
		return targetNode != null;
	}

	public AiNavNode GetNextAINavNode(AiNavNode node)
	{
		return node.GetRandomNeighbor();
	}

	/*
	public AINavNode GetNearestAINavNode()
	{
		var nodes = AINavNode.GetAINavNodes().ToList();
		SortAINavNodesByDistance(nodes);

		return (nodes.Count == 0) ? null : nodes[0];
	}

	public void SortAINavNodesByDistance(List<AINavNode> nodes)
	{
		nodes.Sort(CompareDistance);
	}

	public int CompareDistance(AINavNode a, AINavNode b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
	*/
}
