using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovementAI : MonoBehaviour {
	//The calculated path
	public List<GameObject> thePath;
	
	//The AI's speed per second
	public float speed;
	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 0.05f;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	private Vector3 prevNode;


	// Use this for initialization
	void Start () {
		thePath = new List<GameObject>();
		searchPath();

		speed = GetComponent<Enemy>().getSpeed();
	}

	public void FixedUpdate ()
	{
		if (thePath == null)
		{
			//We have no path to move after yet
			return;
		}

		//Reached end of path
		if (currentWaypoint >= thePath.Count)
		{
			thePath.Clear();
			currentWaypoint = 0;
			return;
		}

		//Direction to the next waypoint
		Vector3 dir = ( thePath[currentWaypoint].transform.position - transform.position ).normalized;
		dir *= speed * Time.deltaTime;
		this.gameObject.transform.Translate( dir );

		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance( transform.position, thePath[currentWaypoint].transform.position ) < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}

	public void searchPath(){
		thePath.Clear();
		currentWaypoint = 0;
		thePath = transform.parent.GetComponent<EnemyParentScript>().Pathfinder.FindPath(transform.position, GetComponent<Enemy>().getType());
	}
}
