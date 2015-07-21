using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public Defense defenses;
	private Defense defense;
	private GameObject game;
	public bool isOccupied = false;
	public bool isMouseOver = false;
	private int cost = 0;
	private int selection = 1;
	public Tree trees;
	public Sprite tile;
	public Rect temp;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
	}
	
	// Update is called once per frame
	void Update () {
//		if (defense == null) {
//			isOccupied = false;
//			selection = 1;
//		}
		if (Input.GetMouseButtonDown (0) && (!isMouseOver && !game.GetComponent<Game>().mouseOverPanel)) {
			TileDeselected();
		}
	}

	public void BuildDefense() {
		// Build tower when button is released
		defense = (Defense)Instantiate (defenses);
		defense.transform.position = transform.position;
		defense.transform.parent = transform;
		isOccupied = true;

		game.GetComponent<Game>().IncreaseDefenseUsed();

		//Update pathNode type and search path for all AI present
		GetComponent<Node>().setNodeType(Node.NodeType.NODE_TOWER);
		EnemyMovementAI[] enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
		}
	}

	public void DestroyDefense() {
		Destroy (defense.gameObject);
		isOccupied = false;
		selection = 1;
		TileDeselected();

		game.GetComponent<Game>().IncreaseDefenseDeleted();
		
		//Update pathNode type and search path for all AI present
		GetComponent<Node>().setNodeType(Node.NodeType.NODE_OPEN);
		EnemyMovementAI[] enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
		}
	}

	public void RankUpDefense() {
		defense.GetComponent<Defense> ().RankUp ();
	}

	void OnMouseEnter() {
		if (game.GetComponent<Game>().isPause == false && GetComponent<Node>().getNodeType () == Node.NodeType.NODE_OPEN || GetComponent<Node>().getNodeType () == Node.NodeType.NODE_TOWER) {
			this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			game.GetComponent<Game> ().mouseOverTile = true;
			isMouseOver = true;
		}
	}

	void OnMouseExit() {
		// Revert back to original sprite
		//this.GetComponent<SpriteRenderer> ().sprite = tile;
		if (game.GetComponent<Game>().isPause == false && GetComponent<Node>().getNodeType () == Node.NodeType.NODE_OPEN || GetComponent<Node>().getNodeType () == Node.NodeType.NODE_TOWER) {
			this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			game.GetComponent<Game> ().mouseOverTile = false;
			isMouseOver = false;
		}
	}

	void OnMouseDown() {
		if (game.GetComponent<Game>().isPause == false) {
			selection = game.GetComponent<Game>().selection;
			switch (selection) {
			case (1):
				cost = (int)Game.defenseCost.DEF_CANNON;
				break;
			case (2):
				cost = (int)Game.defenseCost.DEF_TURRET;
				break;
			case (3):
				cost = (int)Game.defenseCost.DEF_SLOW;
				break;
			case (4):
				cost = (int)Game.defenseCost.DEF_ANTIAIR;
				break;
			case (5):
				cost = (int)Game.defenseCost.DEF_FLAME;
				break;
			}
			
			if (!isOccupied && isMouseOver && selection != 0) {
				if (game.GetComponent<Game> ().resources - cost >= 0) {
					if (!checkAIPath ())		//If monsters cannot find a path to the core
						Debug.Log ("Monsters cannot pass through");
					else if (GetComponent<Node>().getNodeType() == Node.NodeType.NODE_OPEN || GetComponent<Node>().getNodeType() == Node.NodeType.NODE_PLATFORM) {
						BuildDefense ();
						game.GetComponent<Game> ().DisableInfoPanel ();
						game.GetComponent<Game> ().resources -= cost;
					}
				}
			} else {
				if (isOccupied && GetComponent<Node>().getNodeType() == Node.NodeType.NODE_TOWER) {
					// level up defense
					TileSelected ();
					DisplayInfo ();
				}
			}
		}
	}

	public void DisplayInfo() {
		game.GetComponent<Game> ().EnableInfoPanel ();
		game.GetComponent<Game> ().infoPanel.GetComponent<InfoPanelScript> ().defense = defense;	
		game.GetComponent<Game> ().infoPanel.GetComponent<InfoPanelScript> ().tile = this;
	}

	public void TileSelected() {
		this.transform.localScale = new Vector3 (1.2f, 1.2f, 1.0f);
	}

	public void TileDeselected() {
		this.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
	}

	public Vector2 TilePosition() {
		return transform.position;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Obstacle") {
			isOccupied = true;
		}
	}

	//Returns true if normal monsters are able to find a path to the core
	bool checkAIPath(){
		GetComponent<Node>().setNodeType(Node.NodeType.NODE_TOWER);
		List<GameObject> path = new List<GameObject>();
		path = GameObject.Find("Pathfinder").GetComponent<PathfinderScript>().FindPath(GameObject.Find("enemySpawn5").transform.position, Enemy.enemyType.TYPE_NORMAL);
		GetComponent<Node>().setNodeType(Node.NodeType.NODE_OPEN);
		
		if(path.Count > 0)
			//Normal monsters are still able to reach the core
			return true;
		else
			return false;
	}

	public GameObject ReturnDefense() {
		return defense.gameObject;
	}

	public void TowerRage(float duration, float newValue) {
		defense.Rage (duration, newValue);
	}
}
