using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	enum soundclip {
		SOUND_DEPLOY		= 0,
		SOUND_BLOCKED		= 1,
		SOUND_INSUFFICIENT 	= 2,
		SOUND_REMOVE		= 3
	}

	public Defense defenses;
	private Defense defense;
	private GameObject game;
	private GameObject player;
	public bool isOccupied = false;
	public bool isMouseOver = false;
	private int cost = 0;
	private int selection = 1;
	public Tree trees;
	public Sprite tile;
	public Rect temp;
	private float errorDeployTime;

	public AudioClip[] sounds;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
		errorDeployTime = 0.0f;
		if (Application.loadedLevelName == "Game") {
			player = GameObject.Find ("Player");
		}
		else if (Application.loadedLevelName == "Multiplayer") {
			player = GameObject.Find (gameObject.tag);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!game.GetComponent<Game>().GetPause ()) {
			if (errorDeployTime > 0.0f) {
				errorDeployTime -= Time.deltaTime;
				GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, errorDeployTime);
			}
			if (Input.GetMouseButtonDown (0) && (!isMouseOver && !player.GetComponent<Player1>().mouseOverPanel)) {
				TileDeselected();
			}
		}
	}

	public void BuildDefense() {
		// Build tower when button is released
		defense = (Defense)Instantiate (defenses);
		defense.tag = gameObject.tag;
		defense.transform.position = transform.position;
		defense.transform.localScale = transform.localScale;
		defense.transform.parent = transform;
		isOccupied = true;

		player.GetComponent<Player1>().IncreaseDefenseUsed();

		//Update pathNode type and search path for all AI present
		GetComponent<Node>().setNodeType(Node.NodeType.NODE_TOWER);
		EnemyMovementAI[] enemyAIs = null;
		if (Application.loadedLevelName == "Game")
			enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		else if (Application.loadedLevelName == "Multiplayer") {
			if (gameObject.tag == "Player 1")
				enemyAIs = GameObject.Find("EnemyParent 1").GetComponentsInChildren<EnemyMovementAI>();
			else if (gameObject.tag == "Player 2")
				enemyAIs = GameObject.Find("EnemyParent 2").GetComponentsInChildren<EnemyMovementAI>();
		}
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
		}
	}

	public void DestroyDefense() {
		Destroy (defense.gameObject);
		isOccupied = false;
		selection = 1;
		TileDeselected();

		PlaySound (soundclip.SOUND_REMOVE);

		player.GetComponent<Player1>().IncreaseDefenseDeleted();
		
		//Update pathNode type and search path for all AI present
		GetComponent<Node>().setNodeType(Node.NodeType.NODE_OPEN);
		EnemyMovementAI[] enemyAIs = null;
		if (Application.loadedLevelName == "Game")
			enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		else if (Application.loadedLevelName == "Multiplayer") {
			if (gameObject.tag == "Player 1")
				enemyAIs = GameObject.Find("EnemyParent 1").GetComponentsInChildren<EnemyMovementAI>();
			else if (gameObject.tag == "Player 2")
				enemyAIs = GameObject.Find("EnemyParent 2").GetComponentsInChildren<EnemyMovementAI>();
		}
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
		}
	}

	public void RankUpDefense() {
		if (game.GetComponent<Game>().GetPause () == false) {
			int upgradeCost = defense.GetComponent<Defense>().GetCost();
			if (player.GetComponent<Player1> ().resources - upgradeCost >= 0) {
				defense.GetComponent<Defense> ().RankUp ();
				player.GetComponent<Player1>().resources -= upgradeCost;
			} else {
				DisplayDeployError("NOT ENOUGH RESOURCES!");
				PlaySound (soundclip.SOUND_INSUFFICIENT);
			}
		}
	}

#if UNITY_EDITOR
	void OnMouseEnter() {
		TouchOnTile();
	}

	void OnMouseExit() {
		TouchExitTile ();
	}

	void OnMouseDown() {
		CheckDeploy ();
	}
#endif

	public void CheckDeploy() {
		if (game.GetComponent<Game>().GetPause () == false) {
			selection = player.GetComponent<Player1>().selection;
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
				if (player.GetComponent<Player1> ().resources - cost >= 0) {
					if (!checkAIPath ()) {	//If monsters cannot find a path to the core
						//Debug.Log ("Monsters cannot pass through");
						DisplayDeployError("BLOCKED!");
						PlaySound (soundclip.SOUND_BLOCKED);
					}
					else if (GetComponent<Node>().getNodeType() == Node.NodeType.NODE_OPEN || GetComponent<Node>().getNodeType() == Node.NodeType.NODE_PLATFORM) {
						BuildDefense ();
						PlaySound (soundclip.SOUND_DEPLOY);
						player.GetComponent<Player1> ().DisableInfoPanel ();
						player.GetComponent<Player1> ().resources -= cost;
					}
				} else {
					DisplayDeployError("NOT ENOUGH RESOURCES!");
					PlaySound (soundclip.SOUND_INSUFFICIENT);
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

	public void TouchOnTile() {
		if (game.GetComponent<Game>().GetPause () == false && 
		    game.GetComponent<Game>().endGame == false &&
		    GetComponent<Node>().getNodeType () == Node.NodeType.NODE_OPEN || 
		    GetComponent<Node>().getNodeType () == Node.NodeType.NODE_TOWER || 
		    GetComponent<Node>().getNodeType () == Node.NodeType.NODE_PLATFORM) {
			this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			player.GetComponent<Player1> ().mouseOverTile = true;
			isMouseOver = true;
		}
	}

	public void TouchExitTile() {
		// Revert back to original sprite
		if (game.GetComponent<Game>().GetPause () == false && 
		    game.GetComponent<Game>().endGame == false &&
		    GetComponent<Node>().getNodeType () == Node.NodeType.NODE_OPEN || 
		    GetComponent<Node>().getNodeType () == Node.NodeType.NODE_TOWER || 
		    GetComponent<Node>().getNodeType () == Node.NodeType.NODE_PLATFORM) {
			this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			player.GetComponent<Player1> ().mouseOverTile = false;
			isMouseOver = false;
		}
	}

	void PlaySound(soundclip sound) {
		GetComponent<AudioSource>().clip = sounds[(int)sound];
		GetComponent<AudioSource>().Play ();
	}

	public void DisplayInfo() {
		player.GetComponent<Player1> ().EnableInfoPanel ();
		player.GetComponent<Player1> ().infoPanel.GetComponent<InfoPanelScript> ().tile = this;
		player.GetComponent<Player1> ().infoPanel.GetComponent<InfoPanelScript> ().defense = this.defense;
	}

	public void TileSelected() {
		if (Application.loadedLevelName == "Game")
			this.transform.localScale = new Vector3 (1.2f, 1.2f, 1.0f);
		else if (Application.loadedLevelName == "Multiplayer")
			this.transform.localScale = new Vector3 (0.9f, 0.9f, 1.0f);
	}

	public void TileDeselected() {
		if (Application.loadedLevelName == "Game")
			this.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		else if (Application.loadedLevelName == "Multiplayer")
			this.transform.localScale = new Vector3 (0.75f, 0.75f, 1.0f);
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
		if (Application.loadedLevelName == "Game") {
			path = GameObject.Find("Pathfinder").GetComponent<PathfinderScript>().FindPath(GameObject.Find("enemySpawn5").transform.position, Enemy.enemyType.TYPE_NORMAL);
		}
		else if (Application.loadedLevelName == "Multiplayer") {
			if (gameObject.tag == "Player 1")
				path = GameObject.Find("Pathfinder 1").GetComponent<PathfinderScript>().FindPath(GameObject.Find("enemySpawn5").transform.position, Enemy.enemyType.TYPE_NORMAL);
			else if (gameObject.tag == "Player 2")
				path = GameObject.Find("Pathfinder 2").GetComponent<PathfinderScript>().FindPath(GameObject.Find("enemySpawn5").transform.position, Enemy.enemyType.TYPE_NORMAL);
		}
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

	void DisplayDeployError(string errmsg) {
		player.GetComponent<Player1>().DisplayErrorMsg(errmsg);
		errorDeployTime = 3.0f;
	}
}
