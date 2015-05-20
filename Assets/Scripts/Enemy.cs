using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int health;
	private float speed;

	public enum enemyType{
		TYPE_NORMAL = 0,
		TYPE_SLOW,
		TYPE_FAST,
		TYPE_MAX
	}
	private enemyType type;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// If health less than zero destroy object
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public float getSpeed(){
		return speed;
	}

	public void setType(enemyType newType){
		type = newType;

		switch(type){
		case enemyType.TYPE_NORMAL:
			health = 25;
			speed = 1.0f;
			gameObject.name = "Normal";
			break;

		case enemyType.TYPE_SLOW:
			health = 50;
			speed = 0.5f;
			gameObject.name = "Slow";
			break;

		case enemyType.TYPE_FAST:
			health = 10;
			speed = 1.5f;
			gameObject.name = "Fast";
			break;
		}

		GetComponent<EnemyMovementAI>().speed = speed;
	}
}
