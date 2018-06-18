using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class EnemyMovement : MonoBehaviour {

		[SerializeField] float movementPeriod = .5f;

		// Use this for initialization
		void Start ()
		{
			Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
			var path = pathfinder.GetPath();
			StartCoroutine(FollowPath(path));
		}
	
		// Update is called once per frame
		void Update () {

		
		}

		IEnumerator FollowPath(List<Waypoint> path)
		{
			int nextWaypoint = 1;

			foreach (Waypoint waypoint in path)
			{
				transform.position = waypoint.transform.position; //Moveing to next waypoint
				if (nextWaypoint < path.Count)
				{
					Vector3 relativePos = path[nextWaypoint].transform.position - transform.position;
					Quaternion rotation = Quaternion.LookRotation(relativePos);
					transform.rotation = rotation; //turns towards next waypoint
				}

				yield return new WaitForSeconds(movementPeriod);
				nextWaypoint++;

			}
		}
	}
}
