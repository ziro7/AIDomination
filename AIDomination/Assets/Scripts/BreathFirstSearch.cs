using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class BreathFirstSearch : Pathfinder, ISearch
	{

		Queue<Waypoint> queue = new Queue<Waypoint>();

		void Start()
		{
			ColorStartAndEnd();
		}

		public override void Search()
		{
			queue.Enqueue(StartWaypoint);

			while (queue.Count > 0 && IsRunning)
			{
				SearchCenter = queue.Dequeue();
				HaltIfEndFound();
				ExploreNeighbours();
				SearchCenter.isExplored = true;
			}
		}

		public override void AddNewNeighbours(Vector2Int neighbourCoordinates)
		{
			Waypoint neighbour = Grid[neighbourCoordinates];
			if (neighbour.isExplored || queue.Contains(neighbour))
			{
				// do nothing
			}
			else
			{
				queue.Enqueue(neighbour);
				neighbour.exploredFrom = SearchCenter;
			}
		}
	}

}
