using UnityEngine;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour {

	public float health;
	public float maxHealth;
	public float currHealth;
	public Enemy enemy;

	// Use this for initialization
	void Start () {
		health = enemy.GetComponent<Enemy> ().health;
		maxHealth = health;
		currHealth = health / maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		health = enemy.GetComponent<Enemy> ().health;
		currHealth = health / maxHealth;
		transform.localScale = new Vector3 (currHealth, transform.localScale.y, transform.localScale.z);
	}
}
