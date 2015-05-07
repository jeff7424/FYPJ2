using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// Script to create a grid of tiles

	public Tile tile;
	public int numberOfTilesColumn = 10;
	public int numberOfTilesRow = 9;
	public float tileSize = 1.0f;

	// Use this for initialization
	void Start () {
		CreateTiles ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateTiles() {
		float xOffset = 0.0f;
		float yOffset = 0.0f;

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
				}
				
				Instantiate (tile, new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), Quaternion.identity);
			}
		}
	}
}
