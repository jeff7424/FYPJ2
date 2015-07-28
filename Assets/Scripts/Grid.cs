using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	// Script to create a grid of tiles
	public GameObject thePathfinderRoot;
	public GameObject EnemySpawner;
	public GameObject Core;
	public ObstacleSpawnMaster ObstacleSpawn;
	public SpawnObstacles spawnObs;
	public PathfinderScript thePathfinder;

	public GameObject node;
	public GameObject tile;
	public int numberOfTilesColumn = 10;
	public int numberOfTilesRow = 9;
	public float tileSize;

	//Testing
	public GameObject TreeObstacle;
	public GameObject TunnelObstacle;
	public Sprite PlatformObstacle;
	public int maxSpawn;

	private int level = 0;
	private int currTile = 0;

	// Use this for initialization
	void Awake () {
		level = PlayerPrefs.GetInt ("level"); //game.GetComponent<Game>().level;
		CreateTiles ();
		maxSpawn = 0;
		if (Application.loadedLevelName == "Game")
			tileSize = 1.0f;
		else if (Application.loadedLevelName == "Multiplayer")
			tileSize = 0.75f;
	}

	void CreateTiles() {
		float xOffset = 0.0f;
		float yOffset = 0.0f;
		thePathfinder.GetComponent<PathfinderScript>().CreateNodesArray(numberOfTilesRow, numberOfTilesColumn+1);

		for (int y = 0; y < numberOfTilesRow;) {
			for (int x = 0; x < numberOfTilesColumn; ++x) {
				
				// Do offset after instantiating a tile
				if (Application.loadedLevelName == "Game")
					xOffset += tileSize;
				else if (Application.loadedLevelName == "Multiplayer") {
					if (gameObject.tag == "Player 1")
						xOffset += tileSize;
					else if (gameObject.tag == "Player 2")
						xOffset -= tileSize;
				}

				// Reset x to the start and +1 to y to start next row
				if (x % numberOfTilesColumn == 0)
				{
					if (Application.loadedLevelName == "Game")
						yOffset += tileSize;
					else if (Application.loadedLevelName == "Multiplayer") {
						if (gameObject.tag == "Player 1")
							yOffset += tileSize;
						else if (gameObject.tag == "Player 2")
							yOffset -= tileSize;
					}
					xOffset = 0;
					y++;

					//Create a node that serves as enemy spawn points

					GameObject newEnemySpawnNode = null;
					if (Application.loadedLevelName == "Game")
						newEnemySpawnNode = (GameObject)Instantiate (node, new Vector2(transform.position.x + tileSize*numberOfTilesColumn, transform.position.y + yOffset), Quaternion.identity);
					else if (Application.loadedLevelName == "Multiplayer") {
						if (gameObject.tag == "Player 1")
							newEnemySpawnNode = (GameObject)Instantiate (node, new Vector2(transform.position.x + tileSize*numberOfTilesColumn, transform.position.y + yOffset), Quaternion.identity);
						else if (gameObject.tag == "Player 2")
							newEnemySpawnNode = (GameObject)Instantiate (node, new Vector2(transform.position.x - tileSize*numberOfTilesColumn, transform.position.y + yOffset), Quaternion.identity);
					}
					newEnemySpawnNode.name = "enemySpawn" + y.ToString();
					newEnemySpawnNode.transform.SetParent(thePathfinderRoot.transform);
					EnemySpawner.GetComponent<EnemySpawner>().spawnNodes[y-1] = newEnemySpawnNode;
					
					//Linking nodes together
					thePathfinder.Nodes[y-1][numberOfTilesColumn] = newEnemySpawnNode;
					if(y > 1){
						newEnemySpawnNode.GetComponent<Node>().LinkedNodes.Add(thePathfinder.Nodes[y-2][numberOfTilesColumn]);
						thePathfinder.Nodes[y-2][numberOfTilesColumn].GetComponent<Node>().LinkedNodes.Add(newEnemySpawnNode);
					}
				}

				//Create the tile
				GameObject newTile = (GameObject)Instantiate (tile, new Vector2(transform.position.x + xOffset*tileSize, transform.position.y + yOffset*tileSize), Quaternion.identity);
				newTile.name = "Tile" + ((y-1)*9+x).ToString();
				newTile.tag = gameObject.tag;
				//newTile.transform.localScale = new Vector3(tileSize, tileSize, 1.0f);
				newTile.transform.SetParent(thePathfinderRoot.transform);

				currTile++;

				if (Application.loadedLevelName != "Multiplayer") {
					for(int i = 0; i < spawnObs.levels.Count; i++)
					{
						if(level == spawnObs.levels[i].level)
						{
							for(int j  = 0; j < spawnObs.levels[i].TreePos.Length; j++)
							{
								if(currTile == spawnObs.levels[i].TreePos[j])
								{
									Instantiate (TreeObstacle, newTile.transform.position, Quaternion.identity);
									newTile.GetComponent<Tile>().isOccupied = true;
									newTile.GetComponent<Node>().setNodeType(Node.NodeType.NODE_OBSTACLE);
								}
							}
							for(int k = 0; k < spawnObs.levels[i].PlatformPos.Length; k++)
							{
								if(currTile == spawnObs.levels[i].PlatformPos[k])
								{
									newTile.GetComponent<SpriteRenderer>().sprite = PlatformObstacle;
									newTile.GetComponent<Node>().setNodeType(Node.NodeType.NODE_PLATFORM);
								}
							}
							for(int l = 0; l < spawnObs.levels[i].TunnelPos.Length; l++)
							{
								if(currTile == spawnObs.levels[i].TunnelPos[l])
								{
									Instantiate (TunnelObstacle, newTile.transform.position, Quaternion.identity);
									newTile.GetComponent<Tile>().isOccupied = true;
									newTile.GetComponent<Node>().setNodeType(Node.NodeType.NODE_TUNNEL);
								}
							}
							break;
						}
					}
				}

				/*if (Application.loadedLevelName != "Multiplayer") {
					for (int i = 0; i < ObstacleSpawn.levels[level].type.Count; ++i) {
							for (int j = 0; j < ObstacleSpawn.levels[level].type[i].quantity.Count; ++j) {
								if (currTile == ObstacleSpawn.levels[level].type[i].quantity[j]) {
									if (i == (int)ObstacleSpawnMaster.ObstacleType.obstacleType.OBS_OBSTACLE) {
										GameObject newObstacle = (GameObject)Instantiate (obstacleTile, newTile.transform.position, Quaternion.identity);
										newTile.GetComponent<Tile>().isOccupied = true;
										newTile.GetComponent<Node>().setNodeType(Node.NodeType.NODE_OBSTACLE);
									} else if (i == (int)ObstacleSpawnMaster.ObstacleType.obstacleType.OBS_PLATFORM) {
										newTile.GetComponent<SpriteRenderer>().color = Color.black;
										newTile.GetComponent<Node>().setNodeType(Node.NodeType.NODE_PLATFORM);
									} else if (i == (int)ObstacleSpawnMaster.ObstacleType.obstacleType.OBS_TUNNEL) {
										newTile.GetComponent<SpriteRenderer>().color = Color.green;
										newTile.GetComponent<Tile>().isOccupied = true;
										newTile.GetComponent<Node>().setNodeType(Node.NodeType.NODE_TUNNEL);
									}
								}
							}
					}
				}*/

				//Linking nodes together
				thePathfinder.Nodes[y-1][x] = newTile;
				if(y > 1){
					newTile.GetComponent<Node>().LinkedNodes.Add(thePathfinder.Nodes[y-2][x]);
					thePathfinder.Nodes[y-2][x].GetComponent<Node>().LinkedNodes.Add(newTile);
				}
				if(x > 0){
					newTile.GetComponent<Node>().LinkedNodes.Add(thePathfinder.Nodes[y-1][x-1]);
					thePathfinder.Nodes[y-1][x-1].GetComponent<Node>().LinkedNodes.Add(newTile);
				}
				//Link last node of the row with EnemySpawnNode
				if(x == numberOfTilesColumn-1){
					newTile.GetComponent<Node>().LinkedNodes.Add(thePathfinder.Nodes[y-1][numberOfTilesColumn]);
					thePathfinder.Nodes[y-1][numberOfTilesColumn].GetComponent<Node>().LinkedNodes.Add(newTile);
				}
			}
		}

		//Link core with the nodes
		Core.GetComponent<Node>().LinkedNodes.Add(thePathfinder.Nodes[numberOfTilesRow/2][0]);
		thePathfinder.Nodes[numberOfTilesRow/2][0].GetComponent<Node>().LinkedNodes.Add(Core);
		thePathfinder.coreNode = Core;
	}
}
