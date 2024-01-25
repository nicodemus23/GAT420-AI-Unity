using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AiNavPath))]
public class AiNavAgent : AiAgent
{
	[SerializeField] private AiNavPath path;

	void Update()
	{
		if (path.HasTarget())
		{
			Debug.DrawLine(transform.position, path.destination);
			movement.MoveTowards(path.destination);
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
