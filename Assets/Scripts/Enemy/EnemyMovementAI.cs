using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovementAI : MonoBehaviour {

	GameObject game;

	//The calculated path
	public List<GameObject> thePath;
	
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 0.05f;
	
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	//The previous node enemy moved to
	private Node prevNode = null;

	Quaternion facingdir;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
		thePath = new List<GameObject>();
		searchPath();
	}

	public void FixedUpdate ()
	{
		if (!game.GetComponent<Game>().GetPause ()) {
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
			this.gameObject.transform.Translate( dir, Space.World );
			//Rotate sprite
			if(dir.y < 0)
				this.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(new Vector2(-1, 0), dir));
			else if(dir.y > 0)
				this.transform.rotation = Quaternion.Euler(0, 0, -Vector2.Angle(new Vector2(-1, 0), dir));

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
					case Enemy.enemyType.TYPE_NORMAL:
					case Enemy.enemyType.TYPE_FAST:
					case Enemy.enemyType.TYPE_SLOW:
					case Enemy.enemyType.TYPE_SPLIT_PARENT:
					case Enemy.enemyType.TYPE_SPLIT_CHILD:
						if(thePath[currentWaypoint].GetComponent<Node>().getNodeType() == Node.NodeType.NODE_TOWER ||
						   thePath[currentWaypoint].GetComponent<Node>().getNodeType() == Node.NodeType.NODE_OBSTACLE ||
						   thePath[currentWaypoint].GetComponent<Node>().getNodeType() == Node.NodeType.NODE_PLATFORM)
							searchPath();
						break;

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
	}

	public void searchPath(){
		thePath.Clear();
		currentWaypoint = 0;
		thePath = transform.parent.GetComponent<EnemyParentScript>().Pathfinder.FindPath(transform.position, GetComponent<Enemy>().getType());
	}
}
