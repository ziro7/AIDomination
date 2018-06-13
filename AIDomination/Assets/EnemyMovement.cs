using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] List<Waypoint> _path = new List<Waypoint>();

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(FollowPath());
	}
	
	// Update is called once per frame
	void Update () {



		
	}

	private IEnumerator FollowPath()
	{
		int nextWaypoint = 1;

		Debug.Log("Starting Patrol..");
		foreach (var waypoint in _path)
		{
			transform.position = waypoint.transform.position; //Moveing to next waypoint
			if (nextWaypoint < _path.Count)
			{
				Vector3 relativePos = _path[nextWaypoint].transform.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos); 
				transform.rotation = rotation; //turns towards next waypoint
			}
			Debug.Log("Visting " + waypoint.name);
			yield return new WaitForSeconds(1f);
			nextWaypoint++;
			
		}
		Debug.Log("Ending Patrol..");
	}
}
