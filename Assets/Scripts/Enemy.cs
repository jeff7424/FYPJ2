using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Sprite slow;
	public Sprite normal;
	public Sprite fast;
	public Sprite jump;

	public int health;
	private float speed;

	public enum enemyType{
		TYPE_NORMAL = 0,
		TYPE_SLOW,
		TYPE_FAST,
		TYPE_JUMP,
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
			GetComponent<SpriteRenderer>().sprite = normal;
			break;

		case enemyType.TYPE_SLOW:
			health = 50;
			speed = 0.5f;
			gameObject.name = "Slow";
			GetComponent<SpriteRenderer>().sprite = slow;
			break;

		case enemyType.TYPE_FAST:
			health = 10;
			speed = 1.5f;
			gameObject.name = "Fast";
			GetComponent<SpriteRenderer>().sprite = fast;
			break;

		case enemyType.TYPE_JUMP:
			health = 35;
			speed = 1.0f;
			gameObject.name = "Jump";
			GetComponent<SpriteRenderer>().sprite = jump;
			break;
		}

		GetComponent<EnemyMovementAI>().speed = speed;
	}

	public enemyType getType(){
		return type;
	}
}
