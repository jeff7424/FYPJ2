using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyMovementAI : MonoBehaviour {
	private Seeker seeker;

	//The calculated path
	public Path path;
	
	//The AI's speed per second
	public float speed = 1.0f;
	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 0.05f;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();

		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		FindPath(GameObject.Find("enemyTargetPoint").transform.position);

		speed = GetComponent<Enemy>().getSpeed();
	}
	
	public void OnPathComplete ( Path p )
	{
		if (!p.error)
		{
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
	
	public void FixedUpdate ()
	{
		if (path == null)
		{
			//We have no path to move after yet
			return;
		}

		//Reached end of path
		if (currentWaypoint >= path.vectorPath.Count)
		{
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = ( path.vectorPath[ currentWaypoint ] - transform.position ).normalized;
		dir *= speed * Time.fixedDeltaTime;
		this.gameObject.transform.Translate( dir );
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance( transform.position, path.vectorPath[ currentWaypoint ] ) < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}

	public void FindPath(Vector3 targetPos){
		seeker.StartPath( transform.position, targetPos, OnPathComplete );
	}
}
