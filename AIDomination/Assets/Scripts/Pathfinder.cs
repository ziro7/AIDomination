using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Pathfinder : MonoBehaviour
	{

		[SerializeField] Waypoint startWaypoint;
		[SerializeField] Waypoint endWaypoint;

		Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
		bool isRunning = true;
		Waypoint searchCenter;
		List<Waypoint> path = new List<Waypoint>();

		Vector2Int[] directions = {
			Vector2Int.up,
			Vector2Int.right,
			Vector2Int.down,
			Vector2Int.left
		};

		public Dictionary<Vector2Int, Waypoint> Grid
		{
			get { return grid; }
		}

		public Waypoint SearchCenter
		{
			get { return searchCenter; }
			set { searchCenter = value; }
		}

		public Waypoint StartWaypoint
		{
			get { return startWaypoint; }
		}

		public Waypoint EndWaypoint
		{
			get { return endWaypoint; }
		}

		public Boolean IsRunning
		{
			get { return isRunning; }
		}

		void Start()
		{
			ColorStartAndEnd();
		}

		public List<Waypoint> GetPath()
		{
			if (path.Count == 0)
			{
				CalculatePath();
			}
			return path;
		}

		protected void CalculatePath()
		{
			LoadBlocks();
			Search();
			CreatePath();
		}

		public virtual void Search()
		{
			throw new NotImplementedException();
		}

		protected void CreatePath()
		{
			SetAsPath(endWaypoint);
			endWaypoint.SetTopColor(Color.blue);

			Waypoint previous = endWaypoint.exploredFrom;
			while (previous != startWaypoint)
			{
				SetAsPath(previous);
				previous.SetTopColor(Color.blue);
				previous = previous.exploredFrom;
			}

			SetAsPath(startWaypoint);
			path.Reverse();
		}

		protected void SetAsPath(Waypoint waypoint)
		{
			path.Add(waypoint);
			waypoint.isPlaceable = false;
		}

		protected void HaltIfEndFound()
		{
			if (searchCenter == endWaypoint)
			{
				isRunning = false;
			}
		}

		protected void ExploreNeighbours()
		{
			if (!isRunning) { return; }

			foreach (Vector2Int direction in directions)
			{
				Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
				if (grid.ContainsKey(neighbourCoordinates))
				{
					AddNewNeighbours(neighbourCoordinates);
				}
			}
		}

		public virtual void AddNewNeighbours(Vector2Int neighbourCoordinates)
		{
			throw new NotImplementedException();
		}

		protected void LoadBlocks()
		{
			var waypoints = FindObjectsOfType<Waypoint>();
			foreach (Waypoint waypoint in waypoints)
			{
				var gridPos = waypoint.GetGridPos();
				if (grid.ContainsKey(gridPos))
				{
					Debug.LogWarning("Skipping overlapping block " + waypoint);
				}
				else
				{
					grid.Add(gridPos, waypoint);
				}
			}
		}

		public void ColorStartAndEnd()
		{

			startWaypoint.SetTopColor(Color.green);
			endWaypoint.SetTopColor(Color.red);
		}


	}
}

