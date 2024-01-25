using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AiNavNode : MonoBehaviour
{
	[SerializeField] public List<AiNavNode> neighbors = new List<AiNavNode>();

	// properties don't show in the inspector. the following are properties:

	public float Cost { get; set; } = float .MaxValue; // or could use .PositiveInfinity
	public AiNavNode Parent { get; set; } = null;

	// *just need what the cost is and who the parent is

	public AiNavNode GetRandomNeighbor()
	{
		return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<AiNavPath>(out AiNavPath navPath))
		{
			if (navPath.targetNode == this)
			{
				navPath.targetNode = navPath.GetNextAINavNode(navPath.targetNode);
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.TryGetComponent<AiNavPath>(out AiNavPath navPath))
		{
			if (navPath.targetNode == this)
			{
                navPath.targetNode = navPath.GetNextAINavNode(navPath.targetNode);
            }
        }
	}


	#region HELPER_FUNCTIONS

	public static AiNavNode[] GetAiNavNodes()
	{
		return FindObjectsOfType<AiNavNode>();
	}

	public static AiNavNode[] GetAiNavNodesWithTag(string tag)
	{
		var allNodes = GetAiNavNodes();

		// add nodes with tag to nodes
		List<AiNavNode> nodes = new List<AiNavNode>();
		foreach (var node in allNodes)
		{
			if (node.CompareTag(tag))
			{
				nodes.Add(node);
			}
		}

		return nodes.ToArray();
	}

	public static AiNavNode GetRandomAiNavNode()
	{
		var nodes = GetAiNavNodes();
		return (nodes == null) ? null : nodes[Random.Range(0, nodes.Length)];
	}

    public static void ResetNodes()
    {
        var nodes = GetAiNavNodes();
        foreach (var node in nodes)
        {
            node.Parent = null;
            node.Cost = float.MaxValue;
        }
    }

    #endregion
}
