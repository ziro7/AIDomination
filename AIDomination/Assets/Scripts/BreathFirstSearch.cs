using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts
{
	public class BreathFirstSearch : Pathfinder, ISearch
	{
		[SerializeField] float delay = 0.1f;
		Queue<Waypoint> queue = new Queue<Waypoint>();
		public bool SearchIsDone = false;

		void Start()
		{
			ColorStartAndEnd();
		}

		public override void Search()
		{
			StartCoroutine(SearchWithDelay());
			while (!SearchIsDone)
			{
				Thread.Sleep(1000);
			}
		}

		IEnumerator SearchWithDelay()
		{
			queue.Enqueue(StartWaypoint);
			yield return new WaitForSeconds(delay);

			while (queue.Count > 0 && IsRunning)
			{
				SearchCenter = queue.Dequeue();
				HaltIfEndFound();
				ExploreNeighbours();
				SearchCenter.isExplored = true;
				SearchCenter.SetTopColor(Color.grey);
				yield return new WaitForSeconds(delay);
			}

			SearchIsDone = true;
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
