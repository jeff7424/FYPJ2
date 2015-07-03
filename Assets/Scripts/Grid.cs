using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	// Script to create a grid of tiles
	public GameObject thePathfinderRoot;
	public GameObject EnemySpawner;
	public GameObject Core;
	public PathfinderScript thePathfinder;

	public GameObject node;
	public GameObject tile;
	public int numberOfTilesColumn = 10;
	public int numberOfTilesRow = 9;
	public float tileSize = 1.0f;

	//Testing
	public GameObject obstacleTile;
	public int maxSpawn;

	// Use this for initialization
	void Start () {
		CreateTiles ();
		maxSpawn = 0;
	}

	// Update is called once per frame
	void Update () {

	}

	void CreateTiles() {
		float xOffset = 0.0f;
		float yOffset = 0.0f;
		thePathfinder.GetComponent<PathfinderScript>().CreateNodesArray(numberOfTilesRow, numberOfTilesColumn+1);

		for (int y = 0; y < numberOfTilesRow;) {
			for (int x = 0; x < numberOfTilesColumn; ++x) {
				
				// Do offset after instantiating a tile
				xOffset += tileSize;
				
				// Reset x to the start and +1 to y to start next row
				if (x % numberOfTilesColumn == 0)
				{
					yOffset += tileSize;
					xOffset = 0;
					y++;

					//Create a node that serves as enemy spawn points
					GameObject newEnemySpawnNode = (GameObject)Instantiate (node, new Vector2(transform.position.x + tileSize*numberOfTilesColumn, transform.position.y + yOffset), Quaternion.identity);
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
				GameObject newTile = (GameObject)Instantiate (tile, new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), Quaternion.identity);
				newTile.name = "Tile" + ((y-1)*9+x).ToString();
				newTile.transform.SetParent(thePathfinderRoot.transform);

				//Create Obstacles
				int i = Random.Range (1,25); //Random between 1 to 11
				if (i == 5)
				{
					if (maxSpawn < 3)
					{
						GameObject newObstacleTile = (GameObject)Instantiate (obstacleTile, new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), Quaternion.identity);
						newObstacleTile.transform.SetParent(thePathfinderRoot.transform);
						maxSpawn++;
					}
				}

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
		Core.GetComponent<Core>().pathNode = (GameObject)Instantiate(node, Core.transform.position, Quaternion.identity);
		Core.GetComponent<Core>().pathNode.name = "enemyTargetPoint";
		Core.GetComponent<Core>().pathNode.transform.SetParent(thePathfinderRoot.transform);
		Core.GetComponent<Core>().pathNode.GetComponent<Node>().LinkedNodes.Add(thePathfinder.Nodes[numberOfTilesRow/2][0]);
		thePathfinder.Nodes[numberOfTilesRow/2][0].GetComponent<Node>().LinkedNodes.Add(Core.GetComponent<Core>().pathNode);
		thePathfinder.coreNode = Core.GetComponent<Core>().pathNode;
	}
}
