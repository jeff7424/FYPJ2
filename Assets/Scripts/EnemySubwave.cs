using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemySubwave {

	public float spawnRate;
	private float timeLastSpawn = 0.0f;

	private bool active = false;
	public float activateTime;

	public int[] Enemies = new int[(int)global::Enemy.enemyType.TYPE_MAX];
	
	// Update is called once per frame
	public int Update () {
		//If there's no more enemy to spawn, return -1
		if(getTotalEnemies() <= 0)
			return -1;

		//Spawn a random enemy from the list Enemies in the subwave based on spawnRate
		if(Time.time - timeLastSpawn >= spawnRate){
			timeLastSpawn = Time.time;

			int spawnType = Random.Range (0, Enemies.Length);
			while(Enemies[spawnType] <= 0)
				spawnType = Random.Range (0, Enemies.Length);
			--Enemies[spawnType];

			return spawnType;
		}

		return -1;
	}

	public int getTotalEnemies(){
		int total = 0;

		foreach(int monster in Enemies)
			total += monster;

		return total;
	}

	public bool isActive(){return active;}
	public void Activate(){active = true;}
}
