using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AiNavPath))]
public class AiNavAgent : AiAgent
{
	[SerializeField] AiNavPath path;
    [SerializeField] AiNavNode startNode;

    private void Start()
    {
        startNode ??= GetNearestAiNavNode();
        //path.destination = startNode.transform.position; // do I need this???

    }

    void Update()
	{
		if (path.HasTarget())
		{
			Debug.DrawLine(transform.position, path.destination, Color.yellow);
			movement.MoveTowards(path.destination);
		}
        else
        {
            AiNavNode destinationNode  = AiNavNode.GetRandomAiNavNode();
           path.destination = startNode.transform.position;
        }

       
	}

    #region AI_NAVNODE

    public AiNavNode GetNearestAiNavNode()
    {
        var nodes = AiNavNode.GetAiNavNodes().ToList(); 
        SortByDistance(nodes);

        return (nodes.Count == 0) ? null : nodes[0];
    }

    public AiNavNode GetNearestAiNavNode(Vector3 position)
    {
        var nodes = AiNavNode.GetAiNavNodes();
        AiNavNode nearest = null;
        float nearestDistance = float.MaxValue;
        foreach (var node in nodes)
        {
            float distance = (position - node.transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearest = node;
                nearestDistance = distance;
            }
        }

        return nearest;
    }

    public void SortByDistance(List<AiNavNode> nodes)
    {
        nodes.Sort(CompareDistance);
    }

    public int CompareDistance(AiNavNode a, AiNavNode b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }

    #endregion
}
