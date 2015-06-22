using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public enum NodeType{
		NODE_OPEN = 0,
		NODE_TOWER,
		NODE_MAX
	}

	public List<GameObject> LinkedNodes = new List<GameObject>();
	public NodeType type = NodeType.NODE_OPEN;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float F(Vector3 currTile, Vector3 targetTile){
		return G(currTile) + H(targetTile);
	}
	public float G(Vector3 currTile){
		return (transform.position - currTile).magnitude;
	}
	public float H(Vector3 targetTile){
		return (transform.position - targetTile).magnitude;
	}
}
