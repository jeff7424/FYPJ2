using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public enum defenseCost
	{
		DEF_CANON   = 3,
		DEF_TURRET  = 3,
		DEF_SLOW    = 5,
		DEF_ANTIAIR = 6,
	}

	public Defense defenses;
	private Defense defense;
	private GameObject game;
	public bool isOccupied = false;
	public bool isMouseOver = false;
	public bool deleteDefense = false;
	private int cost = 0;
	private int selection = 1;
	public Tree trees;
	public Sprite theBase;
	public Sprite tile;
	public Sprite weapon;
	public Texture2D current;
	public Texture2D[] weapontype;
	public Rect temp;
	//private GameObject info_panel;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
	}
	
	// Update is called once per frame
	void Update () {
		if (defense == null) {
			isOccupied = false;
			deleteDefense = false;
			selection = 1;
		}
		if (Input.GetMouseButtonDown (0) && !isMouseOver) {
			TileDeselected();
		}
	}

	public void BuildDefense() {
		// Build tower when button is released
		defense = (Defense)Instantiate (defenses);
		defense.transform.position = transform.position;
		defense.transform.SetParent (gameObject.transform);
		isOccupied = true;
		game.GetComponent<Game> ().DisableInfoPanel ();

		//Update pathNode type and search path for all AI present
		GetComponent<Node>().type = Node.NodeType.NODE_TOWER;
		EnemyMovementAI[] enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
		}
	}

	public void DestroyDefense() {
		Destroy (defense.gameObject);
		isOccupied = false;
		deleteDefense = false;
		selection = 1;
		
		//Update pathNode type and search path for all AI present
		GetComponent<Node>().type = Node.NodeType.NODE_OPEN;
		EnemyMovementAI[] enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
		}
	}

	public void RankUpDefense() {
		defense.GetComponent<Defense> ().RankUp ();
	}

	void RenderGhost() {
		//selection = (int)defenses.GetComponent<Defense>().selection;
		//current = weapontype [selection - 1];
		//weapon = Sprite.Create (current, new Rect (0, 0, current.width, current.height), new Vector2(0.5f, 0.5f));

		//this.GetComponent<SpriteRenderer> ().sprite = weapon;
		this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);

		//temp = new Rect (0, 0, current.width, current.height);
	}

	void OnMouseEnter() {
		RenderGhost ();
		this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		game.GetComponent<Game> ().mouseOverTile = true;
		isMouseOver = true;
	}

	void OnMouseExit() {
		// Revert back to original sprite
		//this.GetComponent<SpriteRenderer> ().sprite = tile;
		this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		game.GetComponent<Game> ().mouseOverTile = false;
		isMouseOver = false;
	}

	void OnMouseDown() {
		//GameObject game = GameObject.Find ("Game");
		selection = game.GetComponent<Game>().selection;
		if (selection == 0) {
			deleteDefense = true;
		} else {
			deleteDefense = false;
			switch (selection) {
			case (1):
				cost = (int)defenseCost.DEF_CANON;
				break;
			case (2):
				cost = (int)defenseCost.DEF_TURRET;
				break;
			case (3):
				cost = (int)defenseCost.DEF_SLOW;
				break;
			case (4):
				cost = (int)defenseCost.DEF_ANTIAIR;
				break;
			}
		}
		
		if (!isOccupied && game.GetComponent<Game> ().resources - cost >= 0 && !deleteDefense) {
			if (!checkAIPath ())
				Debug.Log ("Monsters cannot pass through");
			else {
				BuildDefense ();
				game.GetComponent<Game> ().resources -= cost;
			}
		} else {
			if (isOccupied && deleteDefense) {
				DestroyDefense ();
			} else if (isOccupied && !deleteDefense) {
				// level up defense
				TileSelected ();
				DisplayInfo ();
			}
		}
	}

	public void DisplayInfo() {
		game.GetComponent<Game> ().EnableInfoPanel ();
		//Debug.Log ("Enabled");
		game.GetComponent<Game> ().infoPanel.GetComponent<InfoPanelScript> ().defense = defense;	
		game.GetComponent<Game> ().infoPanel.GetComponent<InfoPanelScript> ().tile = this;	
	}

	public void TileSelected() {
		//this.transform.localScale = new Vector3 (0.7f, 0.7f, 1.0f);
		this.GetComponent<SpriteRenderer> ().color = new Color (0.3f, 0.3f, 0.7f, 0.5f);

	}

	public void TileDeselected() {
		//this.transform.localScale = new Vector3 (0.55f, 0.55f, 1.0f);
		this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
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
		GetComponent<Node>().type = Node.NodeType.NODE_TOWER;
		List<GameObject> path = new List<GameObject>();
		path = GameObject.Find("Pathfinder").GetComponent<PathfinderScript>().FindPath(GameObject.Find("enemySpawn5").transform.position, Enemy.enemyType.TYPE_NORMAL);
		GetComponent<Node>().type = Node.NodeType.NODE_OPEN;
		
		if(path.Count > 0)
			//Normal monsters are still able to reach the core
			return true;
		else
			return false;
	}
}
