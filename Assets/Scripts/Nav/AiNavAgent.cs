using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiNavPath))]
public class AiNavAgent : AiAgent
{
	[SerializeField] private AiNavPath path;

	void Update()
	{
		if (path.HasPath())
		{
			Debug.DrawLine(transform.position, path.destination);
			movement.MoveTowards(path.destination);
		}
	}
}
