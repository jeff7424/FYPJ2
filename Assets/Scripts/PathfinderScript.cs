using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathfinderScript : MonoBehaviour {
	public GameObject[][] Nodes;
	public GameObject coreNode;

	// Use this for initialization
	void Awake(){
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateNodesArray(int rows, int columns){
		Nodes = new GameObject[rows][];

		for(int i = 0; i < rows; ++i){
			Nodes[i] = new GameObject[columns];
		}
	}

	public List<GameObject> FindPath(Vector2 start, Enemy.enemyType type){
		List<GameObject> path = new List<GameObject>();	//The final path
		List<GameObject> closedset = new List<GameObject>();	//The set of nodes already evaluated
		List<GameObject> openset = new List<GameObject>();


		//Find the closest node to start position
		float shortestDist = 2000.0f;
		GameObject closestNode = null;

		for(int i = 0; i < Nodes.GetLength(0); ++i){
			foreach(GameObject node in Nodes[i]){
				if((start - (Vector2)node.transform.position).magnitude < shortestDist){
					shortestDist = (start - (Vector2)node.transform.position).magnitude;
					closestNode = node;
				}
				
				//Check which nodes are obstacles to the enemy type
				//Add them to closedset
				Node.NodeType theType = node.GetComponent<Node>().getNodeType();
				switch(type){
				case Enemy.enemyType.TYPE_NORMAL:
				case Enemy.enemyType.TYPE_SLOW:
				case Enemy.enemyType.TYPE_FAST:
					if(theType == Node.NodeType.NODE_TOWER ||
					   theType == Node.NodeType.NODE_OBSTACLE ||
					   theType == Node.NodeType.NODE_PLATFORM)
						closedset.Add(node);
					break;

				case Enemy.enemyType.TYPE_JUMP:
					if(theType == Node.NodeType.NODE_OBSTACLE ||
					   theType == Node.NodeType.NODE_PLATFORM)
						closedset.Add(node);
					break;

				case Enemy.enemyType.TYPE_FLY:
					if(theType == Node.NodeType.NODE_PLATFORM)
						closedset.Add(node);
					break;
				}
			}
		}

		//Add starting node to the open set
		openset.Add(closestNode);

		//Pathfinding
		bool pathFound = false;
		Node currNode = closestNode.GetComponent<Node>();

		while(!pathFound){
			//Get the node with the lowest F value in openset
			float lowestF = 2000.0f;
			GameObject selectedNode = null;
			foreach(GameObject node in openset){
				if(node.GetComponent<Node>().getF(coreNode.transform.position) < lowestF){
					lowestF = node.GetComponent<Node>().F;
					selectedNode = node;
				}
			}
			if(selectedNode == null)
				break;
			else
				currNode = selectedNode.GetComponent<Node>();


			//If the node is the core(target)
			if(currNode == coreNode.GetComponent<Node>()){
				//Done
				while(currNode.parent != null){
					path.Add(currNode.gameObject);
					currNode = currNode.parent.GetComponent<Node>();
				}
				path.Reverse();
				pathFound = true;
			}
			else{
				//Remove current node from openset and move it to closedset
				openset.Remove(currNode.gameObject);
				closedset.Add(currNode.gameObject);

				//Consider every neighbour of the current node
				foreach(GameObject neighbour in currNode.GetComponent<Node>().LinkedNodes){
					//If it is in the closedset, move on to the next neighbour
					if(closedset.Contains(neighbour)){
						continue;
					}
					//If it is in the openset and current node's G < neighbour's G
					//Set current node as the neighbour's parent & update it's G with the new and lower one
					else if(openset.Contains(neighbour) && currNode.GetComponent<Node>().G <= neighbour.GetComponent<Node>().G){
						neighbour.GetComponent<Node>().parent = currNode.gameObject;
						neighbour.GetComponent<Node>().G = currNode.GetComponent<Node>().G + 1;
					}
					//Node is not in the closed set or open set
					else{
						openset.Add(neighbour);
						neighbour.GetComponent<Node>().parent = currNode.gameObject;
						neighbour.GetComponent<Node>().G = currNode.GetComponent<Node>().G + 1;
					}
				}
			}
		}


		//Cleanup final path
		for(int i = 0; i < path.Count; ++i){
			for(int j = i+1; j < path.Count; ++j){
				//If there is a path that leads back to where the AI has moved before
				if(path[j] == path[i]){
					for(int k = j; k > i; --k){
						path.RemoveAt(k);
					}
					j = i+1;
				}
			}
		}
		
		//Reset F & G values for every node that was affected
		foreach(GameObject node in closedset)
			node.GetComponent<Node>().resetPathfindValues();
		foreach(GameObject node in openset)
			node.GetComponent<Node>().resetPathfindValues();

		return path;
	}


}
