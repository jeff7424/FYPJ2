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
				switch(type){
				case Enemy.enemyType.TYPE_NORMAL:
				case Enemy.enemyType.TYPE_SLOW:
				case Enemy.enemyType.TYPE_FAST:
					if(node.GetComponent<Node>().type == Node.NodeType.NODE_TOWER)
						closedset.Add(node);
					break;
				}
			}
		}
		
		
		//Add initial node to the path
		path.Add(closestNode);

		//Pathfinding
		bool pathFound = false;
		Node currNode = closestNode.GetComponent<Node>();

		while(!pathFound){
			//Finish search if path has been found
			if(currNode == coreNode.GetComponent<Node>()){
				pathFound = true;
				break;
			}

			//Find linked nodes that can be traversed
			List<GameObject> openLinkedNodes = new List<GameObject>();
			foreach(GameObject node in currNode.LinkedNodes){
				//Check which nodes are in closed list first
				if(!closedset.Contains(node))
					openLinkedNodes.Add(node);
			}

			if(openLinkedNodes.Count > 0){
				//Find open linked node with lowest F
				float lowestF = 2000.0f;
				GameObject selectedNode = null;
				foreach(GameObject openNode in openLinkedNodes){
					float F = openNode.GetComponent<Node>().F(closestNode.transform.position, coreNode.transform.position);
					if(F < lowestF){
						lowestF = F;
						selectedNode = openNode;
					}
				}
				//Add selectedNode to path
				path.Add(selectedNode);
				closedset.Add(currNode.gameObject);
				currNode = selectedNode.GetComponent<Node>();
			}
			else{
				//Reverse search for node with openLinkedNodes > 0
				for(int i = path.Count-1; i > -1; --i){
					List<GameObject> openNodes = new List<GameObject>();
					foreach(GameObject node in path[i].GetComponent<Node>().LinkedNodes){
						//Check which nodes are in closed list first
						if(!closedset.Contains(node))
							openLinkedNodes.Add(node);
					}

					//Node has an open node
					if(openNodes.Count > 0){
						currNode = path[i].GetComponent<Node>();
						//Cleanup and remove path that leads to dead end
						for(int j = path.Count-1; j > i; --j){
							path.RemoveAt(j);
						}
						break;
					}
					else{
						//If all nodes in the path do not have a openLinkedNode, just send the path back
						if(i == 0){
							pathFound = true;
							break;
						}
					}
				}
			}
		}


		//Cleanup final path
		for(int i = 0; i < path.Count; ++i){
			for(int j = i+1; j < path.Count; ++j){
				//If there is a path that leads back to where AI has moved before
				if(path[j] == path[i]){
					for(int k = j; k > i; --k){
						path.RemoveAt(k);
					}
					j = i+1;
				}
			}
		}


		return path;
	}


}
