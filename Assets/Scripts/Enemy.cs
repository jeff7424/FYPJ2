using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Sprite slow;
	public Sprite normal;
	public Sprite fast;
	public Sprite jump;
	public Sprite fly;
	public Sprite split;

	public float health;
	public int reward;
	public float slow_duration = 0.0f;
	public float fire_duration = 0.0f;
	public float damageRate = 0.3f;
	private float speed;
	private float originalSpeed;
	private float finalSpeed = 0.0f;
	private float effectValue = 0.0f;
	private float burnDamage = 0.0f;
	public bool slowByBuff = false;
	private GameObject game;

	public enum enemyType{
		TYPE_NORMAL = 0,
		TYPE_SLOW,
		TYPE_FAST,
		TYPE_JUMP,
		TYPE_FLY,
		TYPE_SPLIT_PARENT,
		TYPE_SPLIT_CHILD,
		TYPE_MAX
	}
	private enemyType type;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
	}

	void OnDestroy(){
		if(type == enemyType.TYPE_SPLIT_PARENT){
			for(int i = 0; i < 2; ++i){
				GameObject child = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().spawnEnemy(enemyType.TYPE_SPLIT_CHILD);
				child.transform.position = this.transform.position + new Vector3(i*0.25f, i*0.25f, 0);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		// If health less than zero destroy object
		if (health <= 0) {
			Destroy (gameObject);
			game.GetComponent<Game>().enemyLeft --;
			game.GetComponent<Game>().resources += reward;
		}
		if (slow_duration > 0.0f) {
			slow_duration -= Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.blue;
			finalSpeed = speed * effectValue;
		} else {
			effectValue = 0.0f;
			//speed = originalSpeed;
			finalSpeed = speed;
			GetComponent<SpriteRenderer>().color = Color.white;
			slowByBuff = false;
		}

		if (fire_duration > 0.0f) {
			fire_duration -= Time.deltaTime;
			damageRate -= Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.red;
			if (damageRate <= 0.0f) {
				health -= burnDamage;
				damageRate = 0.3f;
			}
		} else {
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	public float getSpeed(){
		return speed;
	}

	public float getOriginalSpeed() {
		return originalSpeed;
	}

	public float getFinalSpeed(){
		return finalSpeed;
	}

	public void setSpeed(float new_speed) {
		this.speed = new_speed;
	}

	public void setEffectValue(float effectValue){
		this.effectValue = effectValue;
	}

	public void setBurnDamage(float damage) {
		this.burnDamage = damage;
	}

	public void setType(enemyType newType){
		type = newType;

		switch(type){
		case enemyType.TYPE_NORMAL:
			health = 25;
			reward = 5;
			originalSpeed = 1.0f;
			speed = originalSpeed;
			gameObject.name = "Normal";
			GetComponent<SpriteRenderer>().sprite = normal;
			break;

		case enemyType.TYPE_SLOW:
			health = 50;
			reward = 10;
			originalSpeed = 0.5f;
			speed = originalSpeed;
			gameObject.name = "Slow";
			GetComponent<SpriteRenderer>().sprite = slow;
			break;

		case enemyType.TYPE_FAST:
			health = 10;
			reward = 5;
			originalSpeed = 1.5f;
			speed = originalSpeed;
			gameObject.name = "Fast";
			GetComponent<SpriteRenderer>().sprite = fast;
			break;

		case enemyType.TYPE_JUMP:
			health = 35;
			reward = 5;
			originalSpeed = 1.0f;
			speed = originalSpeed;
			gameObject.name = "Jump";
			GetComponent<SpriteRenderer>().sprite = jump;
			break;
			
		case enemyType.TYPE_FLY:
			health = 25;
			reward = 5;
			originalSpeed = 1.0f;
			speed = originalSpeed;
			gameObject.name = "Fly";
			GetComponent<SpriteRenderer>().sprite = fly;
			break;
			
		case enemyType.TYPE_SPLIT_PARENT:
			health = 40;
			reward = 15;
			originalSpeed = 1.0f;
			speed = originalSpeed;
			gameObject.name = "SplitParent";
			GetComponent<SpriteRenderer>().sprite = split;
			break;

		case enemyType.TYPE_SPLIT_CHILD:
			health = 15;
			reward = 5;
			originalSpeed = 1.3f;
			speed = originalSpeed;
			gameObject.name = "SplitChild";
			GetComponent<SpriteRenderer>().sprite = split;
			gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
			break;
		}

		gameObject.tag = "Enemy";
	}

	public enemyType getType(){
		return type;
	}

	public void SlowByBuff(float duration, float newValue) {
		slowByBuff = true;
		this.slow_duration = duration;
		this.effectValue = newValue;
	}

	public void Kamikazed(int damage) {
		health -= damage;
	}
}
