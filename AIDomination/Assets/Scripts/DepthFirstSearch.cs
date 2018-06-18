using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class DepthFirstSearch : Pathfinder, ISearch {

	Stack<Waypoint> stack = new Stack<Waypoint>();

	void Start()
	{
		ColorStartAndEnd();
	}

	public override void Search()
	{
		stack.Push(StartWaypoint);

		while (stack.Count > 0 && IsRunning)
		{
			SearchCenter = stack.Pop();
			HaltIfEndFound();
			ExploreNeighbours();
			SearchCenter.isExplored = true;
		}
	}

	public override void AddNewNeighbours(Vector2Int neighbourCoordinates)
	{
		Waypoint neighbour = Grid[neighbourCoordinates];
		if (neighbour.isExplored || stack.Contains(neighbour))
		{
			// do nothing
		}
		else
		{
			stack.Push(neighbour);
			neighbour.exploredFrom = SearchCenter;
		}
	}

}
