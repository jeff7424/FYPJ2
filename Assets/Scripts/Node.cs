using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public enum NodeType{
		NODE_OPEN = 0,
		NODE_SPAWN,
		NODE_CORE,
		NODE_TOWER,
		NODE_OBSTACLE,
		NODE_TUNNEL,
		NODE_PLATFORM,
		NODE_MAX
	}

	public List<GameObject> LinkedNodes = new List<GameObject>();
	private NodeType type = NodeType.NODE_OPEN;
	public float F = 0.0f, G = 0.0f;
	//Pathfinding parent
	public GameObject parent = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float getF(Vector3 targetTile){
		return F = G + H(targetTile);
	}
	public float H(Vector3 targetTile){
		Vector2 distance = transform.position - targetTile;
		//return Mathf.Abs(distance.x) + Mathf.Abs(distance.y);				//Manhattan distance
		return Mathf.Sqrt(distance.x*distance.x + distance.y*distance.y);	//Euclidean distance
	}

	public void resetPathfindValues(){
		F = G = 0.0f;
		parent = null;
	}

	public void setNodeType(NodeType newType){
		type = newType;
		switch(type){
		case NodeType.NODE_OPEN:
			GetComponent<SpriteRenderer>().color = Color.white;
			break;

		case NodeType.NODE_TOWER:
			GetComponent<SpriteRenderer>().color = Color.black;
			break;
			
		case NodeType.NODE_OBSTACLE:
			GetComponent<SpriteRenderer>().color = Color.green;
			break;
			
		case NodeType.NODE_TUNNEL:
			GetComponent<SpriteRenderer>().color = Color.yellow;
			break;
			
		case NodeType.NODE_PLATFORM:
			GetComponent<SpriteRenderer>().color = Color.blue;
			break;
		}
	}
	public NodeType getNodeType(){return type;}
}
