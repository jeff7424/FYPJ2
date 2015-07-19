using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovementAI : MonoBehaviour {
	//The calculated path
	public List<GameObject> thePath;
	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 0.05f;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	//The previous node enemy moved to
	private Node prevNode = null;


	// Use this for initialization
	void Start () {
		thePath = new List<GameObject>();
		searchPath();
	}

	public void FixedUpdate ()
	{
		if (thePath == null)
		{
			//We have no path to move after yet
			return;
		}
		
		//Reached end of path
		//--------------------------------------------------------
		if (currentWaypoint >= thePath.Count)
		{
			thePath.Clear();
			currentWaypoint = 0;
			return;
		}
		//--------------------------------------------------------

		//Direction to the next waypoint
		Vector3 dir = ( thePath[currentWaypoint].transform.position - transform.position ).normalized;
		dir *= GetComponent<Enemy>().getFinalSpeed() * Time.deltaTime;
		this.gameObject.transform.Translate( dir );

		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance( transform.position, thePath[currentWaypoint].transform.position ) < nextWaypointDistance)
		{
			prevNode = thePath[currentWaypoint].GetComponent<Node>();
			currentWaypoint++;


			//Behaviour change for certain enemy types
			//--------------------------------------------------------
			if(currentWaypoint < thePath.Count){
				switch(GetComponent<Enemy>().getType()){
				case Enemy.enemyType.TYPE_JUMP:
					if(thePath[currentWaypoint].GetComponent<Node>().getNodeType() == Node.NodeType.NODE_TOWER || prevNode.getNodeType() == Node.NodeType.NODE_TOWER){
						GetComponent<Enemy>().setSpeed(5.0f);
					}
					else
						GetComponent<Enemy>().setSpeed(GetComponent<Enemy>().getOriginalSpeed());
					break;
				}
			}
			//--------------------------------------------------------

			return;
		}
	}

	public void searchPath(){
		thePath.Clear();
		currentWaypoint = 0;
		thePath = transform.parent.GetComponent<EnemyParentScript>().Pathfinder.FindPath(transform.position, GetComponent<Enemy>().getType());
	}
}
